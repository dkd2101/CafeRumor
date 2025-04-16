using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameState
{
    public class GameStateManager : MonoBehaviour
    {
        private static GameStateManager instance;
        [SerializeField] private GameData gameData;
        
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                // this should ensure there is only one GameStateManager in a scene
                Destroy(gameObject);
            }
        }
        
        public static GameStateManager getInstance()
        {
            return instance;
        }
        
        public void setRumorData(string rumorName, string key, bool value)
        {
            gameData.getRumorData(rumorName).setCheckpointStatus(key, value);
        }

        public bool getRumorData(string rumorName, string key)
        {
            return gameData.getRumorData(rumorName).GetCheckpointStatus(key);
        }

        public Dictionary<string, bool> getRumorDataAsDictionary(string rumorName)
        {
            return gameData.getRumorData(rumorName).getCheckpointsAsDictionary();
        }

        public bool isRumorComplete(string rumorName)
        {
            return gameData.getRumorData(rumorName).isCompleted();
        }

        public void resetGameData(bool flag = false)
        {
            gameData.resetAllRumorStates(flag);
        }

        private void OnApplicationQuit()
        {
            resetGameData();
        }
    }
}
