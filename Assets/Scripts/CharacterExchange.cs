using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CharacterExchange : MonoBehaviour
{
    [SerializeField]
    private List<Button> selectorBtnList;
    [SerializeField]
    private List<GameObject> activePanelList;
    private GameObject characterPanel;
    private Button backBtn;
    private readonly string abs_path = "Canvas/Background/RightCenterPanel/PlayerContainer";
    private readonly string[] rel_paths = { "/Player_01", "/Player_02", "/Player_03", "/Player_04" };

    private void Awake()
    {
        selectorBtnList = new List<Button>();
        activePanelList = new List<GameObject>();
        GameObject[] panelGroup = GameObject.FindGameObjectsWithTag("Configuration");
        foreach (GameObject obj in panelGroup)
        {
            activePanelList.Add(obj);
        }
        characterPanel = GameObject.Find("Canvas/Background/CenterPanel");
        characterPanel.SetActive(false);
        backBtn = GameObject.Find("Canvas/Background/TopMask").GetComponentInChildren<Button>();
        backBtn.gameObject.SetActive(false);
        for (int i = 0; i < rel_paths.Length; i++)
        {
            GameObject player_01 = GameObject.Find(abs_path + rel_paths[i]);
            Button[] selectorGroup = GameObject.Find(abs_path + rel_paths[i] + "/Content").GetComponentsInChildren<Button>();
            for (int j = 0; j < selectorGroup.Length; j++)
            {
                selectorBtnList.Add(selectorGroup[j]);
            }
        }
    }

    private void Start()
    {
        foreach (Button btn in selectorBtnList)
        {
            btn.onClick.AddListener(() => {
                DisablePanels(activePanelList);
                ActiveCharacterPanel(btn);
            });
        }
        backBtn.onClick.AddListener(() =>
        {
            activePanelList[0].transform.Find("Text").GetComponentInChildren<Text>().text = "赛局设置>>>";
            foreach (GameObject panel in activePanelList)
            {
                panel.SetActive(!panel.activeSelf);
            }
            backBtn.gameObject.SetActive(false);
        });
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
        activePanelList[0].transform.Find("Text").GetComponentInChildren<Text>().text = "更换角色>>>";
        activePanelList[activePanelList.Count()-1].SetActive(true);
        backBtn.gameObject.SetActive(true);
    }
}
