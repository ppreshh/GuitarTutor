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
            if (m_CurrentSelectedProgressionIndex == value) return;

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

    public Progression CurrentProgression
    {
        get
        {
            if (m_CurrentSelectedProgressionIndex < 0) return null;
            return m_Progressions[m_CurrentSelectedProgressionIndex];
        }
    }

    public event Action<int> OnCurrentSelectedProgressionIndexChanged;
    public event Action OnProgressionNameUpdated;
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

        if (!CurrentProgression.Tuning.Equals(tuning))
        {
            UIManager.Instance.ShowMessage("The current tuning doesn't match the current progression.", "Continue");
            return;
        }

        if (CurrentProgression.CapoPosition != capoPosition)
        {
            UIManager.Instance.ShowMessage("The current capo position doesn't match the current progression.", "Continue");
            return;
        }

        if (CurrentProgression.ContainsPosition(position))
        {
            UIManager.Instance.ShowMessage("This position has already been added to the current progression.", "Continue");
            return;
        }

        CurrentProgression.Positions.Add(new(position));

        UIManager.Instance.ShowNotification($"Added to '{CurrentProgression.Name}'");

        SaveProgressions();
    }

    public void CreateProgression(string name)
    {
        m_Progressions.Add(new(name, GuitarManager.Instance.Tuning, GuitarManager.Instance.CapoPosition));
        CurrentSelectedProgressionIndex = m_Progressions.Count - 1;

        SaveProgressions();
    }

    public void DeleteProgression(int index)
    {
        m_Progressions.RemoveAt(index);

        if (CurrentSelectedProgressionIndex == index || index == 0)
        {
            CurrentSelectedProgressionIndex = -1;
        }
        else if (CurrentSelectedProgressionIndex > index)
        {
            CurrentSelectedProgressionIndex--;
        }

        SaveProgressions();
    }

    public void SaveProgressions()
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

    public enum ProgressionPositionOp { MoveUp, MoveDown, Duplicate, Delete }
    public void UpdateProgressionPositions(Progression progression, ProgressionPositionOp op, int index)
    {
        bool success = false;
        switch (op)
        {
            case ProgressionPositionOp.MoveUp:
                if (progression.TryMovePositionUpInProgression(index)) success = true;
                break;
            case ProgressionPositionOp.MoveDown:
                if (progression.TryMovePositionDownInProgression(index)) success = true;
                break;
            case ProgressionPositionOp.Duplicate:
                progression.DuplicatePosition(index);
                success = true;
                break;
            case ProgressionPositionOp.Delete:
                progression.DeletePosition(index);
                success = true;
                break;
        }

        if (success) SaveProgressions();
    }

    public void UpdateProgressionName(Progression progression, string newName)
    {
        if (newName.Equals(progression.Name)) return;

        progression.Name = newName;

        SaveProgressions();

        OnProgressionNameUpdated?.Invoke();
    }
}
