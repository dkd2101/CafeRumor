using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CardBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TMP_Text recipeTitle;
    [SerializeField] private float yOffset = 30;
    [SerializeField] private GameObject fadeImage;
    private RectTransform rectTransform;
    private float startingYPos;
    private float curYPos;
    private float targetYPos;
    private RecipeSO recipe;

    // Start is called before the first frame update
    void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        startingYPos = rectTransform.anchoredPosition.y;
        curYPos = rectTransform.anchoredPosition.y;
        targetYPos = rectTransform.anchoredPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (curYPos != targetYPos)
        {
            curYPos = Mathf.MoveTowards(curYPos, targetYPos, 300.0f * Time.deltaTime);
            if (Math.Abs(curYPos - targetYPos) <= 0.01)
            {
                curYPos = targetYPos;
            }
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, curYPos);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("pointer enter");
        Debug.Log(startingYPos);
        targetYPos = startingYPos + yOffset;
        Debug.Log(targetYPos);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("pointer exit");
        targetYPos = startingYPos;
    }

    public void SetRecipeTitle(string recipeName)
    {
        recipeTitle.text = recipeName;
    }

    public void SetRecipeData(RecipeSO recipe)
    {
        this.recipe = recipe;
    }

    public void SetFadeImage(GameObject image) {
        this.fadeImage = image;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        fadeImage.SetActive(true);
        fadeImage.GetComponent<Fader>().StartFadeIn();
        SceneManager.LoadScene(recipe.cookingSceneName);
    }
}
