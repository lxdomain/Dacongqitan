using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is used to monitor cards in preset container. 
/// <summary>
public class CardMonitor : MonoBehaviour
{
    [SerializeField]
    private Button presetBtn;
    [SerializeField]
    private List<GameObject> layerList;

    private void Awake()
    {
        presetBtn = GameObject.Find("Canvas/Background/BottomMask/BottomPanel/ButtonPrep").GetComponent<Button>();
        layerList = new List<GameObject>();
    }

    private void Start()
    {
        extractLayers();
        layerList[4].SetActive(false);
        bindingEvent();
    }

    private void extractLayers()
    {
        GameObject[] layerArr = GameObject.FindGameObjectsWithTag("Configuration");
        foreach (GameObject layer in layerArr)
        {
            layerList.Add(layer);
        }
    }
    private void initInterface()
    {

        layerList[0].SetActive(true);
        layerList[0].GetComponent<Image>().sprite = Resources.Load(Utilities.res_folder_path_mask+"yspz", typeof(Sprite)) as Sprite;
        createBlankCards();
    }

    private void createBlankCards()
    {
        GameObject cardContainer = layerList[4];
        cardContainer.SetActive(true);
        Vector3 bounds = cardContainer.GetComponent<MeshFilter>().mesh.bounds.size;
       
        print(bounds.x* cardContainer.transform.localScale.x);
        print(bounds.y* cardContainer.transform.localScale.y);

        GameObject go = new GameObject();
        go.name = "card 0";
        go.transform.parent = cardContainer.transform;
        go.transform.position = cardContainer.transform.position;
        go.AddComponent<Image>();
        go.GetComponent<Image>().sprite = Resources.Load(Utilities.res_folder_path_cards+"blank", typeof(Sprite)) as Sprite;

    }

    private void hideAllLayers()
    {
        foreach(GameObject layer in layerList)
        {
            layer.SetActive(false);
        }
    }
    private void bindingEvent()
    {
        presetBtn.onClick.AddListener(() =>
        {
            hideAllLayers();
            initInterface();
        });;
    }
}
