using UnityEngine;

public class CloseDisplayInteraction : MonoBehaviour, IInteractable
{

   public void Interact()
   {
      UserInterfaceManager.Instance.HideItem();
      PlayerManager.Instance.UnlockPlayerMovement();
      Destroy(this.gameObject);
   }

   public void Clear()
   {
      PlayerManager.Instance.UnlockPlayerMovement();
      UserInterfaceManager.Instance.HideItem();
      Destroy(this.gameObject);
   }
}
