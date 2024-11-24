using System.Collections.Generic;
using UnityEngine;

public class ProgressionsManager : MonoBehaviour
{
    private List<Progression> m_Progressions = new();
    public List<Progression> Progressions { get =>  m_Progressions; }

    public static ProgressionsManager Instance;

    private void Awake()
    {
        Instance = this;

        m_Progressions.Add(new());
    }
}
