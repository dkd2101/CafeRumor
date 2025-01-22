using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    // modify this class to select based on day/time.
    [SerializeField] private DialogueObject dialogueObject;

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
        movement.dialogueUI.ShowDialogue(dialogueObject);
    }
}
