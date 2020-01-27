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
    [SerializeField]
    private Button[] playerbtns;
    [SerializeField]
    private GameObject[] playermsgs;
    [SerializeField]
    private Button[] charbtns;
    private readonly string abs_path_player = "Canvas/Background/RightCenterPanel/PlayerContainer";
    private readonly string[] rel_paths_player = { "/Player_01", "/Player_02", "/Player_03", "/Player_04" };
    private readonly string abs_path_char = "Canvas/Background/CenterPanel";
    private readonly string[] rel_paths_char = {"/Character_01", "/Character_02" , "/Character_03" , "/Character_04" ,
    "/Character_05","/Character_06","/Character_07","/Character_08"};
    private void Awake()
    {
        playercolors = new Color[4];
        ColorUtility.TryParseHtmlString("#ECDF3E", out selectcolor);
        playerbtns = new Button[4];
        playermsgs = new GameObject[4];
        charbtns = new Button[8];
    }

    private void Start()
    {
        extractColor();
        extractPlayerBtn();
        extractPlayerMsg();
        extractCharacterBtn();
        bindingEvent();
    }

    private void extractColor()
    {
        for (int i = 0; i < playercolors.Length; i++)
        {
            Text asnText = GameObject.Find(abs_path_player + rel_paths_player[i] + "/AbstractName").GetComponent<Text>();
            playercolors[i] = asnText.color;
        }
    }

    private void extractPlayerBtn()
    {
        for (int i = 0; i < playerbtns.Length; i++)
        {
            playerbtns[i] = GameObject.Find(abs_path_player + rel_paths_player[i] + "/Content").GetComponentInChildren<Button>();
        }
    }

    private void extractPlayerMsg()
    {
        for (int i = 0; i < playerbtns.Length; i++)
        {
            playermsgs[i] = GameObject.Find(abs_path_player + rel_paths_player[i] + "/Content");
        }
    }

    private void extractCharacterBtn()
    {
        for (int i = 0; i < charbtns.Length; i++)
        {
            charbtns[i] = GameObject.Find(abs_path_char + rel_paths_char[i]).GetComponentInChildren<Button>();
        }
    }

    private void checkCharacter()
    {
        string[] players = { "甲", "乙", "丙", "丁" };
        int num = -1;
        foreach(GameObject pmsg in playermsgs)
        {
            num++;
            name = pmsg.GetComponentInChildren<Text>().text;
            for(int i = 0; i < charbtns.Length; i++)
            {
                // cmsg[0] -> Name cmsg[1] -> Tip cmsg[2] -> Description
                Text[] cmsg = charbtns[i].GetComponentsInChildren<Text>();
                if(name == cmsg[0].text)
                {
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

    private void bindingEvent()
    {
        foreach(Button btn in playerbtns)
        {
            btn.onClick.AddListener(() =>
            {
                checkCharacter();
            });
        }

        foreach(Button btn in charbtns)
        {
            btn.onClick.AddListener(() =>
            {
                print(btn.GetComponentInChildren<Text>().text);
            });
        }
    }
}


