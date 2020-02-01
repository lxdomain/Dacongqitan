using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that handles utilities.
/// <summary>
public class Utilities
{
    public class Card
    {
        protected int id;
        protected string name;
        protected string description;
        public Card(int id,string name,string description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }
    }

    public class MarionetteCard : Card
    {
        protected int typeID;
        //protected int typeName;
        public MarionetteCard(int id,string name,string description) : base(id, name, description)
        {
            this.typeID = 1;
        }
    }

    public class DrawingCard : Card
    {
        protected int typeID;
        //protected int typeName;
        public DrawingCard(int id, string name, string description) : base(id, name, description)
        {
            this.typeID = 2;
        }
    }

    public class GoodCard : Card
    {
        protected int typeID;
        //protected int typeName;
        public GoodCard(int id,string name, string description) : base(id, name, description)
        {
            this.typeID = 3;
        }
    }

    /**********************The following variables will be used in CharacterSelector&CharacterMonitor.*****************************************/

    public static bool entranceIsModified = false;
    public static int entranceID;
    public static string entranceName;
    public static int stageID;
    public static int PRESET_STAGE;
    /************************************************THE END********************************************************************/

    public static Dictionary<string, string> resMap;
    public static int MarionetteCardNum = 32;
    public static int DrawingCardNum = 28;
    public static int GoodCardNum = 23;
    public static MarionetteCard[] MarionetteCardList;
    public static DrawingCard[] DrawingCardList;
    public static GoodCard[] GoodCardList;
    public static Dictionary<int, MarionetteCard> MarionetteCardMap;
    public static Dictionary<int, DrawingCard> DrawingCardMap;
    public static Dictionary<int, GoodCard> GoodCardMap;
    public static string res_folder_path_figure = "Images/Figures/";
    public static string res_folder_path_mask = "Images/Masks/";
    public static string res_folder_path_cards = "Images/Cards/";
    public static string res_folder_path_cards_org = "Images/Cards_org/";

    public static void InitResMap()
    {
        resMap = new Dictionary<string, string>();
        resMap.Add("原天柿", "yts");
        resMap.Add("囡囡", "nn");
        resMap.Add("外公", "wg");
        resMap.Add("二大爷", "edy");
        resMap.Add("七舅", "qj");
        resMap.Add("花栗子", "hlz");
        resMap.Add("冬白", "db");
        resMap.Add("闪电", "sd");
    }

