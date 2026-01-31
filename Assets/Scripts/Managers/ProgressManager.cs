using UnityEngine;
using System.Collections.Generic;

public class ProgressManager : MonoBehaviour
{
   public static ProgressManager Instance;
   private Dictionary<string, bool> progressFlags;

   private void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
         progressFlags = new Dictionary<string, bool>();
         DontDestroyOnLoad(gameObject);
      }
      else
      {
         Destroy(gameObject);
      }
   }

   public void SetProgressFlag(string flag, bool value)
   {
      progressFlags[flag] = value;
   }

   public bool GetProgressFlag(string flag)
   {
      return progressFlags.ContainsKey(flag) && progressFlags[flag];
   }
}
