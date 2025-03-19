using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObj")]
public class DialogueObj : ScriptableObject
{
    public DialogueFrames[] Dialogue;
    public bool RequiresItem;
    public string RequiredItemName;
    public bool givesItem;
    public Item itemToGive;
    public DialogueObj CorrectItemDialogue;
    public DialogueObj WrongItemDialogue;
}

[System.Serializable]
public class DialogueFrames
{
    public string CharacterName;
    [TextArea] public string Text;
}
