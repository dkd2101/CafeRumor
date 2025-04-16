using System.Collections;
using System.Collections.Generic;
using GameState;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Legit just checks if we have completed the rumor and shows the credits if we have.
 */
public class ShowCredits : MonoBehaviour
{
    private bool started = false;
    void Update()
    {
        if (!started && GameStateManager.getInstance().isRumorComplete("DogOnTheHighway"))
        {
            GameStateManager.getInstance().resetGameData();
            StartCoroutine(GoToCredits());
        }
    }

    IEnumerator GoToCredits()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("CreditsScene");
    }
}
