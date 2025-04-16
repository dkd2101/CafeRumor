using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameState
{
    [CreateAssetMenu(fileName = "GameData", menuName = "GameState/GameData")]
    public class GameData : ScriptableObject
    {
        [SerializeField] private List<RumorData> rumorsData = new List<RumorData>();

        public RumorData getRumorData(string name)
        {
            RumorData rumorData = rumorsData.Find(data => data.getRumorName().Equals(name));
            if (rumorData)
            {
                return rumorData;
            }
            else
            {
                throw new Exception("RumorData not found for requested name: " + name 
                            + ". Consider checking your GameStateManager's rumorsData list.");
            }
        }

        public void resetAllRumorStates(bool flag = false)
        {
            foreach (var rumorData in rumorsData)
            {
                foreach (var checkpoint in rumorData.getCheckpointsAsDictionary())
                {
                    rumorData.setCheckpointStatus(checkpoint.Key, flag);
                }
            }
        }
    }
}