using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class TimeCycleUIManager : MonoBehaviour
{
    // upon reaching a specific scene, the scene should fade and the user should be prompted to choose what todo
    // time should pause during conversation or actions
    // 

    [SerializeField] private GameObject actionMenu;
    [SerializeField] private Button cafeButton;
    [SerializeField] private Button townButton;
    [SerializeField] private string cafeScene;
    [SerializeField] private string townScene;
    [SerializeField] private GameObject fadeImage;

    private TimeManager timeManager;

    // Start is called before the first frame update
    void Start()
    {
        cafeButton.onClick.AddListener(OnCafeClicked);
        townButton.onClick.AddListener(OnTownClicked);
        timeManager = FindObjectOfType<TimeManager>();
        fadeImage.SetActive(false);
        actionMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeManager.TimeOfDay.PartOfDay == GameTime.PartOfDay.DAYTIME
            && timeManager.TimeOfDay.Hours >= 17
            && timeManager.TimeOfDay.Minutes >= 0)
        {
            this.PromptWithAction();
        }
        else if (timeManager.TimeOfDay.PartOfDay == GameTime.PartOfDay.NIGHTTIME
                && timeManager.TimeOfDay.Hours >= 23
                && timeManager.TimeOfDay.Minutes >= 0)
        {
            PromptWithAction();
        }
    }

    // display the menu for deciding whether or not to open the cafe or explore the town
    public void PromptWithAction()
    {
        timeManager.PauseTime(true);
        actionMenu.SetActive(true);
        actionMenu.GetComponent<Fader>().StartFadeIn();
    }

    // callback function for when the cafe button is clicked
    public void OnCafeClicked()
    {
        Debug.Log("Cafe Selected");
        timeManager.GoToNextDay();
        fadeImage.SetActive(true);
        fadeImage.GetComponent<Fader>().StartFadeIn();

    }

    // callback function for when the town button is clicked
    public void OnTownClicked()
    {
        Debug.Log("Town Selected");
        timeManager.GoToNextDay();
        fadeImage.SetActive(false);
        fadeImage.GetComponent<Fader>().StartFadeIn();
    }
}
