using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    // Materials for default and hover states
    private Material defaultMaterial, hoverMaterial;

    // Sprite renderer and collider components
    private SpriteRenderer spriteRenderer;
    private Collider2D col;

    // Dragging state and positions
    private bool isDragging;
    private Vector2 offset, originalPos, startDragPos;

    private void Start()
    {
        // Initialize materials
        defaultMaterial = new Material(Shader.Find("Sprites/Default"));
        hoverMaterial = new Material(Shader.Find("Custom/2DOutlineShader"));

        // Get components
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        // Store original position
        originalPos = transform.position;
    }

    private void Update()
    {
        // Update position while dragging
        if (isDragging)
        {
            var mousePosition = GetMousePos();
            transform.position = mousePosition - offset;
        }
    }

    private void OnMouseDown()
    {
        // Start dragging
        isDragging = true;

        // Calculate offset
        offset = GetMousePos() - (Vector2)transform.position;
        startDragPos = transform.position;
    }

    private void OnMouseUp()
    {
        // Stop dragging
        isDragging = false;

        // Check for drop zone
        col.enabled = false;
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);
        col.enabled = true;

        if (hitCollider != null && hitCollider.TryGetComponent(out IDroppable droppable))
        {
            // Handle drop
            droppable.OnDrop(this);
        }
        else
        {
            // Return to start position if no drop zone
            transform.position = startDragPos;
        }
    }

    private void OnMouseEnter()
    {
        // Change material on hover
        spriteRenderer.material = hoverMaterial;
    }

    private void OnMouseExit()
    {
        // Revert material on hover exit
        spriteRenderer.material = defaultMaterial;
    }

    private Vector2 GetMousePos()
    {
        // Get mouse position in world coordinates
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
