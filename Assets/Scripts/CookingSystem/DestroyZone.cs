using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour, IDroppable
{
    public void OnDrop(Draggable ingredient)
    {
        Destroy(ingredient.gameObject);
    }
}
