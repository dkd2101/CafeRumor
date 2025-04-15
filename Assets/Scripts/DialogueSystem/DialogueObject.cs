using DialogueSystem;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] private DialogueFrame[] dialogue;
    [SerializeField] private Branch[] conditionalBranches;
    [SerializeField] private Response[] responses;
    [SerializeField] private EndStateChanges[] endStateChanges;
    [SerializeField] private RecipeSO[] recipiesToAdd;
    [SerializeField] private AudioClip voice;

    public DialogueFrame[] Dialogue => dialogue;

    public bool HasConditionalBranches => conditionalBranches != null && conditionalBranches.Length > 0;

    public Branch[] ConditionalBranches => conditionalBranches;

    public bool HasResponses => Responses != null && Responses.Length > 0;

    public Response[] Responses => responses;

    public EndStateChanges[] EndStateChanges => endStateChanges;

    public RecipeSO[] RecipiesToAdd => recipiesToAdd;

    public AudioClip Voice => voice;
}

[System.Serializable]
public class DialogueFrame
{
    [SerializeField][TextArea] private string text;
    [SerializeField] private CharacterObject characterObject;
    [SerializeField] private float pitch = 2.2f;

    public string Text => text;
    public CharacterObject CharacterObject => characterObject;
    public float Pitch => pitch;
}

[System.Serializable]
public class EndStateChanges
{
    [SerializeField] private string rumorName;
    [SerializeField] private string name;
    [SerializeField] private bool value;
    public string RumorName => rumorName;
    public string Name => name;
    public bool Value => value;
}
