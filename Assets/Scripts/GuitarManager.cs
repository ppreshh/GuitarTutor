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

    public void UpdateCurrentPosition(int stringNumber, int fretNumber, bool isOn)
    {
        if (isOn)
        {
            if (fretNumber == 0)
            {
                m_CurrentPosition[stringNumber] = -1;
            }
            else
            {
                m_CurrentPosition[stringNumber] = 0;
            }
        }
        else
        {
            m_CurrentPosition[stringNumber] = fretNumber;
        }

        OnCurrentPositionUpdated?.Invoke();

        //LogCurrentPosition();
    }

    public NoteWithOctave GetCurrentNoteForString(int stringNumber)
    {
        return NoteTables.GetNote(m_Tuning[stringNumber], m_CurrentPosition[stringNumber]);
    }

    public List<string> GetAllCurrentNotes()
    {
        List<string> notes = new();
        for (int i = 1; i <= 6; i++)
        {
            notes.Add(GetCurrentNoteForString(i).Note);
        }

        return notes;
    }

    private void LogCurrentPosition()
    {
        string formmatedLog = "Current Position:";

        for (int i = 1; i <= 6; i++)
        {
            formmatedLog += $"\nString: {i}, Fret: {m_CurrentPosition[i]}";
        }

        Debug.Log(formmatedLog);
    }
}
