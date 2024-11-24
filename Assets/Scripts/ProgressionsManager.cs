using System;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionsManager : MonoBehaviour
{
    private List<Progression> m_Progressions = new();
    public List<Progression> Progressions { get =>  m_Progressions; }

    private int m_CurrentSelectedProgression = -1;

    public static ProgressionsManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void AddCurrentGuitarPosition()
    {
        var tuning = GuitarManager.Instance.Tuning;
        var position = GuitarManager.Instance.CurrentPosition;

        if (m_CurrentSelectedProgression == -1)
        {
            m_Progressions.Add(new("New Progression", tuning));
            m_CurrentSelectedProgression = m_Progressions.Count - 1;
        }
        else
        {
            if (m_Progressions[m_CurrentSelectedProgression].Tuning == tuning)
            {
                if (!m_Progressions[m_CurrentSelectedProgression].Positions.Contains(position))
                {
                    m_Progressions[m_CurrentSelectedProgression].Positions.Add(position);
                }
                else
                {
                    UIManager.Instance.ShowMessage("This position has already been added to the current progression.", "Continue");
                }
            }
            else
            {
                UIManager.Instance.ShowMessage("The current tuning doesn't match the tuning of the current progression.", "Continue");
            }
        }
    }

    public void CreateProgression(string name)
    {
        m_Progressions.Add(new(name, GuitarManager.Instance.Tuning));
    }
}
