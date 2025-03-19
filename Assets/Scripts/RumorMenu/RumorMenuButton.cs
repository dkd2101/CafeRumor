using System.Collections;
using System.Collections.Generic;
using GameState;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RumorMenuButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RumorData rumorData;

    [SerializeField] private Color hoverColor;

    private RumorMenuScript menu;
    private Color normalColor;

    public void OnPointerClick(PointerEventData eventData)
    {
        this.menu.SetDisplayedRumor(rumorData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.GetComponent<Image>().color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponent<Image>().color = normalColor;
    }

    // Start is called before the first frame update
    void Start()
    {
        menu = FindObjectOfType<RumorMenuScript>();
        normalColor = GetComponent<Image>().color;
    }

}
