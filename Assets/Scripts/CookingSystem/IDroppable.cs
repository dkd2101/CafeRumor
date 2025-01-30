using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDroppable
{
    void OnDrop(Draggable ingredient);
}
