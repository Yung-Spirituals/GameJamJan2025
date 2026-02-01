using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class CustomPlayerController : MonoBehaviour
{
   [SerializeField] private float baseMoveSpeed = 10f;
   [SerializeField] private float sprintMultiplier = 1.5f;
   [SerializeField] private float dodgeDistance = 2f; // Reduced from 3f
   [SerializeField] private float dodgeCooldown = 2f;
   [SerializeField] private float dodgeDuration = 0.1f; // Reduced from 0.2f
   [SerializeField] private Vector2 defaultDodgeDirection = Vector2.up;

   private Vector2 moveInput = Vector2.zero;
   private bool isSprinting = false;
   private bool canDodge = true;
   private bool isDodging = false;
   private float dodgeEndTime = 0f;
   private float lastDodgeTime = -Mathf.Infinity;

   // Cached animation parameter hashes for performance
   private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
   //private static readonly int MoveHash = Animator.StringToHash("Move");
   private static readonly int DodgeHash = Animator.StringToHash("Dodge");

   private Rigidbody2D rb;
   private Animator animator;
   private SpriteRenderer spriteRenderer;
   private Collider2D playerCollider;
   private bool movementIsLocked = false;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      rb = GetComponent<Rigidbody2D>();
      if (rb == null)
      {
         return;
      }

      // Complete Rigidbody2D configuration for 2D top-down movement
      rb.gravityScale = 0f; // Completely disable gravity
      rb.freezeRotation = true; // Prevent rotation
      rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Extra rotation lock
      rb.linearDamping = 0f; // No drag
      rb.angularDamping = 0f; // No angular drag
      rb.bodyType = RigidbodyType2D.Dynamic; // Dynamic for movement
      rb.linearVelocity = Vector2.zero; // Ensure starting velocity is zero
      rb.angularVelocity = 0f; // Ensure no rotation velocity

      animator = GetComponent<Animator>();

      spriteRenderer = GetComponent<SpriteRenderer>();
      playerCollider = GetComponent<Collider2D>();
   }

   // Update is called once per frame
   void Update()
   {
      HandleDodgeCooldown();
   }

   void FixedUpdate()
   {
      if (movementIsLocked) return;
      HandleMovement();
   }

   private void HandleMovement()
   {
      if (rb == null) return;

      // Check if dodge is finished and clear velocity
      if (isDodging && Time.time >= dodgeEndTime)
      {
         isDodging = false;
         rb.linearVelocity = Vector2.zero; // Stop dodge momentum
      }

      // Allow movement during dodge end phase for smoother transition
      if (isDodging && Time.time < dodgeEndTime - 0.05f) // Allow movement in last 0.05s of dodge
      {
         return;
      }

      bool hasInput = moveInput.magnitude > 0.1f;

      // Only perform movement if there's input
      if (hasInput)
      {
         float currentSpeed = isSprinting ? baseMoveSpeed * sprintMultiplier : baseMoveSpeed;
         Vector2 movement = moveInput.normalized * currentSpeed * Time.fixedDeltaTime;
         rb.MovePosition(rb.position + movement);

         // Handle character facing direction
         HandleCharacterFacing(moveInput);
      }

      // Always update animator regardless of input state
      if (animator != null)
      {
         animator.SetBool(IsMovingHash, hasInput);
      }
   }

   private void HandleCharacterFacing(Vector2 inputDirection)
   {
      if (inputDirection.magnitude < 0.1f) return;

      // Flip entire game object horizontally based on movement direction
      Vector3 scale = transform.localScale;

      if (inputDirection.x > 0.1f)
         scale.x = Mathf.Abs(scale.x); // Face right (positive scale)
      else if (inputDirection.x < -0.1f)
         scale.x = -Mathf.Abs(scale.x); // Face left (negative scale)

      transform.localScale = scale;
   }

   private void HandleDodgeCooldown()
   {
      if (!canDodge && Time.time - lastDodgeTime >= dodgeCooldown)
      {
         canDodge = true;
      }
   }

   public void OnMove(InputAction.CallbackContext context)
   {
      if (context.action == null)
      {
         return;
      }

      Vector2 newInput = context.ReadValue<Vector2>();
      moveInput = newInput;
   }

   public void OnSprint(InputAction.CallbackContext context)
   {
      isSprinting = context.performed;
   }

   public void OnDodge(InputAction.CallbackContext context)
   {
      if (context.performed && canDodge && rb != null && !isDodging)
      {
         Vector2 dodgeDirection = moveInput.normalized;
         if (dodgeDirection == Vector2.zero)
         {
            dodgeDirection = defaultDodgeDirection;
         }

         // Start dodge state
         isDodging = true;
         dodgeEndTime = Time.time + dodgeDuration;

         // Apply dodge force for smooth movement instead of teleport
         float dodgeForce = dodgeDistance / dodgeDuration; // Calculate force needed
         rb.linearVelocity = dodgeDirection * dodgeForce;

         canDodge = false;
         lastDodgeTime = Time.time;

         animator?.SetTrigger(DodgeHash);
      }
   }

   public void OnInteract(InputAction.CallbackContext context)
   {
      if (context.performed)
      {
         InteractionManager.Instance.Interact();
      }
   }

   public void LockMovement()
   {
      movementIsLocked = true;
      moveInput = Vector2.zero;
      isSprinting = false;
      rb.linearVelocity = Vector2.zero;
      rb.angularVelocity = 0f;
      if (animator != null)
      {
         animator.SetBool(IsMovingHash, false);
      }
   }

   public void UnlockMovement()
   {
      movementIsLocked = false;
   }
}
