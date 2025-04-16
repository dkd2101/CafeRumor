using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "MainScene";

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}