using System;
using System.Collections;
using System.Collections.Generic;
using GameState;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private GameObject dialogueBox;
    private TypewriterEffect typewriterEffect;
    
    public bool IsOpen { get; private set; }

    private ResponseHandler responseHandler;
    private Dictionary<string, bool> npcConditions;

    public Dictionary<string, bool> NpcConditions
    {
        get => npcConditions;
        set => npcConditions = value;
    }

    private void Start()
    {
        CloseDialogueBox();
        typewriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
    }

    public void ShowDialogue(DialogueObject dialogueObject, Dictionary<string, bool> npcCtx = null)
    {
        npcConditions = npcCtx ?? new Dictionary<string, bool>();
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {

        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i].Text;
            string name = dialogueObject.Dialogue[i].CharacterObject.CharacterName;
            nameLabel.text = name;
            //TODO: look into making the character name background the same width as name?
            yield return RunTypingEffect(dialogue);
            
            textLabel.text = dialogue;

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;
            
            // must wait a frame to make sure advanceDialogue is not still triggered
            yield return null;

            yield return new WaitUntil(() => InputManager.advanceDialogue.triggered);
        }

        if (dialogueObject.HasConditionalBranches)
        {
            foreach (var branch in dialogueObject.ConditionalBranches)
            {
                if (branch.ConditionsAreTrue(npcConditions))
                {
                    ShowDialogue(branch.DialogueObject, npcConditions);
                    break;
                }
            }
        } else if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBox();
            TriggerStateChanges(dialogueObject);
            AddNewRecipies(dialogueObject);
        }
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typewriterEffect.Run(dialogue, textLabel);

        while (typewriterEffect.IsRunning)
        {
            yield return null;
            
            if (InputManager.advanceDialogue.triggered)
            {
                typewriterEffect.Stop();
            }
        }
    }

    private void CloseDialogueBox()
    {
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }

    private static void TriggerStateChanges(DialogueObject dialogueObject)
    {
        foreach (var change in dialogueObject.EndStateChanges)
        {
            try
            {
                GameStateManager.getInstance().setRumorData(change.RumorName, change.Name, change.Value);
            }
            catch (Exception e)
            {
                Debug.Log("Failed " + e.Message);
            }
        }
    }

    private void AddNewRecipies(DialogueObject dialogueObject)
    {
        foreach (var r in dialogueObject.RecipiesToAdd)
        {
            try
                {
                    InventorySystem.Instance.AddRecipe(r);
                }
                catch (Exception e)
                {
                    Debug.Log("Failed " + e.Message);
                }
            }
    }
}
