using UnityEngine;
using System;

public class UserInterfaceManager : MonoBehaviour
{
   public static UserInterfaceManager Instance;

   [SerializeField] private GameObject messagePanel;
   [SerializeField] private TMPro.TextMeshProUGUI messageText;

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

      if (messagePanel == null)
      {
         // Find panel by name (works with inactive objects and prefabs)
         GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
         messagePanel = Array.Find(allObjects, obj =>
            obj.name == "MessagePanel" || obj.name == "Message Panel");

         if (messagePanel == null)
            Debug.LogError("MessagePanel not found! Create a GameObject named 'MessagePanel'.");
      }

      if (messageText == null)
      {
         messageText = messagePanel.GetComponentInChildren<TMPro.TextMeshProUGUI>();
         if (messageText == null)
            Debug.LogError("TextMeshProUGUI component not found! Create one under MessagePanel.");
      }
   }

   public void ShowMessage(string message)
   {
      Debug.Log("UI Message: " + message);

      if (messagePanel == null)
      {
         Debug.LogError("MessagePanel is null! Cannot show message: " + message);
         return;
      }

      if (messageText == null)
      {
         Debug.LogError("MessageText is null! Cannot show message: " + message);
         return;
      }

      messagePanel.SetActive(true);
      messageText.text = message;
   }

   public void HideMessage()
   {
      if (messagePanel != null)
         messagePanel.SetActive(false);

      if (messageText != null)
         messageText.text = "";
   }
}
