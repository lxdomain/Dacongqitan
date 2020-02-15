using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PanelToggle : MonoBehaviour
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
    private Button backBtn;

    private GameObject player03;
    private GameObject player04;

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
        backBtn = GameObject.Find("Canvas/Background/BottomMask/BottomPanel/ButtonBack").GetComponent<Button>();

        player03 = GameObject.Find("Canvas/Background/RightCenterPanel/PlayerContainer/Player_03");
        player04 = GameObject.Find("Canvas/Background/RightCenterPanel/PlayerContainer/Player_04");
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
            int temp_i = i;
             for(int j = 0 ;  j < itemsMgrList[i].Count(); j++)
             {
                    int temp_j = j;
                    itemsMgrList[temp_i][temp_j].onClick.AddListener(() =>
                    {
                        ToggleInnerText(itemsMgrList[temp_i][temp_j], temp_i);

                    });
             }
        }
        backBtn.onClick.AddListener(() =>
        {
            foreach(GameObject go in panelList)
            {
                if(go.activeSelf == true)
                {
                    go.SetActive(false);
                }
            }
            playerContainer.SetActive(true);
        });
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

    private void ToggleInnerText(Button btn, int index)
    {
        Text itemText = btn.transform.Find("Text").GetComponent<Text>();
        //print(itemText.text.ToString());
        Text innerText = barBtnList[index].transform.Find("DynamicText").GetComponent<Text>();
        innerText.text = itemText.text.ToString();
        
        // Special judgement.
        if(index == 0)
        {
            string text = innerText.text;
            switch(text)
            {
                case "2人":
                    player03.SetActive(false);
                    player04.SetActive(false);
                    break;
                case "3人":
                    player03.SetActive(true);
                    player04.SetActive(false);
                    break;
                default:
                    player03.SetActive(true);
                    player04.SetActive(true);
                    break;
            }
            
        }

        // Special judgement.
        if (index == 2)
        {
            innerText.text = itemText.text.ToString().Substring(0, 4);
        }

        panelList[index].SetActive(false);
        playerContainer.SetActive(true);
    }

}
    