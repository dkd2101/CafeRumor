using System.Collections.Generic;
using UnityEngine;

namespace GameState
{
    // a key pair object for representing an checkpoint wihtin a rumor
    [System.Serializable]
    public class RumorCheckpoint
    {
        // the key to refer to this checkpoint
        public string key;

        // whether or not this checkpoint of the rumor is 
        public bool isCompleted;

        // the description to be displayed on the UI when reaching this checkpoint
        [TextArea(10, 20)]
        public string statusDescription;
    }


    [CreateAssetMenu(fileName = "RumorData", menuName = "GameState/RumorData")]
    public class RumorData : ScriptableObject
    {
        [SerializeField] private string rumorName;
        [SerializeField] private List<RumorCheckpoint> rumorCheckpoints = new List<RumorCheckpoint>();

        // the key value of the current checkpoint being worked on by the player
        private string curKey;


        public void OnEnable()
        {
            this.curKey = rumorCheckpoints[0].key;
        }
        
        public bool GetCheckpointStatus(string key)
        {
            var status = rumorCheckpoints.Find(status => status.key.Equals(key));
            return status != null ? status.isCompleted : false;
        }

        public string GetCurrentDescription() {
            int index = rumorCheckpoints.FindIndex(checkpoint => checkpoint.key.Equals(curKey));
            return rumorCheckpoints[index].statusDescription;
        }

        public void setCheckpointStatus(string key, bool value)
        {
            int index = rumorCheckpoints.FindIndex(checkpoint => checkpoint.key.Equals(key));
            if (index != -1)
            {
                rumorCheckpoints[index].isCompleted = value;
                if(!isCompleted())
                    curKey = rumorCheckpoints[Mathf.Clamp(index + 1, 0, rumorCheckpoints.Count - 1)].key;
            }
            else // I'm not entirely sure why this else branch is necessarry
            {
                rumorCheckpoints.Add(new RumorCheckpoint { key = key, isCompleted = value });
            }
        }

        public bool isCompleted()
        {
            return rumorCheckpoints.TrueForAll(checkpoint => checkpoint.isCompleted);
        }

        public Dictionary<string, bool> getCheckpointsAsDictionary()
        {
            Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
            foreach (RumorCheckpoint rumorCheckpoint in rumorCheckpoints)
            {
                dictionary.Add(rumorCheckpoint.key, rumorCheckpoint.isCompleted);
            }

            return dictionary;
        }

        public string getRumorName()
        {
            return name;
        }
    }
}