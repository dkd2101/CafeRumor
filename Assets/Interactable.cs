using UnityEngine;
using System;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public GameObject interactionPanel;
    private bool playerInRange = false;
    public UnityEvent<bool> interactedWith;

    void Start() {
        interactedWith.AddListener(GameObject.FindObjectOfType<TimeManager>().PauseTime);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            interactionPanel.SetActive(true);
            interactedWith.Invoke(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player in range");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}