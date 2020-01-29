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
    private List<GameObject> layerList;

    private void Awake()
    {
        presetBtn = GameObject.Find("Canvas/Background/BottomMask/BottomPanel/ButtonPrep").GetComponent<Button>();
        layerList = new List<GameObject>();
    }

    private void Start()
    {
        extractLayers();
        BindingEvent();
    }

    private void extractLayers()
    {
        GameObject[] layerArr = GameObject.FindGameObjectsWithTag("Configuration");
        foreach (GameObject layer in layerArr)
        {
            layerList.Add(layer);
        }
    }

    private void hideAllLayers()
    {
        foreach(GameObject layer in layerList)
        {
            layer.SetActive(false);
        }
    }
    private void BindingEvent()
    {
        presetBtn.onClick.AddListener(() =>
        {
            hideAllLayers();
        });;
    }
}
