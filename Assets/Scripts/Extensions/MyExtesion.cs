using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Extesion class for Unity.
/// <summary>
public static class MyExtesion
{
    public static T[] GetComponentsInRealChildren<T>(this GameObject go, bool includeInactive = false)
    {
        List<T> TList = new List<T>();
        TList.AddRange(go.GetComponentsInChildren<T>(includeInactive));
        TList.RemoveAt(0);
        return TList.ToArray();
    }
}
