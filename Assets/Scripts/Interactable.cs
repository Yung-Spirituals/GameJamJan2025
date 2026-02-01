using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour
{
   [SerializeField] private GameObject attentionIndicator;
   [SerializeField] private string interactPrompt = "Press E to interact";
   private GameObject interactPromptUI;
   private Collider2D triggerCollider;
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

      // Find interact prompt UI if not assigned
      if (interactPromptUI == null)
      {
         // First try to find as child
         interactPromptUI = transform.Find("InteractPrompt")?.gameObject;

         // If not found as child, search by name in scene
         if (interactPromptUI == null)
         {
            GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
            interactPromptUI = Array.Find(allObjects, obj =>
               obj.name == "InteractPrompt" || obj.name == "Interact Prompt" || obj.name == "InteractPromptUI");
         }
      }

      interactionManager = InteractionManager.Instance;
      if (interactionManager == null)
      {
      }
   }

   public void ClearInteractionPrompt()
   {
      if (interactPromptUI != null)
      {
         interactPromptUI.SetActive(false);
      }
   }

   // Update is called once per frame
   // called when this GameObject collides with GameObject2.
   void OnTriggerEnter2D(Collider2D col)
   {
      if (col.CompareTag("Player"))
      {
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
      if (col.CompareTag("Player"))
      {
         // Remove prompt when player exits trigger
         if (attentionIndicator != null) attentionIndicator.SetActive(true);
         if (interactPromptUI != null) interactPromptUI.SetActive(false);
         interactionManager.ClearCurrentInteractable();
      }
   }
}
