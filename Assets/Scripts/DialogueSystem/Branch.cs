using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DialogueSystem
{
    [System.Serializable]
    public class ConditionPair
    {
        public string key;
        public bool value;
    }
    
    [System.Serializable]
    public class Branch
    {
        // I wanted this to be a Dictionary (Hashmap) but that doesn't show up in the inspector
        [SerializeField] private List<ConditionPair> branchConditions = new List<ConditionPair>();
        [SerializeField] private DialogueObject dialogueObject;

        public Dictionary<string, bool> BranchConditions => branchConditions.ToDictionary(c => c.key, c => c.value);

        public DialogueObject DialogueObject => dialogueObject;

        public bool ConditionsAreTrue(Dictionary<string, bool> receivedConditions)
        {
            if (branchConditions.Count == 0)
            {
                Debug.LogWarning("Switched to a new branch without any branch (name =" + dialogueObject.name + ") conditions.");
                return true;
            }
            return branchConditions.All(condition =>
                receivedConditions.TryGetValue(condition.key, out bool value) && value == condition.value
            );
        }
    }
}
