using System.Collections.Generic;
using UnityEngine;

public static class MyExtensions
{
    public static void Swap<T>(this List<T> list, int index1, int index2)
    {
        if (index1 >= 0 && index1 < list.Count && index2 >= 0 && index2 < list.Count)
        {
            // Swap using a temporary variable
            T temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }
        else
        {
            Debug.LogError("Invalid indices.");
        }
    }
}
