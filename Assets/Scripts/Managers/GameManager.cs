using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;

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

   public void StartGame()
   {
      Debug.Log("Game Started");
      // Logic to start the game goes here
   }
}
