using System.ComponentModel;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject panel;

    public Item ShoyuPork;

    public void OnSelectShoyu()
    {
        UnityEngine.Debug.Log("Shoyu selected!");
        panel.SetActive(false);
        SceneManager.LoadScene("Shoyu Pork");
        InventorySystem.Instance.AddItem(ShoyuPork);
    }

    public void OnSelectCoffee()
    {
        UnityEngine.Debug.Log("Coffee selected!");
        panel.SetActive(false);
    }
}