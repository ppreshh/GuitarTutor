using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ChordFinder
{
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

        var name = IntervalsToName(intervals);

        Debug.Log(" --------------------- ");

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

        if (intervals.Count >= 4)
        {
            return CalcQuadAndMore(intervals);
        }

        return null;
    }

    private static string CalcQuadAndMore(List<int> intervals)
    {
        var name = CalcTriad(intervals);
        LogList(intervals, $"{name}, Remaining intervals for 4 note chord: ");

        if (name != null)
        {
            if (name.Equals("minor"))
            {
                if (intervals.TryCheckAndRemove(10)) name += "7";
            }

            if (name.Equals("major"))
            {
                if (intervals.TryCheckAndRemove(11)) name += "7";
            }

            if (intervals.TryCheckAndRemove(9)) name += " add6";
            if (intervals.TryCheckAndRemove(10)) name += " add7";
            if (intervals.TryCheckAndRemove(2)) name += " add9";
            if (intervals.TryCheckAndRemove(5)) name += " add11";
        }

        return name;
    }

    private static string CalcTriad(List<int> intervals)
    {
        intervals.Remove(0); // remove root note

        if (intervals.TryCheckAndRemove(7))
        {
            var name = "";
            
            int found = FindAndRemoveTriadInterval(intervals);

            if (found != -1)
            {
                if (found == 2) name = "sus2";
                if (found == 3) name = "minor";
                if (found == 4) name = "major";
                if (found == 5) name = "sus4";
            }

            if (!string.IsNullOrEmpty(name)) return name;
        }
        else if (intervals.TryCheckAndRemove(6))
        {
            if (intervals.TryCheckAndRemove(3)) return "dim";
        }
        else if (intervals.TryCheckAndRemove(8))
        {
            if (intervals.TryCheckAndRemove(4)) return "aug";
        }

        return null;
    }

    private static void LogList<T>(List<T> list, string header)
    {
        string result = header;
        foreach (var item in list) result += $"{item} ";
        Debug.Log(result);
    }

    private static int FindAndRemoveTriadInterval(List<int> intervals)
    {
        List<int> majorMinor = new() { 3, 4 };
        List<int> sus2sus4 = new() { 2, 5 };

        for (int i = 0; i < intervals.Count; i++)
        {
            if (majorMinor.Contains(intervals[i]))
            {
                int index = majorMinor.IndexOf(intervals[i]);

                intervals.RemoveAt(i);

                return majorMinor[index];
            }
        }

        for (int i = 0; i < intervals.Count; i++)
        {
            if (sus2sus4.Contains(intervals[i]))
            {
                int index = sus2sus4.IndexOf(intervals[i]);

                intervals.RemoveAt(i);

                return sus2sus4[index];
            }
        }

        return -1;
    }
}
