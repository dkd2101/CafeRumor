using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSys : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private InventorySystem inventorySystem;

    private bool isWaitingForItem = false;
    private DialogueObj currentDialogue;

    private void Start()
    {
        dialogueBox.SetActive(false);
    }

    public void StartDialogue(DialogueObj dialogueObject)
    {
        currentDialogue = dialogueObject;
        dialogueBox.SetActive(true);
        StartCoroutine(ProcessDialogue(dialogueObject));
    }

    private IEnumerator ProcessDialogue(DialogueObj dialogueObject)
    {
        foreach (var frame in dialogueObject.Dialogue)
        {
            textLabel.text = frame.Text;

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        if (dialogueObject.RequiresItem)
        {
            isWaitingForItem = true;

            if (inventorySystem.HasItem(dialogueObject.RequiredItemName))
            {
                inventorySystem.OpenInventory(HandleItemGiven);
            }
            else
            {
                textLabel.text = "You don't have the required item.";
                yield return new WaitForSeconds(2f);
                EndDialogue();
            }
        }
        else
        {
            EndDialogue();
        }
    }

    private void HandleItemGiven(Item selectedItem)
    {
        if (selectedItem.itemName == currentDialogue.RequiredItemName)
        {
            inventorySystem.RemoveItem(selectedItem.itemName);
            StartDialogue(currentDialogue.CorrectItemDialogue);
        }
        else
        {
            StartDialogue(currentDialogue.WrongItemDialogue);
        }

        isWaitingForItem = false;
    }

    private void EndDialogue()
    {
        dialogueBox.SetActive(false);

        // Ensure NPC only gives items without wiping the inventory
        if (currentDialogue.givesItem && currentDialogue.itemToGive != null)
        {
            inventorySystem.AddItem(currentDialogue.itemToGive);
            UnityEngine.Debug.Log($"Received Item: {currentDialogue.itemToGive.itemName}");
        }
    }


}
