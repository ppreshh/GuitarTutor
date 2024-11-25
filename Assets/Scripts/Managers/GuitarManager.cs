using Palmmedia.ReportGenerator.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GuitarManager : MonoBehaviour
{
    public static GuitarManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public event Action OnCurrentPositionUpdated;
    public event Action OnCapoPositionUpdated;
    public event Action OnTuningUpdated;

    private Tuning m_Tuning = Tuning.Default();
    public Tuning Tuning { get => m_Tuning; }

    // Guitar String, Fret
    private Dictionary<int, int> m_CurrentPosition = new()
    {
        { 1, 0 },
        { 2, 0 },
        { 3, 0 },
        { 4, 0 },
        { 5, 0 },
        { 6, 0 },
    };
    public Dictionary<int, int> CurrentPosition { get => m_CurrentPosition; }

    private int m_CapoPosition = 0;
    public int CapoPosition { get => m_CapoPosition; }

    public void SetCurrentPositionToAllOpen()
    {
        for (int i = 1; i <= 6; i++)
        {
            if (m_CapoPosition > 0)
            {
                m_CurrentPosition[i] = m_CapoPosition;
            }
            else
            {
                m_CurrentPosition[i] = 0;
            }
        }

        OnCurrentPositionUpdated?.Invoke();
    }

    public void UpdateCurrentPosition(int stringNumber, int fretNumber, bool isOn)
    {
        if (isOn)
        {
            if (m_CapoPosition > 0)
            {
                if (fretNumber == m_CapoPosition) m_CurrentPosition[stringNumber] = -1;
                else m_CurrentPosition[stringNumber] = m_CapoPosition;
            }
            else
            {
                if (fretNumber == 0) m_CurrentPosition[stringNumber] = -1;
                else m_CurrentPosition[stringNumber] = 0;
            }

            OnCurrentPositionUpdated?.Invoke();
        }
        else
        {
            if (fretNumber >= m_CapoPosition)
            {
                m_CurrentPosition[stringNumber] = fretNumber;

                OnCurrentPositionUpdated?.Invoke();
            }
        }
    }

    public NoteWithOctave? GetCurrentNoteForString(int stringNumber)
    {
        if (m_CurrentPosition[stringNumber] == -1)
        {
            return null;
        }
        else
        {
            return NoteTables.GetNote(m_Tuning.Settings[stringNumber], m_CurrentPosition[stringNumber]);
        }
    }

    public List<NoteWithOctave?> GetAllCurrentNotes()
    {
        List<NoteWithOctave?> notes = new();
        for (int i = 1; i <= 6; i++)
        {
            if (m_CurrentPosition[i] == -1)
            {
                notes.Add(null);
            }
            else
            {
                notes.Add(GetCurrentNoteForString(i).Value);
            }
        }

        return notes;
    }

    public void UseCapo()
    {
        UpdateCapoPosition(1);
    }

    public void RemoveCapo()
    {
        m_CapoPosition = 0;
        OnCapoPositionUpdated?.Invoke();
    }

    public void UpdateCapoPosition(int fretNumber)
    {
        if (fretNumber <= 22 && fretNumber >= 1)
        {
            int increment = fretNumber - m_CapoPosition;

            m_CapoPosition = fretNumber;
            OnCapoPositionUpdated?.Invoke();

            for (int i = 1; i <= 6; i++)
            {
                if (m_CurrentPosition[i] != -1)
                {
                    m_CurrentPosition[i] += increment;
                }
            }

            OnCurrentPositionUpdated?.Invoke();
        }
    }

    public void SetTuning(Tuning tuning)
    {
        if (m_Tuning.Equals(tuning)) return;

        m_Tuning = tuning.Copy();

        OnCurrentPositionUpdated?.Invoke();
        OnTuningUpdated?.Invoke();
    }

    public void UpdateTuning(int stringNumber, NoteWithOctave note)
    {
        if (!m_Tuning.Settings[stringNumber].Equals(note))
        {
            m_Tuning.Settings[stringNumber] = note;

            OnCurrentPositionUpdated?.Invoke();
            OnTuningUpdated?.Invoke();
        }
    }

    public bool CurrentPositionIsAllOpen()
    {
        foreach (var position in m_CurrentPosition)
        {
            if (position.Value != m_CapoPosition) return false;
        }

        return true;
    }
}
