using System.Collections.Generic;
using UnityEngine;

namespace GameState
{
    [System.Serializable]
    public class RumorPair
    {
        public string key;
        public bool value;
    }
    
    [CreateAssetMenu(fileName = "RumorData", menuName = "GameState/RumorData")]
    public class RumorData : ScriptableObject
    { 
        [SerializeField] private List<RumorPair> rumorCheckpoints = new List<RumorPair>();

        public bool getCheckpointStatus(string key)
        {
            var pair = rumorCheckpoints.Find(pair => pair.key.Equals(key));
            return pair != null ? pair.value : false;
        }

        public void setCheckpointStatus(string key, bool value)
        {
            int index = rumorCheckpoints.FindIndex(pair => pair.key.Equals(key));
            if (index != -1)
            {
                rumorCheckpoints[index].value = value;
            }
            else
            {
                rumorCheckpoints.Add(new RumorPair { key = key, value = value });
            }
        }

        public bool isCompleted()
        {
            return rumorCheckpoints.TrueForAll(pair => pair.value);
        }

        public string getRumorName()
        {
            return name;
        }
    }
}