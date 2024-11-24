using System;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionsManager : MonoBehaviour
{
    private List<Progression> m_Progressions = new();
    public List<Progression> Progressions { get =>  m_Progressions; }

    private int m_CurrentSelectedProgressionIndex = -1;
    public int CurrentSelectedProgressionIndex 
    { 
        get => m_CurrentSelectedProgressionIndex; 
        set
        {
            m_CurrentSelectedProgressionIndex = value;
            OnCurrentSelectedProgressionIndexChanged?.Invoke(m_CurrentSelectedProgressionIndex);
        }
    }

    public event Action<int> OnCurrentSelectedProgressionIndexChanged;

    public static ProgressionsManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void AddCurrentGuitarPosition()
    {
        if (m_CurrentSelectedProgressionIndex == -1) return;

        var tuning = GuitarManager.Instance.Tuning;
        var position = GuitarManager.Instance.CurrentPosition;

        if (m_Progressions[m_CurrentSelectedProgressionIndex].Tuning == tuning)
        {
            if (!m_Progressions[m_CurrentSelectedProgressionIndex].Positions.Contains(position))
            {
                m_Progressions[m_CurrentSelectedProgressionIndex].Positions.Add(position);

                UIManager.Instance.ShowNotification($"Added to '{m_Progressions[m_CurrentSelectedProgressionIndex].Name}'");
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

    public void CreateProgression(string name)
    {
        m_Progressions.Add(new(name, GuitarManager.Instance.Tuning));
        CurrentSelectedProgressionIndex = m_Progressions.Count - 1;
    }
}
