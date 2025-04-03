using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBetweenManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartInBetweens(List<GameObject> inBetweens) {
        StartCoroutine(PlayInBetweens(inBetweens));
    }

    private IEnumerator PlayInBetweens(List<GameObject> inBetweens) {
        foreach(GameObject frame in inBetweens) {
            frame.SetActive(true);
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(1f);
        
        foreach(GameObject frame in inBetweens) {
            frame.SetActive(false);
        }
    }
}
