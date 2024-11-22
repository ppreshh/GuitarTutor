using System;
using System.Collections.Generic;
using UnityEngine;

public static class ChordFinder
{
    // E B E G# B E

    private static readonly Dictionary<string, int> NoteToInt = new()
    {
        { "C", 0 },
        { "C#", 1 },
        { "D", 2 },
        { "D#", 3 },
        { "E", 4 },
        { "F", 5 },
        { "F#", 6 },
        { "G", 7 },
        { "G#", 8 },
        { "A", 9 },
        { "A#", 10 },
        { "B", 11 },
    };

    private static readonly Dictionary<string, string> IntervalToName = new()
    {
        { "3 7 ", "Minor" },
        { "4 7 ", "Major" },
        { "3 6 ", "Diminished" },
        { "4 8 ", "Augmented" },
        { "4 7 9 ", "Major7" },
        { "4 7 8 ", "Dominant7" },
        { "4 6 9 ", "Minor7" },
    };

    public static string GetChordName(List<string> notes)
    {
        List<int> noteNums = new();
        foreach (string note in notes)
        {
            if (noteNums.Contains(NoteToInt[note])) continue;

            noteNums.Add(NoteToInt[note]);
        }
        noteNums.Sort();

        string intervals = "";
        for (int i = 1; i < noteNums.Count; i++)
        {
            intervals += ((noteNums[i] - noteNums[0]).ToString() + " ");
        }

        return notes[0] + " " + IntervalToName[intervals];
    }
}
