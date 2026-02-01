using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(EntityStatus))]
public class Attack : MonoBehaviour
{
   [SerializeField] private int attackDamage = 1;
   [SerializeField] private float attackCooldown = 1f;
   [SerializeField] private float attackRange = 1f;
   [SerializeField] private GameObject weapon;
   private float lastAttackTime = -Mathf.Infinity;
   private EntityStatus entityStatus;

   void Start()
   {
      entityStatus = GetComponent<EntityStatus>();
   }

   public int GetAttackDamage()
   {
      return attackDamage;
   }

   // Method to perform an attack on a target
   public void OnAttack(InputAction.CallbackContext context)
   {
      if (!context.performed) return;
      if (Time.time - lastAttackTime < attackCooldown)
      {
         return;
      }
      // Perform attack logic here (e.g., detect enemies in range, apply damage)
      lastAttackTime = Time.time;

      // Trigger attack animation on the weapon if it exists
      if (weapon != null && weapon.GetComponent<Animator>() != null)
         weapon.GetComponent<Animator>().SetTrigger("Attack");
   }
}
