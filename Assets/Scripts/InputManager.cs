using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public static Vector2 Movement => Instance.pauseMovement ? Vector2.zero : Instance.moveAction.ReadValue<Vector2>();

    private PlayerInput playerInput;
    private InputAction moveAction;
    public InputAction advanceDialogue;
    public InputAction interactKey;

    private bool pauseMovement = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        advanceDialogue = playerInput.actions["ContinueDialogue"];
        interactKey = playerInput.actions["Interact"];

        DontDestroyOnLoad(gameObject);
    }

    public void PauseMovement() => pauseMovement = true;
    public void UnPauseMovement() => pauseMovement = false;
}

