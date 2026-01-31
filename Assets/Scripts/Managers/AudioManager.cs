using UnityEngine;

public class AudioManager : MonoBehaviour
{
   public static AudioManager Instance;

   private void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
         DontDestroyOnLoad(gameObject);
      }
      else
      {
         Destroy(gameObject);
      }
   }

   public void PlaySound(string soundName)
   {
      Debug.Log("Playing sound: " + soundName);
      // Logic to play the sound goes here
   }
}
