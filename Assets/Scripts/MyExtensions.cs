using System;
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

    public static List<T> AllDiferringElements<T>(this List<T> list1, List<T> list2)
    {
        List<T> differingElements = new();
        int maxLength = Math.Max(list1.Count, list2.Count);

        for (int i = 0; i < maxLength; i++)
        {
            if (i >= list1.Count || i >= list2.Count || !list1[i].Equals(list2[i]))
            {
                if (i < list1.Count)
                    differingElements.Add(list1[i]);
                if (i < list2.Count)
                    differingElements.Add(list2[i]);
            }
        }

        return differingElements;
    }
}
