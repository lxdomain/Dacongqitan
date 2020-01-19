using UnityEngine;
using UnityEngine.UI;

public class ImageListener : MonoBehaviour
{
    [SerializeField]
    private Image figure;
    [SerializeField]
    private Sprite tempSprite;

    private void Awake()
    {
        figure = GetComponent<Image>();
        tempSprite = figure.sprite;
    }

    private void OnMouseEnter()
    {

        string path = "Images/yts_hover";
        Sprite sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        figure.sprite = sprite;
    }

    private void OnMouseExit()
    {
        figure.sprite = tempSprite;
    }
}
