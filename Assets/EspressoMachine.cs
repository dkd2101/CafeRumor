using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspressoMachine : MonoBehaviour
{
    public GameObject prefab; 
    public Transform spawnPoint;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Espresso Pod") 
        {
            GameObject newItem = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            newItem.name = prefab.name;
         
            BoxCollider2D collider = newItem.GetComponent<BoxCollider2D>();
            if (collider != null) collider.enabled = true;
         
            Draggable draggable = newItem.GetComponent<Draggable>();
            if (draggable != null) draggable.enabled = true;

            Destroy(collision.gameObject);
        }
    }
}
