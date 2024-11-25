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

    // Guitar String, NoteWithOctave
    private Dictionary<int, NoteWithOctave> m_Tuning = new()
    {
        { 1, new("E", 2) },
        { 2, new("A", 2) },
        { 3, new("D", 3) },
        { 4, new("G", 3) },
        { 5, new("B", 3) },
        { 6, new("E", 4) },
    };
    public Dictionary<int, NoteWithOctave> Tuning { get => m_Tuning; }

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

    public NoteWithOctave GetCurrentNoteForString(int stringNumber)
    {
        if (m_CurrentPosition[stringNumber] == -1)
        {
            return null;
        }
        else
        {
            return NoteTables.GetNote(m_Tuning[stringNumber], m_CurrentPosition[stringNumber]);
        }
    }

    public List<NoteWithOctave> GetAllCurrentNotes()
    {
        List<NoteWithOctave> notes = new();
        for (int i = 1; i <= 6; i++)
        {
            if (m_CurrentPosition[i] == -1)
            {
                notes.Add(null);
            }
            else
            {
                notes.Add(GetCurrentNoteForString(i));
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

    public void SetTuning(Dictionary<int, NoteWithOctave> tuning)
    {
        for (int i = 1; i <= 6; i++)
        {
            if (!m_Tuning[i].Note.Equals(tuning[i]))
            {
                UpdateTuning(i, tuning[i]);
            }
        }
    }

    public void UpdateTuning(int stringNumber, NoteWithOctave note)
    {
        if (!m_Tuning[stringNumber].Equals(note))
        {
            m_Tuning[stringNumber] = note;

            OnCurrentPositionUpdated?.Invoke();
            OnTuningUpdated?.Invoke();
        }
    }

    public string GetFormattedTuning()
    {
        string tuning = "";

        for (int i = 1; i <= 6; i++)
        {
            tuning += m_Tuning[i].Note + " ";
        }

        return tuning[..^1];
    }
}
