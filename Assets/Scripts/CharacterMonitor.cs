using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is used to monitor character changes in player container. 
/// <summary>
public class CharacterMonitor : MonoBehaviour
{
    [SerializeField]
    private Color[] playercolors;
    [SerializeField]
    private Color selectcolor;
    private Color defaultcolor;
    [SerializeField]
    private Button[] playerbtns;
    [SerializeField]
    private GameObject[] playermsgs;
    private int entrance_id;
    private string entrance_name;
    [SerializeField]
    private Button[] charbtns;
    //private Button backBtn;
    private readonly string abs_path_player = "Canvas/Background/RightCenterPanel/PlayerContainer";
    private readonly string[] rel_paths_player = { "/Player_01", "/Player_02", "/Player_03", "/Player_04" };
    private readonly string abs_path_char = "Canvas/Background/CenterPanel";
    private readonly string[] rel_paths_char = {"/Character_01", "/Character_02" , "/Character_03" , "/Character_04" ,
    "/Character_05","/Character_06","/Character_07","/Character_08"};
    private readonly string[] players = { "甲", "乙", "丙", "丁" };
    private void Awake()
    {
        playercolors = new Color[4];
        ColorUtility.TryParseHtmlString("#ECDF3E", out selectcolor);
        ColorUtility.TryParseHtmlString("#DBDAB0", out defaultcolor);
        playerbtns = new Button[4];
        playermsgs = new GameObject[4];
        charbtns = new Button[8];
        //backBtn = GameObject.Find("Canvas/Background/TopMask").GetComponentInChildren<Button>();
        //backBtn.gameObject.SetActive(false);
    }

    private void Start()
    {
        ExtractColor();
        ExtractPlayerBtn();
        ExtractPlayerMsg();
        ExtractCharacterBtn();
        BindingEvent();
    }

    private void ExtractColor()
    {
        for (int i = 0; i < playercolors.Length; i++)
        {
            Text asnText = GameObject.Find(abs_path_player + rel_paths_player[i] + "/AbstractName").GetComponent<Text>();
            playercolors[i] = asnText.color;
        }
    }

    private void ExtractPlayerBtn()
    {
        for (int i = 0; i < playerbtns.Length; i++)
        {
            playerbtns[i] = GameObject.Find(abs_path_player + rel_paths_player[i] + "/Content").GetComponentInChildren<Button>();
        }
    }

    private void ExtractPlayerMsg()
    {
        for (int i = 0; i < playerbtns.Length; i++)
        {
            playermsgs[i] = GameObject.Find(abs_path_player + rel_paths_player[i] + "/Content");
        }
    }

    private void ExtractCharacterBtn()
    {
        for (int i = 0; i < charbtns.Length; i++)
        {
            charbtns[i] = GameObject.Find(abs_path_char + rel_paths_char[i]).GetComponentInChildren<Button>();
        }
    }

    private void CheckCharacter()
    {
        int num = -1;
        foreach(GameObject pmsg in playermsgs)
        {
            num++;
            string name = pmsg.GetComponentInChildren<Text>().text;
            for(int i = 0; i < charbtns.Length; i++)
            {
                // cimg[0] -> Button cimg[1] -> Image
                Image[] cimg = charbtns[i].GetComponentsInChildren<Image>();
                // cmsg[0] -> Name cmsg[1] -> Tip cmsg[2] -> Description
                Text[] cmsg = charbtns[i].GetComponentsInChildren<Text>();
                if(name == cmsg[0].text)
                {
                    cimg[1].sprite = Resources.Load(Utilities.res_folder_path_figure+cmsg[0].text+"_hover", typeof(Sprite)) as Sprite;
                    cmsg[0].color = selectcolor;
                    cmsg[1].color = playercolors[num];
                    cmsg[1].text = "当前玩家" + players[num] + "控制";
                    cmsg[2].color = selectcolor;
                    //charbtns[i].enabled = false;
                    charbtns[i].interactable = false;
                    break;
                }
            }
        }
    }

    private void CheckEntrance(int index)
    {
        entrance_id = index;
        entrance_name = playermsgs[index].GetComponentInChildren<Text>().text;
        //print(entrance_name);
    }

    private void LightenSingleCharacterByIndex(int index)
    {
        Image[] cimg = charbtns[index].GetComponentsInChildren<Image>();
        Text[] cmsg = charbtns[index].GetComponentsInChildren<Text>();

        cimg[1].sprite = Resources.Load(Utilities.res_folder_path_figure + cmsg[0].text+"_hover", typeof(Sprite)) as Sprite;
        cmsg[0].color = selectcolor;
        cmsg[1].color = playercolors[entrance_id];
        cmsg[1].text = "当前玩家" + players[entrance_id] + "控制";
        cmsg[2].color = selectcolor;
        charbtns[index].interactable = false;

        UpdateEntranceName(cmsg[0].text);
    }
    private void UpdateEntranceName(string modified_name)
    {
        entrance_name = modified_name;
    }

 

    private void ExtinctSingleCharacterByIndex(int index)
    {
        for(int i = 0; i < charbtns.Length; i++)
        {
            Image[] cimg = charbtns[i].GetComponentsInChildren<Image>();
            Text[] cmsg = charbtns[i].GetComponentsInChildren<Text>();
            //print(i + " " + cmsg[1].color + "->" + playercolors[index]);
            if (cmsg[1].color == playercolors[index])
            {
                //print("ok");
                //print(cmsg[0].text);
                cimg[1].sprite = Resources.Load(Utilities.res_folder_path_figure + cmsg[0].text, typeof(Sprite)) as Sprite;
                cmsg[0].color = defaultcolor;
                cmsg[1].color = Color.white;// Just to distinguish with the players color.
                cmsg[1].text = "";
                cmsg[2].color = defaultcolor;
                charbtns[i].interactable = true;
                break;
            }
        }
    }

    private void BindingEvent()
    {
        for(int i = 0; i < playerbtns.Length; i++)
        {
            int index = i;
            playerbtns[index].onClick.AddListener(() =>
            {
                //entrance_name = btn.GetComponentInParent<GameObject>().GetComponentInChildren<Text>().text;
                CheckEntrance(index);
                CheckCharacter();
            });
        }

        for (int i = 0; i < charbtns.Length; i++)
        {
            int index = i;
            charbtns[index].onClick.AddListener(() =>
            {
                ExtinctSingleCharacterByIndex(entrance_id);
                LightenSingleCharacterByIndex(index);

                Utilities.entranceIsModified = true;
                Utilities.entranceID = entrance_id;
                Utilities.entranceName = entrance_name;
            });
        }

        //backBtn.onClick.AddListener(() =>
        //{
        //    updateEntrance();
        //});
    }
}