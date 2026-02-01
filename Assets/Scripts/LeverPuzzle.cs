using UnityEngine;
using System.Collections.Generic;

public class LeverPuzzle : MonoBehaviour
{
   private ProgressManager progressManager;
   private bool isCompleted = false;
   private bool isLooted = false;

   [SerializeField] private ChestInteractable interactableChestObject;


   private Dictionary<string, bool> solutionStates = new Dictionary<string, bool>()
   {
      {"LeverActivated_1", true},
      {"LeverActivated_2", false},
      {"LeverActivated_3", false},
      {"LeverActivated_4", true},
      {"LeverActivated_5", false},
      {"LeverActivated_6", false}
   };

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      progressManager = ProgressManager.Instance;
      isCompleted = progressManager.GetProgressFlag("SolvedPuzzle");
      isLooted = progressManager.GetProgressFlag("PickedUpMaskPart1");
   }

   private void CheckPuzzleCompletion()
   {
      foreach (var state in solutionStates)
      {
         if (progressManager.GetProgressFlag(state.Key) != state.Value)
         {
            return; // Puzzle not yet solved
         }
      }
      // Trigger puzzle completion events here
      isCompleted = true;
      progressManager.SetProgressFlag("SolvedPuzzle", true);
   }

   // Update is called once per frame
   void Update()
   {
      if (isCompleted)
      {
         // Only enable the chest collider once when puzzle completes, then let ChestInteractable handle it
         if (!isLooted && !progressManager.GetProgressFlag("ChestOpened"))
         {
            interactableChestObject.GetComponent<Collider2D>().enabled = true;
         }
         return; // No need to check further
      }
      CheckPuzzleCompletion();
   }
}
