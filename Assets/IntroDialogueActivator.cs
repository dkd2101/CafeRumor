using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameState;

public class IntroDialogueRunner : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private string associatedRumor;

    [SerializeField] private string nextSceneName = "MainScene";
    [SerializeField] private float delayAfterDialogue = 2f;

    private void Start()
    {
        StartCoroutine(PlayIntroDialogue());
    }

    private IEnumerator PlayIntroDialogue()
    {
        yield return null;

        Dictionary<string, bool> conditions;

        try
        {
            conditions = GameStateManager.getInstance().getRumorDataAsDictionary(associatedRumor);
        }
        catch
        {
            UnityEngine.Debug.LogWarning($"Rumor '{associatedRumor}' not found — using empty conditions.");
            conditions = new Dictionary<string, bool>();
        }

        dialogueUI.ShowDialogue(dialogueObject, conditions);

        yield return new WaitUntil(() => !dialogueUI.IsOpen);
        yield return new WaitForSeconds(delayAfterDialogue);

        // Transition to the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
