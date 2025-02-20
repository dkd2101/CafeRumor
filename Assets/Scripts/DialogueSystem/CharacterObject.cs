using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/CharacterObject")]
public class CharacterObject : ScriptableObject
{
    [SerializeField] private string characterName;
    
    public string CharacterName => characterName;
}
