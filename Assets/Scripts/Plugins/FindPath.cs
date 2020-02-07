using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// This class is used to copy the path of a game object quickly when using GameObject.Find() function.
/// <summary>
public class FindPath
{
    public static List<string> filelist = new List<string>();
    public static List<string> deallist = new List<string>();
    public static string filepath;

    // Use this for initialization.
    [MenuItem("GameObject/Copy Path %5", priority = 0)]
    public static void GetPath()
    {
        Clearmemory();
        RecursiveFind(Selection.activeGameObject.gameObject);
        PrintinScreen();
        Clearmemory();
    }
    public static void Clearmemory()
    {
        filelist.Clear();
        deallist.Clear();
    }
    public static void RecursiveFind(GameObject go)
    {
        if (go != null)
        {
            filelist.Add(go.name);
            if (go.transform.parent != null)
            {
                RecursiveFind(go.transform.parent.gameObject);
            }
        }
    }
    public static void PrintinScreen()
    {

        for (int i = filelist.Count - 1; i >= 0; i--)
        {
            string str = filelist[i];
            if (i != 0)
            {
                str += "/";
            }
            deallist.Add(str);
        }
        string showstr = "";
        foreach (var list in deallist)
        {
            showstr += list;

        }
        Debug.Log(showstr);

        // Text will be copied to the shear plate.
        TextEditor te = new TextEditor
        {
            text = showstr
        };
        te.SelectAll();
        te.Copy();
    }
}
