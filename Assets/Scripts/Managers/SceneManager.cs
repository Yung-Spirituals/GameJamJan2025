using UnityEngine;

public class SceneManager : MonoBehaviour
{
   public static SceneManager Instance;
   public string currentSceneName;

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
      currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
   }

   public void LoadScene(string sceneName)
   {
      UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
      currentSceneName = sceneName;
   }
}
