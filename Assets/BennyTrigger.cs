using System;
using System.Collections;
using System.Collections.Generic;
using GameState;
using UnityEngine;

public class BennyTrigger : MonoBehaviour
{
    private GameObject child;

    [SerializeField] private Item pork;
    // Start is called before the first frame update
    void Start()
    {
        child = transform.GetChild(0).gameObject;
        child.SetActive(false);

        // Comment out later if we have a non dumb way to get pork
        InventorySystem.Instance.AddItem(pork);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!child.activeSelf && other.CompareTag("Player"))
        {
            if (!GameStateManager.getInstance().isRumorComplete("DogOnTheHighway")
                && GameStateManager.getInstance().getRumorData("DogOnTheHighway", "Benny")
                && InventorySystem.Instance.HasItem(pork)
                // Time? I want to make it a singleton so it is not ass to access.
                )
            {
                child.SetActive(true);
                // InventorySystem.Instance.RemoveItem("Pork");
            }
        }
    }
}
