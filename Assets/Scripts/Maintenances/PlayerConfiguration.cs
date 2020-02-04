using System;
using System.ComponentModel;
using UnityEngine;

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
        [Description("预设牌组1")]
        ONE,
        [Description("预设牌组2")]
        TWO,
        [Description("预设牌组3")]
        THREE,
        [Description("预设牌组4")]
        FOUR
    }

    #region properity
    public AbstractNameOptions AbstractName { get; set; }
    public ModeOptions Mode { get; set; }
    public NameOptions Name { get; set; }
    public PresetCardsOptions PresetCards { get; set; }
    #endregion

    public PlayerConfiguration(AbstractNameOptions AbstractName, ModeOptions Mode, NameOptions Name, PresetCardsOptions PresetCards)
    {
        this.AbstractName = AbstractName;
        this.Mode = Mode;
        this.Name = Name;
        this.PresetCards = PresetCards;
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
