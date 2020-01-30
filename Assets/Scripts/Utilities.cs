using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that handles utilities.
/// <summary>
public class Utilities
{
    /**********************The following variables will be used in CharacterSelector&CharacterMonitor.*****************************************/

    public static bool entranceIsModified = false;
    public static int entranceID;
    public static string entranceName;
    /************************************************THE END********************************************************************/
    public static Dictionary<string, string> resMap;
    public static string res_folder_path_figure = "Images/Figures/";
    public static string res_folder_path_mask = "Images/Masks/";
    public static string res_folder_path_cards = "Images/Cards/";
    public static void initResMap()
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
}