    public static void InitCardMap()
    {
        MarionetteCardList = new MarionetteCard[MarionetteCardNum];
        DrawingCardList = new DrawingCard[DrawingCardNum];
        GoodCardList = new GoodCard[GoodCardNum];

        MarionetteCardList[0] = new MarionetteCard(0, "风晴雪 ", "派遣时，每3回合附近2格地块内的己方人偶工具数+1");
        MarionetteCardList[1] = new MarionetteCard(1, "方兰生 ", "派遣时，经过此场景的玩家只可再前进1格地块");
        MarionetteCardList[2] = new MarionetteCard(2, "阿阮 ", "退场时，拥有者立即获得卡牌露草");
        MarionetteCardList[3] = new MarionetteCard(3, "沈曦 ", "派遣时，每3回合工具数、工具锋利度、工具牢固度均恢复到初始状态");
        MarionetteCardList[4] = new MarionetteCard(4, "巽芳 ", "退场时，拥有者立即获得卡牌欧阳少恭");
        MarionetteCardList[5] = new MarionetteCard(5, "襄铃 ", "比试时，扑卖阶段可投掷12枚铜钱");
        MarionetteCardList[6] = new MarionetteCard(6, "乐无异 ", "驻守时，每5回合场景规模+1级");
        MarionetteCardList[7] = new MarionetteCard(7, "谢衣 ", "驻守时，场内扩建所需的大葱-50%");
        MarionetteCardList[8] = new MarionetteCard(8, "禺期 ", "退场时，拥有者立即获得卡牌无名之剑");
        MarionetteCardList[9] = new MarionetteCard(9, "悭臾 ", "派遣时，每5回合其所在场景及附近2格地块内的场景土木值-5");
        MarionetteCardList[10] = new MarionetteCard(10, "红玉 ", "比试时必定先手");
        MarionetteCardList[11] = new MarionetteCard(11, "尹千觞 ", "退场时，随机进入场上一名玩家的手牌");
        MarionetteCardList[12] = new MarionetteCard(12, "闻人羽 ", "驻守百草谷时，工具锋利度+1，工具牢固度+1");
        MarionetteCardList[13] = new MarionetteCard(13, "欧阳少恭 ", "退场时，所有场景土木值-5");
        MarionetteCardList[14] = new MarionetteCard(14, "清和真人 ", "位于太华山时，每3回合大葱+100.与夏夷则比试时，工具锋利度-1");
        MarionetteCardList[15] = new MarionetteCard(15, "夏夷则 ", "退场时，有50%概率回到己方手牌，且工具数上限-3");
        MarionetteCardList[16] = new MarionetteCard(16, "百里屠苏 ", "工具数少于5时，工具锋利度+1");
        MarionetteCardList[17] = new MarionetteCard(17, "沈夜 ", "派遣时，每3回合工具锋利度+1，工具牢固度+1，工具数-1");
        MarionetteCardList[18] = new MarionetteCard(18, "陵越 ", "驻守天墉城时，工具锋利度+1.工具牢固度+1");
        MarionetteCardList[19] = new MarionetteCard(19, "华月 ", "退场时，拥有者立即获得卡牌华月的箜篌");
        MarionetteCardList[20] = new MarionetteCard(20, "北洛 ", "比试时，若一回合内无人偶退场，则再比试一回合");
        MarionetteCardList[21] = new MarionetteCard(21, "云无月 ", "派遣时，所有玩家每3回合大葱+100");
        MarionetteCardList[22] = new MarionetteCard(22, "岑缨 ", "奇数回合时，回避其他人偶的比试请求");
        MarionetteCardList[23] = new MarionetteCard(23, "姬轩辕 ", "驻守时，场景规模+1级，退场时，场景规模-1级");
        MarionetteCardList[24] = new MarionetteCard(24, "缙云 ", "比试结束时，若没有退场，则工具锋利度+1");
        MarionetteCardList[25] = new MarionetteCard(25, "巫炤 ", "退场时，所有玩家随机抽取1个厄运");
        MarionetteCardList[26] = new MarionetteCard(26, "司危 ", "比试结束时，若没有退场，则工具数+1");
        MarionetteCardList[27] = new MarionetteCard(27, "玄戈&霓商 ", "驻守天鹿城时，工具锋利度+1，工具锋利度+1，工具牢固度+1，工具数+1");
        MarionetteCardList[28] = new MarionetteCard(28, "嫘祖 ", "驻守西陵时，工具锋利度+1，工具牢固度+1");
        MarionetteCardList[29] = new MarionetteCard(29, "刘兄 ", "派遣时，每3回合从牌组中抽取1张卡牌");
        MarionetteCardList[30] = new MarionetteCard(30, "馋鸡 ", "工具数大于1时，比试过程中工具被一次性全毁后，会重新获得1个工具");
        MarionetteCardList[31] = new MarionetteCard(31, "阿翔 ", "比试时，有50%的概率避免工具受损");

        DrawingCardList[0] = new DrawingCard(0, "琴川 ", "关联人偶：方兰生");
        DrawingCardList[1] = new DrawingCard(1, "长安 ", "关联人偶：乐无异");
        DrawingCardList[2] = new DrawingCard(2, "天墉城 ", "关联人偶：百里屠苏、陵越");
        DrawingCardList[3] = new DrawingCard(3, "百草谷 ", "关联人偶：闻人羽");
        DrawingCardList[4] = new DrawingCard(4, "巫山 ", "关联人偶：阿阮");
        DrawingCardList[5] = new DrawingCard(5, "蓬莱 ", "关联人偶：巽芳、欧阳少恭");
        DrawingCardList[6] = new DrawingCard(6, "青玉坛 ", "关联人偶：欧阳少恭");
        DrawingCardList[7] = new DrawingCard(7, "紫榕林 ", "关联人偶：襄铃");
        DrawingCardList[8] = new DrawingCard(8, "榣山 ", "关联人偶：悭臾、欧阳少恭");
        DrawingCardList[9] = new DrawingCard(9, "太华山 ", "关联人偶：清和真人、夏夷则");
        DrawingCardList[10] = new DrawingCard(10, "幽都 ", "关联人偶：风晴雪、尹千觞");
        DrawingCardList[11] = new DrawingCard(11, "流月城 ", "关联人偶：沈曦、谢衣、沈夜、华月");
        DrawingCardList[12] = new DrawingCard(12, "桃花谷 ", "关联人偶：风晴雪、百里屠苏");
        DrawingCardList[13] = new DrawingCard(13, "静水湖 ", "关联人偶：谢衣");
        DrawingCardList[14] = new DrawingCard(14, "安陆 ", "关联人偶：红玉");
        DrawingCardList[15] = new DrawingCard(15, "神女墓 ", "关联人偶：阿阮");
        DrawingCardList[16] = new DrawingCard(16, "鄢陵 ", "关联人偶：岑缨");
        DrawingCardList[17] = new DrawingCard(17, "天鹿城 ", "关联人偶：北洛、玄戈&霓商");
        DrawingCardList[18] = new DrawingCard(18, "西陵 ", "关联人偶：巫炤、司危、嫘祖");
        DrawingCardList[19] = new DrawingCard(19, "有熊 ", "关联人偶：姬轩辕、缙云");
        DrawingCardList[20] = new DrawingCard(20, "白梦泽 ", "关联人偶：云无月、缙云、北洛");
        DrawingCardList[21] = new DrawingCard(21, "巫之国 ", "关联人偶：北洛、刘兄");
        DrawingCardList[22] = new DrawingCard(22, "鼎湖 ", "关联人偶：姬轩辕、缙云、嫘祖");
        DrawingCardList[23] = new DrawingCard(23, "赤水 ", "关联人偶：姬轩辕、嫘祖、悭臾");
        DrawingCardList[24] = new DrawingCard(24, "阳平 ", "关联人偶：刘兄");
        DrawingCardList[25] = new DrawingCard(25, "无名之地 ", "关联人偶：巫炤、司危");
        DrawingCardList[26] = new DrawingCard(26, "红叶湖 ", "关联人偶：襄铃、百里屠苏");
        DrawingCardList[27] = new DrawingCard(27, "捐毒 ", "关联人偶：乐无异、谢衣、沈夜");

        GoodCardList[0] = new GoodCard(0, "昭明 ", "指定一名人偶工具锋利度+1");
        GoodCardList[1] = new GoodCard(1, "露草 ", "使用后立即抽2张牌");
        GoodCardList[2] = new GoodCard(2, "凤来 ", "指定一名玩家原地停留1回合");
        GoodCardList[3] = new GoodCard(3, "黑龙鳞片 ", "指定一名玩家随机损失2张牌");
        GoodCardList[4] = new GoodCard(4, "古剑焚寂 ", "指定一名人偶工具锋利度+1，若对象为百里屠苏，则+2");
        GoodCardList[5] = new GoodCard(5, "古剑红玉 ", "指定一名人偶工具锋利度+1，若对象为红玉，则+2");
        GoodCardList[6] = new GoodCard(6, "青玉司南佩 ", "使用后免除下一次厄运");
        GoodCardList[7] = new GoodCard(7, "焦炭 ", "指定一名人偶工具数-5");
        GoodCardList[8] = new GoodCard(8, "通天之器 ", "通天之器");
        GoodCardList[9] = new GoodCard(9, "无名之剑 ", "指定一名人偶工具锋利度+1，若对象为乐无异，则+2");
        GoodCardList[10] = new GoodCard(10, "金麒麟 ", "免除下一次借宿葱");
        GoodCardList[11] = new GoodCard(11, "兔子抱枕 ", "指定一名人偶工具数+5");
        GoodCardList[12] = new GoodCard(12, "古剑晗光 ", "指定一名人偶工具锋利度+1，若对象为乐无异，则+2");
        GoodCardList[13] = new GoodCard(13, "百胜刀 ", "指定一处场景土木值-5");
        GoodCardList[14] = new GoodCard(14, "忘川 ", "指定一名人偶工具锋利度+1，若对象为谢衣，则+2");
        GoodCardList[15] = new GoodCard(15, "华月箜篌 ", "指定一名场上的人偶，回到其拥有者的手牌中");
        GoodCardList[16] = new GoodCard(16, "太岁 ", "指定一名人偶工具锋利度+1，若对象为缙云或北洛，则+2");
        GoodCardList[17] = new GoodCard(17, "画板 ", "指定一处场景土木值+5");
        GoodCardList[18] = new GoodCard(18, "半魂莲 ", "将下一次遭遇到的厄运嫁祸给指定玩家，此效果持续5回合");
        GoodCardList[19] = new GoodCard(19, "玉梳 ", "指定一名人偶工具牢固度+2，若对象为司危，则+3");
        GoodCardList[20] = new GoodCard(20, "梦魂枝 ", "指定下一次扑卖点数");
        GoodCardList[21] = new GoodCard(21, "青丘尘中记 ", "指定一名玩家与自己平分葱，若刘兄在手牌中，多获得10%葱");
        GoodCardList[22] = new GoodCard(22, "天鹿 ", "指定一名人偶工具锋利度+1，若对象为北洛或玄戈&霓商，则+2");

        MarionetteCardMap = new Dictionary<int, MarionetteCard>();
        DrawingCardMap = new Dictionary<int, DrawingCard>(); 
        GoodCardMap = new Dictionary<int, GoodCard>();
        for(int i = 0; i < MarionetteCardNum; i++)
        {
            //MarionetteCardMap.Add(MarionetteCardList[i]);
        }
        for (int i = 0; i < DrawingCardNum; i++)
        {
            //DrawingCardMap.Add(DrawingCardList[i]);
        }
        for (int i = 0; i < GoodCardNum; i++)
        {
            //GoodCardMap.Add(GoodCardList[i]);
        }
    }
}
