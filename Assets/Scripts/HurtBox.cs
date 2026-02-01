using UnityEngine;

public class HurtBox : MonoBehaviour
{
   void OnTriggerEnter2D(Collider2D col)
   {
      if (col.CompareTag("Weapon") && col.GetComponent<Attack>() != null)
      {
         Attack attack = col.GetComponent<Attack>();
         EntityStatus entityStatus = GetComponentInParent<EntityStatus>();
         if (entityStatus != null)
         {
            entityStatus.TakeDamage(attack.GetAttackDamage());
         }
      }
   }
}
