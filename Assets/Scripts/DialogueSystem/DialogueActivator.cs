using System;
using System.Collections;
using System.Collections.Generic;
using GameState;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    // modify this class to select based on day/time.
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private string associatedRumor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement player))
        {
            Debug.Log("enter");
            player.interactable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement player))
        {
            if (player.interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                Debug.Log("exit");
                player.interactable = null;
            }
        }
    }

    public void Interact(PlayerMovement movement)
    {
        Dictionary<string, bool> dictionary;
        try
        {
            dictionary = GameStateManager.getInstance().getRumorDataAsDictionary(associatedRumor);
            foreach (var pair in dictionary)
            {
                Debug.Log(pair.Key + ": " + pair.Value);
            }
            //InventorySystem.Instance
        }
        catch
        {
            Debug.Log(associatedRumor + " was not found by the dialogue activator. Make sure you are using the rumor's filename");
            dictionary = movement.dialogueUI.NpcConditions;
        }
        movement.dialogueUI.ShowDialogue(dialogueObject, dictionary);
    }
}
