using UnityEditor;
using UnityEngine;

namespace GameState.Editor
{
    [CustomEditor(typeof(RumorData))]
    public class RumorDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            RumorData rumorData = (RumorData)target;
            
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Set all to FALSE"))
            {
                foreach (var checkpoint in rumorData.getCheckpointsAsDictionary())
                {
                    rumorData.setCheckpointStatus(checkpoint.Key, false);
                }
            }
            
            if (GUILayout.Button("Set all to TRUE"))
            {
                foreach (var checkpoint in rumorData.getCheckpointsAsDictionary())
                {
                    rumorData.setCheckpointStatus(checkpoint.Key, true);
                }
            }
            GUILayout.EndHorizontal();
            
            DrawDefaultInspector();
        }
    }
}