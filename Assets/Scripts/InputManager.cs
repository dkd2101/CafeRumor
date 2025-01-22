using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 Moveement;

    private PlayerInput playerInput;
    private InputAction moveAction;
    public static InputAction advanceDialogue;
    public static InputAction interactKey;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        advanceDialogue = playerInput.actions["ContinueDialogue"];
        interactKey = playerInput.actions["Interact"];
    }

    private void Update()
    {
        Moveement = moveAction.ReadValue<Vector2>();
        // advanceDialogue.performed += ctx => Debug.Log($"Input detected: {ctx.control}");
    }
}
