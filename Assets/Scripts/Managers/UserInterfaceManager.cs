using UnityEngine;
using System;

public class UserInterfaceManager : MonoBehaviour
{
   public static UserInterfaceManager Instance;

   [SerializeField] private GameObject messagePanel;
   [SerializeField] private TMPro.TextMeshProUGUI messageText;
   [SerializeField] private GameObject itemDisplayPanel;
   [SerializeField] private TMPro.TextMeshProUGUI itemDisplayText;
   [SerializeField] private UnityEngine.UI.Image itemDisplaySpriteRenderer;
   [SerializeField] private GameObject closeDisplayInteraction;
   [SerializeField] private GameObject imageDisplayPanel;
   [SerializeField] private UnityEngine.UI.Image imageDisplaySpriteRenderer;

   [SerializeField] private GameObject textDisplayPanel;

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
      }

      if (messageText == null)
      {
         messageText = messagePanel.GetComponentInChildren<TMPro.TextMeshProUGUI>();
      }

      if (itemDisplayPanel == null)
      {
         // Find panel by name (works with inactive objects and prefabs)
         GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
         itemDisplayPanel = Array.Find(allObjects, obj =>
            obj.name == "ItemDisplayPanelCanObj");
      }

      if (itemDisplayText == null && itemDisplayPanel != null)
      {
         itemDisplayText = itemDisplayPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>();
      }

      if (itemDisplaySpriteRenderer == null && itemDisplayPanel != null)
      {
         itemDisplaySpriteRenderer = itemDisplayPanel.GetComponentInChildren<UnityEngine.UI.Image>();
      }

      if (closeDisplayInteraction == null)
      {
         // Find prefab by name in Resources or project
         closeDisplayInteraction = Resources.Load<GameObject>("CloseDisplayInteraction");
      }
   }

   public void ShowMessage(string message)
   {
      if (messagePanel == null)
      {
         return;
      }

      if (messageText == null)
      {
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

   public void DisplayItem(Sprite itemSprite, string itemText)
   {
      if (itemDisplayPanel == null)
      {
         return;
      }

      // Activate the display element
      itemDisplayPanel.SetActive(true);

      // Set the sprite if available
      if (itemSprite != null && itemDisplaySpriteRenderer != null)
      {
         itemDisplaySpriteRenderer.sprite = itemSprite;
      }

      // Set the text if available
      if (itemDisplayText != null)
      {
         itemDisplayText.text = itemText;
      }

      // Create close interaction prefab on player
      if (closeDisplayInteraction != null)
      {
         if (PlayerManager.Instance != null)
         {
            PlayerManager.Instance.LockPlayerMovement();
            Transform playerTransform = PlayerManager.Instance.GetPlayerTransform();
            if (playerTransform != null)
            {
               Vector3 playerPosition = playerTransform.position;
               GameObject closeInteractionInstance = Instantiate(closeDisplayInteraction, playerPosition, Quaternion.identity);
            }
         }
      }
   }

   public void DisplayImage(Sprite itemSprite)
   {
      if (imageDisplayPanel == null)
      {
         return;
      }

      // Activate the display element
      imageDisplayPanel.SetActive(true);
      // Set the sprite if available
      if (itemSprite != null && imageDisplaySpriteRenderer != null)
      {
         imageDisplaySpriteRenderer.sprite = itemSprite;
      }

      // Create close interaction prefab on player
      if (closeDisplayInteraction != null)
      {
         if (PlayerManager.Instance != null)
         {
            PlayerManager.Instance.LockPlayerMovement();
            Transform playerTransform = PlayerManager.Instance.GetPlayerTransform();
            if (playerTransform != null)
            {
               Vector3 playerPosition = playerTransform.position;
               GameObject closeInteractionInstance = Instantiate(closeDisplayInteraction, playerPosition, Quaternion.identity);
            }
         }
      }
   }

   public void DisplayText(string itemText)
   {
      if (textDisplayPanel == null)
      {
         return;
      }

      // Activate the display element
      textDisplayPanel.SetActive(true);
      textDisplayPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = itemText;

      // Create close interaction prefab on player
      if (closeDisplayInteraction != null)
      {
         if (PlayerManager.Instance != null)
         {
            PlayerManager.Instance.LockPlayerMovement();
            Transform playerTransform = PlayerManager.Instance.GetPlayerTransform();
            if (playerTransform != null)
            {
               Vector3 playerPosition = playerTransform.position;
               GameObject closeInteractionInstance = Instantiate(closeDisplayInteraction, playerPosition, Quaternion.identity);
            }
         }
      }
   }

   public void HideItem()
   {
      if (itemDisplayPanel != null)
      {
         itemDisplayPanel.SetActive(false);
      }
      if (imageDisplayPanel != null)
      {
         imageDisplayPanel.SetActive(false);
      }
      if (textDisplayPanel != null)
      {
         textDisplayPanel.SetActive(false);
      }
   }
}
