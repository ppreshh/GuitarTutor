using System.Collections.Generic;
using UnityEngine;

public static class ChordFinder
{
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

    public static string GetChordName(List<NoteWithOctave> notes)
    {
        // just for logging
        string notesString = "";
        foreach (var note in notes) if (note != null) notesString += $"{note.ToString()} ";
        string notesUsedForIntervalCalc = "";

        List<int> noteNums = new();
        foreach (var note in notes)
        {
            if (note == null) continue;
            if (noteNums.Contains(NoteTables.NoteToInt[note.Note])) continue;

            noteNums.Add(NoteTables.NoteToInt[note.Note]);

            // just for logging
            notesUsedForIntervalCalc += $"{note.Note} ";
        }
        noteNums.Sort();

        // just for logging
        string sortedNotes = "";
        foreach (var note in noteNums) sortedNotes += $"{NoteTables.IntToNote[note]} ";

        string intervals = "";
        for (int i = 1; i < noteNums.Count; i++)
        {
            intervals += ((noteNums[i] - noteNums[0]).ToString() + " ");
        }

        Debug.Log($"GetChordName Log:\nNotes: {notesString}\nNotes Used For Interval Calc: {notesUsedForIntervalCalc}\nSorted Notes: {sortedNotes}\nIntervals: {intervals}");

        if (IntervalToName.TryGetValue(intervals, out var chordName))
        {
            return NoteTables.IntToNote[noteNums[0]] + " " + chordName;
        }
        else
        {
            return "No Match";
        }
    }
}
