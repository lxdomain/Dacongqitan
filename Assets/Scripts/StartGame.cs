using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This script is used for the final check of  a game configuration.
/// <summary>
public class StartGame : MonoBehaviour
{
    private Button startBtn;
    private Text playerNumberTxt;
    private Text mapTypeTxt;
    private Text targetTxt;
    private Text initialFundsTxt;

    private string[][] playerStrs;
    private readonly string[] prefix = { "Player_01", "Player_02", "Player_03", "Player_04" };
    private readonly string[] suffix = { "AbstractName", "ModeSet/Text", "Content/Name", "PreSet/Text" };
    private void Awake()
    {
        startBtn = GameObject.Find("Canvas/Background/BottomMask/BottomPanel/ButtonStart").GetComponent<Button>();
        playerNumberTxt = GameObject.Find("Canvas/Background/LeftCenterPanel/Button_01/DynamicText").GetComponent<Text>();
        mapTypeTxt = GameObject.Find("Canvas/Background/LeftCenterPanel/Button_02/DynamicText").GetComponent<Text>(); 
        targetTxt = GameObject.Find("Canvas/Background/LeftCenterPanel/Button_03/DynamicText").GetComponent<Text>();
        initialFundsTxt = GameObject.Find("Canvas/Background/LeftCenterPanel/Button_04/DynamicText").GetComponent<Text>();


        playerStrs = new string[4][];
        for(int i = 0; i < 4; i++)
        {
            playerStrs[i] = new string[4];
        }
    }

    private void Start()
    {
        startBtn.onClick.AddListener(() => {
            string playerNumber = playerNumberTxt.text;
            string mapType = mapTypeTxt.text;
            string target = targetTxt.text;
            string initialFunds = initialFundsTxt.text;
            switch (playerNumber)
            {
                case "2人":
                    Utilities.gc.PlayerNumber = GameConfiguration.PlayerNumberOptions.TWO;
                    break;
                case "3人":
                    Utilities.gc.PlayerNumber = GameConfiguration.PlayerNumberOptions.THREE;
                    break;
                case "4人":
                    Utilities.gc.PlayerNumber = GameConfiguration.PlayerNumberOptions.FOUR;
                    break;
                default:
                    break;
            }

            switch (mapType)
            {
                case "原天柿画的":
                    Utilities.gc.MapType = GameConfiguration.MapTypeOptions.YTS;
                    break;
                case "二大爷画的":
                    Utilities.gc.MapType = GameConfiguration.MapTypeOptions.EDY;
                    break;
                case "囡囡画的":
                    Utilities.gc.MapType = GameConfiguration.MapTypeOptions.NN;
                    break;
                case "外公画的":
                    Utilities.gc.MapType = GameConfiguration.MapTypeOptions.WG;
                    break;
                default:
                    break;
            }

            switch (target)
            {
                case "谁与争葱":
                    Utilities.gc.Target = GameConfiguration.TargetOptions.SYZC;
                    break;
                case "小试葱锄":
                    Utilities.gc.Target = GameConfiguration.TargetOptions.XSCC;
                    break;
                case "葱建大赛":
                    Utilities.gc.Target = GameConfiguration.TargetOptions.CJDS;
                    break;
                default:
                    break;
            }

            switch (initialFunds)
            {
                case "2000大葱":
                    Utilities.gc.InitialFunds = GameConfiguration.InitialFundsOptions.TWO_THOUSAND;
                    break;
                case "4000大葱":
                    Utilities.gc.InitialFunds = GameConfiguration.InitialFundsOptions.FOUR_THOUSAND;
                    break;
                case "8000大葱":
                    Utilities.gc.InitialFunds = GameConfiguration.InitialFundsOptions.EIGHT_THOUSAND;
                    break;
                default:
                    break;
            }

            PlayerConfiguration.MakeDict();
            for(int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    playerStrs[i][j] = GameObject.Find("Canvas/Background/RightCenterPanel/PlayerContainer/"+prefix[i]+"/"+suffix[j]).GetComponent<Text>().text;
                }
                Utilities.gc.PlayerList[i] = new PlayerConfiguration(playerStrs[i]);
            }
            //Utilities.gc.PrintAll();
            SceneManager.LoadScene("MainScene");
        });
    }
}
