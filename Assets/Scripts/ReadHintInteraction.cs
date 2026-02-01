using UnityEngine;

public class ReadHintInteraction : MonoBehaviour, IInteractable
{
   [SerializeField] private bool isImageHint = false;
   [SerializeField] private Sprite hintSprite;
   [SerializeField] private string hintText;

   public void Interact()
   {
      if (isImageHint)
      {
         UserInterfaceManager.Instance.DisplayImage(hintSprite);
         return;
      }
      UserInterfaceManager.Instance.DisplayText(hintText);
   }

   public void Clear() { }
}
