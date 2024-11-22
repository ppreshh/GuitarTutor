using System.Collections.Generic;

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

    public static string GetChordName(List<string> notes)
    {
        List<int> noteNums = new();
        foreach (string note in notes)
        {
            if (noteNums.Contains(NoteTables.NoteToInt[note])) continue;

            noteNums.Add(NoteTables.NoteToInt[note]);
        }
        noteNums.Sort();

        string intervals = "";
        for (int i = 1; i < noteNums.Count; i++)
        {
            intervals += ((noteNums[i] - noteNums[0]).ToString() + " ");
        }

        if (IntervalToName.TryGetValue(intervals, out var chordName))
        {
            return notes[0] + " " + chordName;
        }
        else
        {
            return "No Match";
        }
    }
}
