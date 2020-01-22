using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    [SerializeField]
    private List<Button> barBtnList;
    //[SerializeField]
    //private List<Button> itemBtnList;
    [SerializeField]
    private List<List<Button>> itemsMgrList;
    [SerializeField]
    private List<GameObject> panelList;
    
    private GameObject playerContainer;


    private void Awake()
    {
        Button[] barGroup = GameObject.Find("Canvas/Background/LeftCenterPanel").GetComponentsInChildren<Button>();
        barBtnList = new List<Button>(barGroup);

        itemsMgrList = new List<List<Button>>();

        panelList = new List<GameObject>();

        GameObject[] panelGroup = GameObject.FindGameObjectsWithTag("SubItem");
        for (int i = 0; i < panelGroup.Length; i++)
        {
            panelList.Add(panelGroup[i]);
            panelGroup[i].SetActive(false);

            Button[] itemGroup = panelGroup[i].GetComponentsInChildren<Button>();
            List<Button> itemBtnList = new List<Button>(itemGroup);
            itemsMgrList.Add(itemBtnList);
        }

        playerContainer = GameObject.Find("Canvas/Background/RightCenterPanel/PlayerContainer");
    }

    private void Start()
    {
        foreach(Button btn in barBtnList)
        {
            btn.onClick.AddListener(() => ActivePanel(btn));
        }
        for(int i = 0; i < itemsMgrList.Count(); i++)
        {
            // Avoid closure trap.
            int index = i;
             foreach(Button item in itemsMgrList[i])
             {
                        item.onClick.AddListener(() => toggleInnerText(item, index));
             }
        }
    }

    
    private void ActivePanel(Button btn)
    {
        playerContainer.SetActive(false);
        for (int i = 0; i < barBtnList.Count(); i++)
        {
            bool isSelect = (barBtnList[i] == btn);
            barBtnList[i].interactable = !isSelect;
            if (isSelect)
            {
                // Show the panel related to the current clicked button.
                panelList[i].SetActive(true);
            }
            for(int j = 0; j < panelList.Count(); j++)
            {
                // If not related.
                if (j != i && isSelect)
                {
                    // Hide the panel that not related to the current clicked button.
                    panelList[j].SetActive(false);
                }
            }
        }
    }

    private void toggleInnerText(Button btn, int index)
    {
        Text itemText = btn.transform.Find("Text").GetComponent<Text>();
        //print(itemText.text.ToString());
        Text innerText = barBtnList[index].transform.Find("DynamicText").GetComponent<Text>();
        innerText.text = itemText.text.ToString();

        if(index == 2)
        {
            innerText.text = itemText.text.ToString().Substring(0, 4);
        }

        panelList[index].SetActive(false);
        playerContainer.SetActive(true);
    }

}
    