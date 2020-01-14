using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    [SerializeField]
    private List<Button> btnList;
    [SerializeField]
    private Button btn1;
    [SerializeField]
    private Button btn2;
    [SerializeField]
    private Button btn3;
    [SerializeField]
    private Button btn4;
    [SerializeField]
    private List<GameObject> viewObjects;

    private void Awake()
    {
        btnList = new List<Button>();
        viewObjects = new List<GameObject>();
        Button[] btnGroup = GameObject.Find("Canvas/Background/LeftCenterPanel").GetComponentsInChildren<Button>();
        for (int i = 0; i < btnGroup.Length; i++)
        {
            btnList.Add(btnGroup[i]);
        }
        Debug.Log(btnGroup.Length);
        GameObject[] panelGroup = GameObject.FindGameObjectsWithTag("SubItem");
        for (int i = 0; i < panelGroup.Length; i++)
        {
            viewObjects.Add(panelGroup[i]);
            panelGroup[i].SetActive(false);
        }
    }

    void Start()
    {
        foreach(Button btn in btnList)
        {
            btn.onClick.AddListener(() => ActivePanel(btn));
        }
        if (btnList.Count > 0)
        {
            viewObjects[0].SetActive(true);
        }
    }

    
    private void ActivePanel(Button btn)
    {
        for(int i = 0; i < btnList.Count(); i++)
        {
            bool isSelect = (btnList[i] == btn);
            btnList[i].interactable = !isSelect;
            if (isSelect)
            {
                // Show the panel related to the current clicked button.
                viewObjects[i].SetActive(true);
            }
            for(int j = 0; j < viewObjects.Count(); j++)
            {
                // If not related.
                if (j != i && isSelect)
                {
                    // Hide the panel that not related to the current clicked button.
                    viewObjects[j].SetActive(false);
                }
            }
        }
    }
}
