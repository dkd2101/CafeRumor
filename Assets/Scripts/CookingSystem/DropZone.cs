using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour, IDroppable
{
    public void OnDrop(Draggable ingredient)
    {
        ingredient.transform.position = transform.position;
    }
}
