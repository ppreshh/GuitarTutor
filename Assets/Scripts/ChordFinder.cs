using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ChordFinder
{
    private static readonly Dictionary<List<int>, string> Triads = new()
    {
        { new(){ 0, 2, 7 }, "sus2" },
        { new(){ 0, 3, 7 }, "minor" },
        { new(){ 0, 4, 7 }, "major" },
        { new(){ 0, 5, 7 }, "sus4" },
        { new(){ 0, 4, 8 }, "aug" },
    };

    public static string GetChordName(List<NoteWithOctave?> notes)
    {
        // Add all played strings to list
        List<NoteWithOctave> chordNotes = new();
        foreach (NoteWithOctave? note in notes)
        {
            if (note.HasValue)
            {
                chordNotes.Add(note.Value);
            }
        }

        // Store root note
        NoteWithOctave rootNote = chordNotes[0];

        // Remove duplicates
        List<NoteWithOctave> intervalNotes = new();
        foreach (var note in chordNotes)
        {
            if (!intervalNotes.Any(obj => obj.Note.Equals(note.Note)))
            {
                intervalNotes.Add(note);
            }
        }

        // calculate intervals
        List<int> intervals = new();
        for (int i = 0; i < intervalNotes.Count; i++)
        {
            int interval = intervalNotes[i].ToInt(false) - rootNote.ToInt(false);
            while (interval < 0) interval += 12;

            intervals.Add(interval);
        }

        LogList(chordNotes, "All Notes: ");
        LogList(intervalNotes, "Remove Duplicates: ");
        LogList(intervals, "Calculated Intervals: ");
        Debug.Log(" --------------------- ");

        var name = IntervalsToName(intervals);
        if (name != null)
        {
            return $"{rootNote.Note}{name}";
        }
        else
        {
            return "No Match";
        }
    }

    private static string IntervalsToName(List<int> intervals)
    {
        if (intervals.Count == 0)
        {
            return "None";
        }

        if (intervals.Count == 1)
        {
            return $"{intervals[0]}";
        }

        if (intervals.Count == 2)
        {
            return "Diad";
        }

        if (intervals.Count == 3)
        {
            return CalcTriad(intervals);
        }

        return null;
    }

    private static string CalcTriad(List<int> intervals)
    {
        intervals.Remove(0); // remove root note
        int perfectFifthIndex = intervals.IndexOf(7);
        int dimIndex = intervals.IndexOf(6);
        int augIndex = intervals.IndexOf(8);

        if (perfectFifthIndex >= 0)
        {
            intervals.RemoveAt(perfectFifthIndex);

            var name = "";

            if (intervals.TryCheckAndRemove(2, out _)) name = "sus2";
            else if (intervals.TryCheckAndRemove(3, out _)) name = "minor";
            else if (intervals.TryCheckAndRemove(4, out _)) name = "major";
            else if (intervals.TryCheckAndRemove(5, out _)) name = "sus4";

            if (!string.IsNullOrEmpty(name)) return name;
        }
        else if (dimIndex >= 0)
        {
            intervals.RemoveAt(dimIndex);
            if (intervals.TryCheckAndRemove(3, out _)) return "dim";
        }
        else if (augIndex >= 0)
        {
            intervals.RemoveAt(augIndex);
            if (intervals.TryCheckAndRemove(4, out _)) return "aug";
        }

        return null;
    }

    private static void LogList<T>(List<T> list, string header)
    {
        string result = header;
        foreach (var item in list) result += $"{item} ";
        Debug.Log(result);
    }

    private static bool TryCheckAndRemove<T>(this List<T> list, T checkValue, out T removedValue)
    {
        removedValue = default;
        int index = list.IndexOf(checkValue);
        if (index >= 0)
        {
            removedValue = list[index];
            list.RemoveAt(index);
            return true;
        }

        return false;
    }
}
