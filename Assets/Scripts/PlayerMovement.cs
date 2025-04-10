using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 movement;

    private Rigidbody2D rb;
    private Animator animator;

    public DialogueUI dialogueUI;
    public IInteractable interactable { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dialogueUI = FindObjectOfType<DialogueUI>();
    }

    private void Update()
    {
        if (dialogueUI.IsOpen)
        {
            movement = Vector2.zero;
            rb.velocity = Vector2.zero;
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
            return;
        };
        
        movement.Set(InputManager.Moveement.x, InputManager.Moveement.y);

        rb.velocity = movement * moveSpeed;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);

        if (movement != Vector2.zero)
        {
            animator.SetFloat("LastHorizontal", movement.x);
            animator.SetFloat("LastVertical", movement.y);
        }

        if (InputManager.interactKey.triggered && !dialogueUI.IsOpen)
        {
            interactable?.Interact(this);
        }
    }
}
