using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour
{
    [SerializeField] private string sceneName = "";
  
    private void Awake()
    {
        
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            try
            {
                SceneManager.LoadScene(sceneName);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Failed to load scene: " + sceneName + ".\nError: " + e.Message);
            }
        }
    }
}
