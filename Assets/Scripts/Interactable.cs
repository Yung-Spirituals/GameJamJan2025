using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour
{
   [SerializeField] private string interactPrompt = "Press E to interact";
   [SerializeField] private Collider2D triggerCollider;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      triggerCollider ??= GetComponent<Collider2D>();
      triggerCollider.isTrigger = true;
   }

   // Update is called once per frame
   // called when this GameObject collides with GameObject2.
   void OnTriggerEnter2D(Collider2D col)
   {
      Debug.Log("GameObject1 collided with " + col.name);
      if (col.CompareTag("Player"))
      {
         Debug.Log(interactPrompt);
      }
      // Show prompt to player
   }

   void OnTriggerExit2D(Collider2D col)
   {
      Debug.Log("GameObject1 exited collision with " + col.name);
      if (col.CompareTag("Player"))
      {
         Debug.Log("Exited interaction zone.");
      }
      // Remove prompt when player exits trigger
   }
}
