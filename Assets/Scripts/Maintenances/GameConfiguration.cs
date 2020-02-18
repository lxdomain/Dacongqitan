using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

/// <summary>
/// This class is used to save configurations for each game.
/// <summary>
public class GameConfiguration
{

    public enum PlayerNumberOptions
    {
        [Description("2人")]
        TWO,
        [Description("3人")]
        THREE,
        [Description("4人")]
        FOUR
    };

    public enum MapTypeOptions
    {
        [Description("原天柿画的")]
        YTS,
        [Description("二大爷画的")]
        EDY,
        [Description("囡囡画的")]
        NN,
        [Description("外公画的")]
        WG
    }

    public enum TargetOptions
    {
        [Description("谁与争葱：场上只剩一位玩家。")]
        SYZC,
        [Description("小试葱锄：30回合后，葱产最多的玩家胜出")]
        XSCC,
        [Description("葱建大赛：60回合后，葱产最多的玩家胜出")]
        CJDS
    }

    public enum InitialFundsOptions
    {
        [Description("2000大葱")]
        TWO_THOUSAND,
        [Description("4000大葱")]
        FOUR_THOUSAND,
        [Description("8000大葱")]
        EIGHT_THOUSAND
    }

    #region properity
    public PlayerNumberOptions PlayerNumber { get; set; }
    public MapTypeOptions MapType { get; set; }
    public TargetOptions Target { get; set; }
    public InitialFundsOptions InitialFunds { get; set; }
    public List<PlayerConfiguration> PlayerList { get; set; }
    #endregion

    private Dictionary<PlayerNumberOptions, int> pnoToInt;

    public GameConfiguration()
    {
        this.PlayerNumber = PlayerNumberOptions.FOUR;
        this.MapType = MapTypeOptions.YTS;
        this.Target = TargetOptions.SYZC;
        this.InitialFunds = InitialFundsOptions.TWO_THOUSAND;
        this.PlayerList = new List<PlayerConfiguration>
        {
            new PlayerConfiguration(
            PlayerConfiguration.AbstractNameOptions.JIA,
            PlayerConfiguration.ModeOptions.MANUAL,
            PlayerConfiguration.NameOptions.YTS,
            PlayerConfiguration.PresetCardsOptions.ONE
            ),
            new PlayerConfiguration(
            PlayerConfiguration.AbstractNameOptions.YI,
            PlayerConfiguration.ModeOptions.MANUAL,
            PlayerConfiguration.NameOptions.NN,
            PlayerConfiguration.PresetCardsOptions.ONE
            ),
            new PlayerConfiguration(
            PlayerConfiguration.AbstractNameOptions.BING,
            PlayerConfiguration.ModeOptions.MANUAL,
            PlayerConfiguration.NameOptions.WG ,
            PlayerConfiguration.PresetCardsOptions.ONE
            ),
            new PlayerConfiguration(
            PlayerConfiguration.AbstractNameOptions.DING,
            PlayerConfiguration.ModeOptions.MANUAL,
            PlayerConfiguration.NameOptions.EDY,
            PlayerConfiguration.PresetCardsOptions.ONE
            ),
        };

        pnoToInt = new Dictionary<PlayerNumberOptions, int>
        {
            { PlayerNumberOptions.FOUR,4},
            {PlayerNumberOptions.THREE,3 },
            {PlayerNumberOptions.TWO,2 }
        };
    }

    public void PrintAll()
    {
        string[] infos = new string[4];
        infos[0] = String.Format("游戏人数 {0}", MyTool.GetEnumDescription(PlayerNumber));
        infos[1] = String.Format("游戏地图 {0}", MyTool.GetEnumDescription(MapType));
        infos[2] = String.Format("本次目标 {0}", MyTool.GetEnumDescription(Target));
        infos[3] = String.Format("初始资金 {0}", MyTool.GetEnumDescription(InitialFunds));
        foreach (string info in infos)
        {
            Debug.Log(info);
        }
        for(int i = 0; i < pnoToInt[PlayerNumber]; i++)
        //foreach(PlayerConfiguration pc in PlayerList)
        {
            PlayerList[i].PrintAll();
        }
    }
}
