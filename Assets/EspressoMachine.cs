using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspressoMachine : MonoBehaviour
{
    public GameObject prefab;
    public Transform spawnPoint;

    public List<GameObject> inBetweens;
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
        else if (collision.gameObject.name == "Cup")
        {
            collision.gameObject.GetComponent<Drinks>().AddEspresso();
            collision.gameObject.transform.position = new Vector3(0,0,0);
            StartCoroutine("PlayInBetweens");
        }
    }

    public IEnumerator PlayInBetweens() {
        foreach(GameObject frame in inBetweens) {
            frame.SetActive(true);
            yield return new WaitForSeconds(1f);
        }

        foreach(GameObject frame in inBetweens) {
            frame.SetActive(false);
        }
    }
}
