using UnityEngine;

public class OpenTextboxInteractable : MonoBehaviour, IInteractable
{

   private UserInterfaceManager uiManager;
   private bool isActivated = false;

   [SerializeField] private string message = "This is a textbox interaction!";

   private void Start()
   {
      uiManager = UserInterfaceManager.Instance;
   }

   public void Interact()
   {
      if (!isActivated)
      {
         uiManager.ShowMessage(message);
         isActivated = true;
      }
      else
      {
         Clear();
      }
   }

   public void Clear()
   {
      if (isActivated)
      {
         uiManager.HideMessage();
         isActivated = false;
      }
   }
}
