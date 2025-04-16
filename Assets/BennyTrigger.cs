using System;
using System.Collections;
using System.Collections.Generic;
using GameState;
using UnityEngine;

public class BennyTrigger : MonoBehaviour
{
    private GameObject child;

    [SerializeField] private Item pork;

    [SerializeField] private string rumorName = "DogOnTheHighway";

    [SerializeField] private SpiritCutSceneManager spiritCutSceneManager;
    // Start is called before the first frame update
    void Start()
    {
        child = transform.GetChild(0).gameObject;
        if (!GameStateManager.getInstance().getRumorData(rumorName, "RecipeCreated"))
        {
            child.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!child.activeSelf && other.CompareTag("Player"))
        {
            if (!GameStateManager.getInstance().isRumorComplete(rumorName)
                && !GameStateManager.getInstance().getRumorData(rumorName, "RecipeCreated")
                && GameStateManager.getInstance().getRumorData(rumorName, "Benny")
                && InventorySystem.Instance.HasItem(pork)
                // Time? I want to make it a singleton so it is not ass to access.
                )
            {
                spiritCutSceneManager.StartAppearance(child);
                // InventorySystem.Instance.RemoveItem("Pork");
            }
        }
    }
}
