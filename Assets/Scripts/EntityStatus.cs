using UnityEngine;

public class EntityStatus : MonoBehaviour
{
   [SerializeField] private int maxHealth = 3;
   private int currentHealth;

   private bool isDead = false;

   // Awake is called once when the script instance is being loaded
   public void Awake()
   {
      currentHealth = maxHealth;
   }

   public void Update()
   {
      if (currentHealth < 1)
      {
         // Handle entity death (e.g., play animation, disable controls)
         isDead = true;
      }
   }

   public int GetCurrentHealth()
   {
      return currentHealth;
   }

   public bool IsDead()
   {
      return isDead;
   }

   public void TakeDamage(int damage)
   {
      currentHealth -= damage;
      if (currentHealth < 0)
      {
         currentHealth = 0;
      }
   }

   public void Heal(int amount)
   {
      currentHealth += amount;
      if (currentHealth > maxHealth)
      {
         currentHealth = maxHealth;
      }
   }
}
