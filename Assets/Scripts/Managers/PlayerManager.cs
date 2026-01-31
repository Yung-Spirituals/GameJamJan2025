using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   public static PlayerManager Instance;
   private GameObject player;

   private void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
         player = GameObject.FindWithTag("Player");
         DontDestroyOnLoad(gameObject);
      }
      else
      {
         Destroy(gameObject);
      }
   }

   public GameObject GetPlayer()
   {
      return player;
   }

   public void SetPlayer(GameObject playerObj)
   {
      player = playerObj;
   }

   public void RespawnPlayer(Vector3 respawnPosition)
   {
      if (player != null)
      {
         player.transform.position = respawnPosition;
         EntityStatus status = player.GetComponent<EntityStatus>();
         if (status != null)
         {
            status.Heal(9999); // Heal to full health
         }
      }
   }
}
