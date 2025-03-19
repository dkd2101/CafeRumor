using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    private GameObject tooltip;
    private TextMeshProUGUI tooltipText;

    private void Awake()
    {
        tooltip = GameObject.Find("Tooltip");
        tooltipText = tooltip.GetComponentInChildren<TextMeshProUGUI>();   
    }

    // Update the tooltip text and display it
    public void ShowTooltip(string text)
    {
        tooltipText.text = text;
        tooltip.SetActive(true);
    }

    // Hide the tooltip
    public void HideTooltip()
    {
        tooltip.SetActive(false);
    }
}
