using DialogueSystem;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] private DialogueFrame[] dialogue;
    [SerializeField] private Branch[] conditionalBranches;
    [SerializeField] private Response[] responses;
    
    public DialogueFrame[] Dialogue => dialogue;
    
    public bool HasConditionalBranches => conditionalBranches != null && conditionalBranches.Length > 0;
    
    public Branch[] ConditionalBranches => conditionalBranches;

    public bool HasResponses => Responses != null && Responses.Length > 0;
    
    public Response[] Responses => responses;
    
}

[System.Serializable]
public class DialogueFrame
{
    [SerializeField] [TextArea] private string text;
    [SerializeField] private CharacterObject characterObject;

    public string Text => text;
    public CharacterObject CharacterObject => characterObject;
}
