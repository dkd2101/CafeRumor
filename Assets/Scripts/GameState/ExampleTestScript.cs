using System;
using UnityEngine;

namespace GameState
{
    public class ExampleTestScript : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("TestRumorData Checkpoint1: " + GameStateManager.getInstance().getRumorData("TestRumorData", "Checkpoint1"));
            Debug.Log("Setting TestRumorData Checkpoint1 to true");
            GameStateManager.getInstance().setRumorData("TestRumorData", "Checkpoint1", true);
            Debug.Log("TestRumorData Checkpoint1: " + GameStateManager.getInstance().getRumorData("TestRumorData", "Checkpoint1"));
            Debug.Log("TestRumorData Completed: " + GameStateManager.getInstance().isRumorComplete("TestRumorData"));
            Debug.Log("Setting TestRumorData Checkpoint1 to false");
            GameStateManager.getInstance().setRumorData("TestRumorData", "Checkpoint1", false);
            Debug.Log("TestRumorData Checkpoint1: " + GameStateManager.getInstance().getRumorData("TestRumorData", "Checkpoint1"));
            Debug.Log("TestRumorData Completed: " + GameStateManager.getInstance().isRumorComplete("TestRumorData"));
        }
    }
}