using System;
using Unity.VisualScripting;
using UnityEngine;

public class GhostWiggle : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] baseVertices;
    public float waveStrength = 0.1f;
    public float waveSpeed = 2.0f;
    public float scaleFactor = 0f; // Scaling factor
    public float scaleSpeed = 0.05f;
    public float maxScale = 1;
    public float topWiggleAmount = 0.2f; // Side-to-side movement strength

    private float currentTopWiggleAmount;
    private Vector3 pivotPoint; // Bottom middle pivot
    private int topLeftIndex = -1, topRightIndex = -1; // Indices for top points

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        baseVertices = mesh.vertices.Clone() as Vector3[];
        
        scaleFactor = 0f;
        currentTopWiggleAmount = topWiggleAmount;
        Vector3[] collapsedVertices = new Vector3[baseVertices.Length];
        for (int i = 0; i < collapsedVertices.Length; i++)
        {
            collapsedVertices[i] = pivotPoint; // or Vector3.zero, but pivotPoint looks better
        }

        // makes sure there is not a frame where it starts at full scale
        mesh.vertices = collapsedVertices;
        mesh.RecalculateBounds();
        
        // Find the bottom middle point
        float minY = float.MaxValue, maxY = float.MinValue;
        float sumX = 0;
        int count = 0;

        for (int i = 0; i < baseVertices.Length; i++)
        {
            Vector3 v = baseVertices[i];

            // Find bottom center pivot
            if (v.y < minY)
            {
                minY = v.y;
                sumX = v.x;
                count = 1;
            }
            else if (Mathf.Approximately(v.y, minY))
            {
                sumX += v.x;
                count++;
            }

            // Find top two points
            if (v.y > maxY)
            {
                maxY = v.y;
                topLeftIndex = i;
                topRightIndex = i; // Assume same at first
            }
            else if (Mathf.Approximately(v.y, maxY))
            {
                if (v.x < baseVertices[topLeftIndex].x)
                    topLeftIndex = i;
                if (v.x > baseVertices[topRightIndex].x)
                    topRightIndex = i;
            }
        }

        pivotPoint = new Vector3(sumX / count, minY, 0); // Bottom center pivot
    }

    void Update()
    {
        if (scaleFactor == maxScale)
        {
            currentTopWiggleAmount = Mathf.MoveTowards(currentTopWiggleAmount, 0, scaleSpeed * Time.deltaTime);
        }
        Vector3[] vertices = new Vector3[baseVertices.Length];

        for (int i = 0; i < baseVertices.Length; i++)
        {
            Vector3 originalVertex = baseVertices[i];

            // Calculate direction from pivot
            Vector3 direction = originalVertex - pivotPoint;

            // Scale outward from the pivot
            Vector3 scaledVertex = pivotPoint + direction * scaleFactor;

            // Apply vertical wave effect (for all vertices)
            float waveOffset = Mathf.Sin(Time.time * waveSpeed + scaledVertex.x * 2) * waveStrength;
            scaledVertex.y += waveOffset;

            // Apply horizontal side-to-side motion ONLY for top points
            if (i == topLeftIndex || i == topRightIndex)
            {
                float horizontalOffset = Mathf.Sin(Time.time * waveSpeed) * currentTopWiggleAmount;
                scaledVertex.x += horizontalOffset;
            }

            vertices[i] = scaledVertex;
        }

        // Apply modified vertices
        mesh.vertices = vertices;
        mesh.RecalculateBounds();

        scaleFactor = Mathf.MoveTowards(scaleFactor, maxScale, scaleSpeed * Time.deltaTime);
    }
}
