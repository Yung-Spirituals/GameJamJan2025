using Microsoft.VisualBasic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour
{
   [SerializeField] private GameObject attentionIndicator;
   [SerializeField] private string interactPrompt = "Press E to interact";
   [SerializeField] private GameObject interactPromptUI;
   [SerializeField] private Collider2D triggerCollider;
   [SerializeField] private bool isInteractable = true;
   [SerializeField] private bool hasInteracted = false;


   private IInteractable linkedInteractable;
   private InteractionManager interactionManager;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      linkedInteractable = GetComponent<IInteractable>();
      triggerCollider ??= GetComponent<Collider2D>();
      triggerCollider.isTrigger = true;
      interactionManager = InteractionManager.Instance;
      if (interactionManager == null)
      {
         Debug.LogError("InteractionManager instance not found in the scene.");
      }
   }

   // Update is called once per frame
   // called when this GameObject collides with GameObject2.
   void OnTriggerEnter2D(Collider2D col)
   {
      Debug.Log("GameObject1 collided with " + col.name + " " + col.tag + " " + (col.CompareTag("Player") ? "is Player" : "not Player"));
      if (col.CompareTag("Player"))
      {
         Debug.Log("Entered interaction zone.");
         if (attentionIndicator != null) attentionIndicator.SetActive(false);
         if (interactPromptUI != null) interactPromptUI.SetActive(true);
         if (linkedInteractable != null)
         {
            interactionManager.SetCurrentInteractable(linkedInteractable);
         }
      }
   }

   void OnTriggerExit2D(Collider2D col)
   {
      Debug.Log("GameObject1 exited collision with " + col.name + " " + col.tag + " " + (col.CompareTag("Player") ? "is Player" : "not Player"));
      if (col.CompareTag("Player"))
      {
         // Remove prompt when player exits trigger
         if (attentionIndicator != null) attentionIndicator.SetActive(true);
         if (interactPromptUI != null) interactPromptUI.SetActive(false);
         Debug.Log("Exited interaction zone.");
         interactionManager.ClearCurrentInteractable();
      }
   }
}
