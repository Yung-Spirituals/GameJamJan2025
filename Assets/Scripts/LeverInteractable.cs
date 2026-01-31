using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LeverInteractable : MonoBehaviour, IInteractable
{
   private bool isUp = false;
   private SpriteRenderer spriteRenderer;
   [SerializeField] private Sprite leverUpSprite;
   [SerializeField] private Sprite leverDownSprite;
   [SerializeField] private int leverID;
   public void Interact()
   {
      isUp = !isUp;
      if (isUp)
      {
         spriteRenderer.sprite = leverUpSprite;
         ProgressManager.Instance.SetProgressFlag("LeverActivated_" + leverID, true);
      }
      else
      {
         spriteRenderer.sprite = leverDownSprite;
         ProgressManager.Instance.SetProgressFlag("LeverActivated_" + leverID, false);
      }
   }
   public void Clear() { }
   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      spriteRenderer = GetComponent<SpriteRenderer>();
      spriteRenderer.sprite = ProgressManager.Instance.GetProgressFlag("LeverActivated_" + leverID) ? leverUpSprite : leverDownSprite;
      isUp = ProgressManager.Instance.GetProgressFlag("LeverActivated_" + leverID);
   }
}
