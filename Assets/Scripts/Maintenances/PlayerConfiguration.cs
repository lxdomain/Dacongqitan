using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is used to save configurations for each player.
/// <summary>
public class PlayerConfiguration
{
    public enum AbstractNameOptions
    {
        [Description("玩家甲")]
        JIA,
        [Description("玩家乙")]
        YI,
        [Description("玩家丙")]
        BING,
        [Description("玩家丁")]
        DING
    }

    public enum ModeOptions
    {
        [Description("电脑")]
        AUTO,
        [Description("手动")]
        MANUAL
    }

    public enum NameOptions
    {
        [Description("原天柿")]
        YTS,
        [Description("囡囡")]
        NN,
        [Description("外公")]
        WG,
        [Description("二大爷")]
        EDY,
        [Description("七舅")]
        QJ,
        [Description("花栗子")]
        HLZ,
        [Description("冬白")]
        DB,
        [Description("闪电")]
        SD
    }

    public enum PresetCardsOptions
    {
        [Description("预设卡组1")]
        ONE,
        [Description("预设卡组2")]
        TWO,
        [Description("预设卡组3")]
        THREE,
        [Description("预设卡组4")]
        FOUR
    }

    #region properity
    public AbstractNameOptions AbstractName { get; set; }
    public ModeOptions Mode { get; set; }
    public NameOptions Name { get; set; }
    public PresetCardsOptions PresetCards { get; set; }
    #endregion

    private static Dictionary<string, AbstractNameOptions> strToAbstractName;
    private static Dictionary<string, ModeOptions> strToMode;
    private static Dictionary<string, NameOptions> strToName;
    private static Dictionary<string, PresetCardsOptions> strToPresetCards;

    public PlayerConfiguration(AbstractNameOptions AbstractName, ModeOptions Mode, NameOptions Name, PresetCardsOptions PresetCards)
    {
        this.AbstractName = AbstractName;
        this.Mode = Mode;
        this.Name = Name;
        this.PresetCards = PresetCards;
    }

    public static void MakeDict()
    {
        strToAbstractName = new Dictionary<string, AbstractNameOptions>
        {
            { "玩家甲",AbstractNameOptions.JIA },
            { "玩家乙",AbstractNameOptions.YI },
            { "玩家丙",AbstractNameOptions.BING },
            { "玩家丁",AbstractNameOptions.DING }
        };

        strToMode = new Dictionary<string, ModeOptions>
        {
             { "电脑",ModeOptions.AUTO },
             { "手动",ModeOptions.MANUAL }
        };

        strToName = new Dictionary<string, NameOptions>
        {
            { "原天柿",NameOptions.YTS },
            { "囡囡",NameOptions.NN },
            { "外公",NameOptions.WG },
            { "二大爷",NameOptions.EDY },
            { "七舅",NameOptions.QJ },
            { "花栗子",NameOptions.HLZ },
            { "冬白",NameOptions.DB },
            { "闪电",NameOptions.SD }
        };

        strToPresetCards = new Dictionary<string, PresetCardsOptions>
        {
             { "预设卡组1",PresetCardsOptions.ONE },
             { "预设卡组2",PresetCardsOptions.TWO },
             { "预设卡组3",PresetCardsOptions.THREE },
             { "预设卡组4",PresetCardsOptions.FOUR }
        };
    }

    public PlayerConfiguration(string[] msg)
    {
        AbstractName = strToAbstractName[msg[0]];
        Mode = strToMode[msg[1]];
        Name = strToName[msg[2]];
        PresetCards = strToPresetCards[msg[3]];
    }

    public void PrintAll()
    {
        string info = String.Format("{0} {1} {2} {3}", MyTool.GetEnumDescription(AbstractName),
            MyTool.GetEnumDescription(Mode),
            MyTool.GetEnumDescription(Name),
            MyTool.GetEnumDescription(PresetCards)
         );
        Debug.Log(info);
    }
}
