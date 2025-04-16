using UnityEngine;
using UnityEngine.UI;

public class ImageSwitcher : MonoBehaviour
{
    public Sprite Image1;
    public Sprite Image2;
    public float switchInterval = 2f;

    private UnityEngine.UI.Image imageComponent;
    private float timer;
    private bool showingFirstImage = true;

    void Start()
    {
        imageComponent = GetComponent<Image>();
        imageComponent.sprite = Image1;
        timer = switchInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            showingFirstImage = !showingFirstImage;
            imageComponent.sprite = showingFirstImage ? Image1 : Image2;
            timer = switchInterval;
        }
    }
}
