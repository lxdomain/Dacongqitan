using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{    
    //[SerializeField]
    //private List<Button> modeBtnList;
    [SerializeField]
    private List<Button> selectorBtnList;
    [SerializeField]
    private List<GameObject> activePanelList;
    [SerializeField]
    private List<GameObject> modeSetList;
    [SerializeField]
    private List<GameObject> playermsgList;
    [SerializeField]
    private List<GameObject> preSetList;
    private GameObject characterPanel;
    private Button backBtn;
    private readonly string abs_path = "Canvas/Background/RightCenterPanel/PlayerContainer";
    private readonly string[] rel_paths = { "/Player_01", "/Player_02", "/Player_03", "/Player_04" };

  

    private void Awake()
    {
        //modeBtnList = new List<Button>();
        selectorBtnList = new List<Button>();
        activePanelList = new List<GameObject>();
        modeSetList = new List<GameObject>();
        preSetList = new List<GameObject>();
        GameObject[] panelArr = GameObject.FindGameObjectsWithTag("Configuration");
        foreach (GameObject obj in panelArr)
        {
            activePanelList.Add(obj);
        }
        characterPanel = GameObject.Find("Canvas/Background/CenterPanel");
        characterPanel.SetActive(false);
        backBtn = GameObject.Find("Canvas/Background/TopMask").GetComponentInChildren<Button>();
        backBtn.gameObject.SetActive(false);
        //getBtnsByAbsPath(modeBtnList, "/ModeSet");
        GetBtnsByAbsPath(selectorBtnList, "/Content");
        //for (int i = 0; i < rel_paths.Length; i++)
        //{
        //    Button[] selectorGroup = GameObject.Find(abs_path + rel_paths[i] + "/Content").GetComponentsInChildren<Button>();
        //    for (int j = 0; j < selectorGroup.Length; j++)
        //    {
        //        selectorBtnList.Add(selectorGroup[j]);
        //    }
        //}
        GetGameObjectsByAbsPath(modeSetList, "/ModeSet");
        GetGameObjectsByAbsPath(playermsgList, "/Content");
        GetGameObjectsByAbsPath(preSetList, "/PreSet");
        //for(int i = 0; i < rel_paths.Length; i++)
        //{
        //    GameObject modeSet = GameObject.Find(abs_path + rel_paths[i] + "/ModeSet");
        //    modeSetList.Add(modeSet);
        //}
        Utilities.InitAll();
    }

    private void Start()
    {

        // Binding events for a group of mode set buttons.
        for (int i = 0; i < modeSetList.Count(); i++)
        {
            // Avoid closure trap.
            int index = i;
            foreach (Button btn in modeSetList[i].GetComponentsInChildren<Button>())
            {
                btn.onClick.AddListener(() =>
                {
                    Text mode = modeSetList[index].GetComponentsInChildren<Text>()[1];
                    mode.text = (mode.text == "手动" ? "电脑" : "手动");
                });
            }
        }

        // Binding events for a group of pre set buttons.
        for(int i = 0; i < preSetList.Count(); i++)
        {
            Button[] btnPair = preSetList[i].GetComponentsInChildren<Button>();
            Text cardsNum = preSetList[i].GetComponentsInChildren<Text>()[1];
            string tag = "预设卡组";
            int index = cardsNum.text[cardsNum.text.Length - 1] - '1';
            btnPair[0].onClick.AddListener(() =>
            {
                index = (index+3) % 4;
                cardsNum.text = tag + (index+1);
            });
            btnPair[1].onClick.AddListener(() =>
            {
                index = (index + 1) % 4;
                cardsNum.text = tag + (index+1);
            });
        }

        // Binding events for a group of selector buttons.
        foreach (Button btn in selectorBtnList)
        {
            btn.onClick.AddListener(() => {
                DisablePanels(activePanelList);
                ActiveCharacterPanel(btn);
            });
        }
        // Binding event for the back button.
        backBtn.onClick.AddListener(() =>
        {
            activePanelList[0].GetComponent<Image>().sprite = Resources.Load(Utilities.res_folder_path_mask + "sjsz", typeof(Sprite)) as Sprite;
            //activePanelList[0].transform.Find("Text").GetComponentInChildren<Text>().text = "赛局设置>>>";
            foreach (GameObject panel in activePanelList)
            {
                //  avoid the top mask hidden incorrectly.
                //  avoid the card container actived incorrectly.
                if (panel.name != "TopMask"&&panel.name != "CardContainer")
                {
                        panel.SetActive(!panel.activeSelf);
                }
            }
            backBtn.gameObject.SetActive(false);
            if (Utilities.entranceIsModified)
            {
                UpdateEntrance();
                Utilities.entranceIsModified = false;
            }
            
        });
    }


    private void UpdateEntrance()
    {

        playermsgList[Utilities.entranceID].GetComponentInChildren<Text>().text = Utilities.entranceName;
        Button targetBtn = playermsgList[Utilities.entranceID].GetComponentInChildren<Button>(); 
        targetBtn.GetComponent<Image>().sprite = Resources.Load(Utilities.res_folder_path_figure + Utilities.resMap[Utilities.entranceName], typeof(Sprite)) as Sprite;

        SpriteState sp = new SpriteState();
        Sprite tmpSprite = Resources.Load(Utilities.res_folder_path_figure + Utilities.resMap[Utilities.entranceName]+"_hover", typeof(Sprite)) as Sprite;
        sp.highlightedSprite = tmpSprite;
        sp.pressedSprite = tmpSprite;
        targetBtn.spriteState = sp;
    }



    private void GetBtnsByAbsPath(List<Button> tar,string rel_path)
    {
        for (int i = 0; i < rel_paths.Length; i++)
        {
            Button[] selectorGroup = GameObject.Find(abs_path + rel_paths[i] + rel_path).GetComponentsInChildren<Button>();
            for (int j = 0; j < selectorGroup.Length; j++)
            {
                tar.Add(selectorGroup[j]);
            }
        }
    }

    private void GetGameObjectsByAbsPath(List<GameObject> tar,string rel_path="")
    {
        for (int i = 0; i < rel_paths.Length; i++)
        {
            GameObject modeSet = GameObject.Find(abs_path + rel_paths[i] + rel_path);
            tar.Add(modeSet);
        }
    }

    private void DisablePanels(List<GameObject> panels)
    {
        for(int i = 1; i <panels.Count()-1;i++)
        {
            panels[i].SetActive(false);
        }
    }

    private void ActiveCharacterPanel(Button btn)
    {
        activePanelList[0].GetComponent<Image>().sprite = Resources.Load(Utilities.res_folder_path_mask+"ghjs", typeof(Sprite)) as Sprite;
        //activePanelList[0].transform.Find("Text").GetComponentInChildren<Text>().text = "更换角色>>>";
        activePanelList[activePanelList.Count()-2].SetActive(true);
        backBtn.gameObject.SetActive(true);
    }
}
