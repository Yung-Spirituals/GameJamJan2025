using UnityEngine;

public class PickupItem : MonoBehaviour
{
   [SerializeField] private Sprite itemSprite;
   private GameObject itemInstance;


   public void Pickup(string pickupText, string itemCollectFlag)
   {
      // Show the item in the UI using UI manager
      UserInterfaceManager.Instance.DisplayItem(itemSprite, pickupText);


      // Disable the item in the world
      if (itemInstance != null)
      {
         itemInstance.SetActive(false);
         GetComponent<Collider2D>().enabled = false;
      }

      // Update progress
      ProgressManager.Instance.SetProgressFlag(itemCollectFlag, true);
   }
}
