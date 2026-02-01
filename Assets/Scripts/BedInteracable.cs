using UnityEngine;

public class BedInteracable : MonoBehaviour, IInteractable
{
   [SerializeField] private Transform getUpPosition;
   [SerializeField] private Collider2D bedCollider;
   private bool isOccupied = false;
   private PlayerManager playerManager;

   private void Start()
   {
      playerManager = PlayerManager.Instance;
   }

   public void Interact()
   {
      Transform playerTransform = playerManager.GetPlayerTransform();
      if (!isOccupied)
      {
         isOccupied = true;
         playerTransform.position = transform.position; // Move player to bed position
         playerTransform.rotation = transform.rotation; // Align player rotation with bed
         playerManager.LockPlayerMovement();
         bedCollider.enabled = false;
      }
      else
      {
         isOccupied = false;
         playerTransform.position = getUpPosition.position; // Move player to get up position
         playerTransform.rotation = Quaternion.identity; // Reset player rotation
         playerManager.UnlockPlayerMovement();
         bedCollider.enabled = true;
      }
   }

   public void Clear()
   {
      if (isOccupied)
      {
         isOccupied = false;
         Transform playerTransform = playerManager.GetPlayerTransform();
         playerTransform.position = getUpPosition.position; // Move player to get up position
         playerTransform.rotation = Quaternion.identity; // Reset player rotation
         playerManager.UnlockPlayerMovement();
         bedCollider.enabled = true;
      }
   }
}
