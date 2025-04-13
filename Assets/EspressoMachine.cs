using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspressoMachine : MonoBehaviour
{
    public List<GameObject> inBetweens;
    public void OnTriggerEnter2D(Collider2D collision)
    {  
        if (collision.gameObject.name == "Cup")
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
