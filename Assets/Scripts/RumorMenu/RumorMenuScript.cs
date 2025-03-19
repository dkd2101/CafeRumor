using System.Collections;
using System.Collections.Generic;
using GameState;
using TMPro;
using UnityEngine;

public class RumorMenuScript : MonoBehaviour
{
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text descriptionText;

    public void SetDisplayedRumor(RumorData data) {
        this.titleText.text = data.name;
        this.descriptionText.text = data.GetCurrentDescription();
    }
}
