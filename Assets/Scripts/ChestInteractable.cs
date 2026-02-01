using UnityEngine;

public class ChestInteractable : MonoBehaviour, IInteractable
{
   private SpriteRenderer spriteRenderer;
   [SerializeField] private Sprite chestOpenSprite;
   [SerializeField] private Sprite chestClosedSprite;
   [SerializeField] private PickupItem pickupItem;
   private bool isOpen = false;
   public void Interact()
   {
      if (isOpen)
      {
         return;
      }
      isOpen = !isOpen;
      if (isOpen)
      {
         spriteRenderer.sprite = chestOpenSprite;
         ProgressManager.Instance.SetProgressFlag("ChestOpened", true);
         GetComponent<Collider2D>().enabled = false;
         if (pickupItem != null)
         {
            pickupItem.Pickup("You found a mask part!", "PickedUpMaskPart1");
         }
      }
      else
      {
         spriteRenderer.sprite = chestClosedSprite;
         ProgressManager.Instance.SetProgressFlag("ChestOpened", false);
      }
   }
   public void Clear() { }
   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      spriteRenderer = GetComponent<SpriteRenderer>();
      spriteRenderer.sprite = ProgressManager.Instance.GetProgressFlag("PickedUpMaskPart1") ? chestOpenSprite : chestClosedSprite;
      isOpen = ProgressManager.Instance.GetProgressFlag("PickedUpMaskPart1");
      if (ProgressManager.Instance.GetProgressFlag("SolvedPuzzle") && !isOpen)
      {
         GetComponent<Collider2D>().enabled = true;
      }
      else
      {
         GetComponent<Collider2D>().enabled = false;
      }
   }
}
