using UnityEngine;

public class InteractionManager : MonoBehaviour
{
   public static InteractionManager Instance;
   [SerializeField] private GameObject player;
   private IInteractable currentInteractable;

   private void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
      }
      else
      {
         Destroy(gameObject);
      }
   }

   public void Interact()
   {
      currentInteractable?.Interact();
   }

   public void SetCurrentInteractable(IInteractable interactable)
   {
      currentInteractable = interactable;
   }

   public void ClearCurrentInteractable()
   {
      currentInteractable.Clear();
      currentInteractable = null;
   }
}
