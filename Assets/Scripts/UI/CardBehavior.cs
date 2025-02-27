using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text recipeTitle;
    [SerializeField] private float yOffset = 30;
    private RectTransform rectTransform;
    private float startingYPos;
    private float curYPos;
    private float targetYPos;

    // Start is called before the first frame update
    void Start()
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
        targetYPos = startingYPos + yOffset;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("pointer exit");
        targetYPos = startingYPos;
    }

    public void SetRecipeTitle(string recipeName) {
        recipeTitle.text = recipeName;
    }
}
