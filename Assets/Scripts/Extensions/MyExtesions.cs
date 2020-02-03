using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// My Extesions for class methods in UnityEngine.
/// <summary>
public static class MyExtesions
{
    public static T[] GetComponentsInRealChildren<T>(this GameObject go , bool includeInactive = false)
    {
        List<T> TList = new List<T>();
        TList.AddRange(go.GetComponentsInChildren<T>(includeInactive));
        TList.RemoveAt(0);
        return TList.ToArray();
    }
}
