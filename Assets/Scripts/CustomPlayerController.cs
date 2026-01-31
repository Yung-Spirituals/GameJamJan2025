using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
//using System.Diagnostics;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class CustomPlayerController : MonoBehaviour
{
   [SerializeField] private float baseMoveSpeed = 10f;
   [SerializeField] private float sprintMultiplier = 1.5f;
   [SerializeField] private float dodgeDistance = 3f;
   [SerializeField] private float dodgeCooldown = 2f;
   [SerializeField] private float dodgeInvincibilityDuration = 0.5f;
   [SerializeField] private Vector2 defaultDodgeDirection = Vector2.up;

   private Vector2 moveInput = Vector2.zero;
   private bool isSprinting = false;
   private bool canDodge = true;
   private float lastDodgeTime = -Mathf.Infinity;

   // Cached animation parameter hashes for performance
   private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
   //private static readonly int MoveHash = Animator.StringToHash("Move");
   private static readonly int DodgeHash = Animator.StringToHash("Dodge");

   private Rigidbody2D rb;
   private Animator animator;
   private SpriteRenderer spriteRenderer;
   private Collider2D playerCollider;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      Debug.Log("=== CustomPlayerController Start() called ===");

      rb = GetComponent<Rigidbody2D>();
      if (rb == null)
      {
         Debug.LogError("Rigidbody2D component missing on " + gameObject.name);
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
      if (animator == null) Debug.Log("Animator component missing on " + gameObject.name);

      spriteRenderer = GetComponent<SpriteRenderer>();
      playerCollider = GetComponent<Collider2D>();
      if (playerCollider == null) Debug.Log("Collider2D component missing on " + gameObject.name);
   }

   // Update is called once per frame
   void Update()
   {
      HandleDodgeCooldown();
   }

   void FixedUpdate()
   {
      HandleMovement();
   }

   private void HandleMovement()
   {
      if (rb == null) return;

      if (moveInput.magnitude < 0.1f) return;
      float currentSpeed = isSprinting ? baseMoveSpeed * sprintMultiplier : baseMoveSpeed;
      Vector2 movement = moveInput.normalized * currentSpeed * Time.fixedDeltaTime;
      rb.MovePosition(rb.position + movement);

      // Handle character facing direction
      HandleCharacterFacing(moveInput);

      if (animator != null)
      {
         if (moveInput.magnitude > 0.1f)
         {
            animator.SetBool(IsMovingHash, true);
            // Set movement direction parameters for animator
            animator.SetFloat("MoveX", moveInput.x);
            animator.SetFloat("MoveY", moveInput.y);
         }
         else
         {
            animator.SetBool(IsMovingHash, false);
         }
      }
   }

   private void HandleCharacterFacing(Vector2 inputDirection)
   {
      if (inputDirection.magnitude < 0.1f) return;

      // Flip sprite horizontally based on movement direction
      if (spriteRenderer != null)
      {
         if (inputDirection.x > 0.1f)
            spriteRenderer.flipX = false; // Face right
         else if (inputDirection.x < -0.1f)
            spriteRenderer.flipX = true;  // Face left
      }
   }

   private void HandleDodgeCooldown()
   {
      if (!canDodge && Time.time - lastDodgeTime >= dodgeCooldown)
      {
         canDodge = true;
      }
   }

   private IEnumerator DodgeInvincibility()
   {
      playerCollider.enabled = false;
      yield return new WaitForSeconds(dodgeInvincibilityDuration);
      playerCollider.enabled = true;
   }

   public void OnMove(InputAction.CallbackContext context)
   {
      Debug.Log($"=== OnMove called! Phase: {context.phase} ===");

      if (context.action == null)
      {
         Debug.LogError("OnMove: context.action is null!");
         return;
      }

      Vector2 newInput = context.ReadValue<Vector2>();
      moveInput = newInput;

      Debug.Log($"OnMove: Input = {moveInput}, Phase = {context.phase}");
   }

   public void OnSprint(InputAction.CallbackContext context)
   {
      Debug.Log($"OnSprint called! Phase: {context.phase}, Performed: {context.performed}");
      isSprinting = context.performed;
   }

   public void OnDodge(InputAction.CallbackContext context)
   {
      Debug.Log($"OnDodge called! Phase: {context.phase}, Performed: {context.performed}, CanDodge: {canDodge}");

      if (context.performed && canDodge && rb != null)
      {
         Vector2 dodgeDirection = moveInput.normalized;
         if (dodgeDirection == Vector2.zero)
         {
            dodgeDirection = defaultDodgeDirection;
         }

         Vector2 dodgeTarget = rb.position + dodgeDirection * dodgeDistance;
         rb.MovePosition(dodgeTarget);

         StartCoroutine(DodgeInvincibility());

         canDodge = false;
         lastDodgeTime = Time.time;

         animator?.SetTrigger(DodgeHash);

         Debug.Log($"Dodge executed to: {dodgeTarget}");
      }
   }
}
