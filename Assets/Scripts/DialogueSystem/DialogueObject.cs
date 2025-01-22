using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogue;
    [SerializeField] private Response[] responses;
    
    // TODO: should also be able to accept an object that 

    public string[] Dialogue => dialogue;

    public bool HasResponses => Responses != null && Responses.Length > 0;
    
    public Response[] Responses => responses;
}
