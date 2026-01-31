using UnityEngine;

public class SwitchSceneInteractable : MonoBehaviour, IInteractable
{

   private SceneManager sceneManager;
   [SerializeField] private string sceneToLoad;
   public void Interact()
   {
      sceneManager.LoadScene(sceneToLoad);
   }
   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      sceneManager = SceneManager.Instance;
   }
   public void Clear()
   {
   }
}
