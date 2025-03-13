using System.Diagnostics;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private DialogueObj dialogueObject;
    [SerializeField] private DialogueSys dialogueSystem;

    private bool isPlayerInRange = false;

    private void Update()
    {
        // Trigger dialogue when player presses 'E'
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TriggerDialogue();
        }
    }

    private void TriggerDialogue()
    {
        dialogueSystem.StartDialogue(dialogueObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            UnityEngine.Debug.Log("Player entered NPC range.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            UnityEngine.Debug.Log("Player exited NPC range.");
        }
    }
}
