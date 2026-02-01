using UnityEngine;

public class PickupItem : MonoBehaviour
{
   [SerializeField] private Sprite itemSprite;
   private GameObject itemInstance;
   [SerializeField] private GameObject displayItemUiElement;
   private UserInterfaceManager uiManager;

   private void Start()
   {
      uiManager = UserInterfaceManager.Instance;
   }


   public void Pickup(string pickupText, string itemCollectFlag)
   {
      // Show the item in the UI using UI manager
      if (uiManager != null)
      {
         uiManager.DisplayItem(itemSprite, pickupText);
      }

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
