using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ProgressionsManager : MonoBehaviour
{
    private const string k_ProgressionsFileName = "Progressions.txt";

    private List<Progression> m_Progressions = new();
    public List<Progression> Progressions { get =>  m_Progressions; }

    private int m_CurrentSelectedProgressionIndex = -1;
    public int CurrentSelectedProgressionIndex 
    { 
        get => m_CurrentSelectedProgressionIndex; 
        set
        {
            m_CurrentSelectedProgressionIndex = value;

            if (m_CurrentSelectedProgressionIndex >= 0)
            {
                GuitarManager.Instance.SetTuning(CurrentProgression.Tuning);

                if (GuitarManager.Instance.CapoPosition > 0 && CurrentProgression.CapoPosition == 0)
                {
                    GuitarManager.Instance.RemoveCapo();
                }
                else
                {
                    GuitarManager.Instance.UpdateCapoPosition(CurrentProgression.CapoPosition);
                }

                GuitarManager.Instance.SetCurrentPositionToAllOpen();
            }

            OnCurrentSelectedProgressionIndexChanged?.Invoke(m_CurrentSelectedProgressionIndex);
        }
    }

    public Progression CurrentProgression { get => m_Progressions[m_CurrentSelectedProgressionIndex]; }

    public event Action<int> OnCurrentSelectedProgressionIndexChanged;
    public event Action OnInitialized;

    public static ProgressionsManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadProgressions();

        OnInitialized?.Invoke();
    }

    public void AddCurrentGuitarPosition()
    {
        if (m_CurrentSelectedProgressionIndex == -1) return;

        var tuning = GuitarManager.Instance.Tuning;
        var capoPosition = GuitarManager.Instance.CapoPosition;
        var position = GuitarManager.Instance.CurrentPosition;

        if (!m_Progressions[m_CurrentSelectedProgressionIndex].Tuning.Equals(tuning))
        {
            UIManager.Instance.ShowMessage("The current tuning doesn't match the current progression.", "Continue");
            return;
        }

        if (m_Progressions[m_CurrentSelectedProgressionIndex].CapoPosition != capoPosition)
        {
            UIManager.Instance.ShowMessage("The current capo position doesn't match the current progression.", "Continue");
            return;
        }

        if (m_Progressions[m_CurrentSelectedProgressionIndex].ContainsPosition(position))
        {
            UIManager.Instance.ShowMessage("This position has already been added to the current progression.", "Continue");
            return;
        }

        m_Progressions[m_CurrentSelectedProgressionIndex].Positions.Add(position);

        UIManager.Instance.ShowNotification($"Added to '{m_Progressions[m_CurrentSelectedProgressionIndex].Name}'");

        SaveProgressions();
    }

    public void CreateProgression(string name)
    {
        m_Progressions.Add(new(name, GuitarManager.Instance.Tuning, GuitarManager.Instance.CapoPosition));
        CurrentSelectedProgressionIndex = m_Progressions.Count - 1;

        SaveProgressions();
    }

    private void SaveProgressions()
    {
        var progressions = JsonConvert.SerializeObject(m_Progressions);

        string filePath = Path.Combine(Application.persistentDataPath, k_ProgressionsFileName);
        File.WriteAllText(filePath, progressions);
    }

    private void LoadProgressions()
    {
        string filePath = Path.Combine(Application.persistentDataPath, k_ProgressionsFileName);
        if (File.Exists(filePath))
        {
            string data = File.ReadAllText(filePath);
            m_Progressions = JsonConvert.DeserializeObject<List<Progression>>(data);
        }
    }
}
