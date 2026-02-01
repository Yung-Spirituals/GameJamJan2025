using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LeverInteractable : MonoBehaviour, IInteractable
{
   private bool isUp = false;
   private SpriteRenderer spriteRenderer;
   [SerializeField] private Sprite leverUpSprite;
   [SerializeField] private Sprite leverDownSprite;
   [SerializeField] private int leverID;
   private ProgressManager progressManager;
   public void Interact()
   {
      isUp = !isUp;
      if (isUp)
      {
         spriteRenderer.sprite = leverUpSprite;
         progressManager.SetProgressFlag("LeverActivated_" + leverID, false);
      }
      else
      {
         spriteRenderer.sprite = leverDownSprite;
         progressManager.SetProgressFlag("LeverActivated_" + leverID, true);
      }
   }
   public void Clear() { }
   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      progressManager = ProgressManager.Instance;

      if (progressManager.GetProgressFlag("SolvedPuzzle"))
      {
         GetComponent<Collider2D>().enabled = false;
      }
      spriteRenderer = GetComponent<SpriteRenderer>();
      spriteRenderer.sprite = progressManager.GetProgressFlag("LeverActivated_" + leverID) ? leverDownSprite : leverUpSprite;
      isUp = !progressManager.GetProgressFlag("LeverActivated_" + leverID);
   }

   void Update()
   {
      if (progressManager.GetProgressFlag("SolvedPuzzle") && GetComponent<Collider2D>().enabled)
      {
         GetComponent<Collider2D>().enabled = false;
      }
   }
}
