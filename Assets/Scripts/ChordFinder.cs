using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ChordFinder
{
    private static readonly Dictionary<List<int>, string> IntervalToName = new()
    {
        { new(){ 3, 7 }, "Minor" },
        { new(){ 4, 7 }, "Major" },
        { new(){ 3, 6 }, "Diminished" },
        { new(){ 4, 8 }, "Augmented" },
        { new(){ 4, 7, 9 }, "Major7" },
        { new(){ 4, 7, 8 }, "Dominant7" },
        { new(){ 4, 6, 9 }, "Minor7" },
    };

    public static string GetChordName(List<NoteWithOctave?> notes)
    {
        //List<NoteWithOctave> chordNotes = new();
        //foreach (NoteWithOctave? note in notes)
        //{
        //    if (note.HasValue)
        //    {
        //        if (!chordNotes.Contains(note.Value))
        //        {
        //            chordNotes.Add(note.Value);
        //        }
        //    }
        //}

        //NoteWithOctave rootNote = chordNotes[0];

        //List<int> intervals = new();
        //for (int i = 1; i < chordNotes.Count; i++)
        //{
        //    intervals.Add(chordNotes[i].ToInt() - rootNote.ToInt());
        //}

        //intervals.Sort();


        // just for logging
        string notesUsedForIntervalCalc = "";

        int roomNoteNum = 0;
        foreach (var note in notes)
        {
            if (note.HasValue)
            {
                roomNoteNum = NoteTools.NoteToInt[note.Value.Note];
                break;
            }
        }

        List<int> noteNums = new();
        foreach (var note in notes)
        {
            if (note == null) continue;
            if (noteNums.Contains(NoteTools.NoteToInt[note.Value.Note])) continue;
            if (NoteTools.NoteToInt[note.Value.Note] == roomNoteNum) continue;

            noteNums.Add(NoteTools.NoteToInt[note.Value.Note]);

            // just for logging
            notesUsedForIntervalCalc += $"{note.Value.Note} ";
        }
        noteNums.Sort();
        noteNums.Insert(0, roomNoteNum);

        // just for logging
        string sortedNotes = "";
        foreach (var note in noteNums) sortedNotes += $"{NoteTools.IntToNote[note]} ";

        List<int> intervals = new();
        for (int i = 1; i < noteNums.Count; i++)
        {
            intervals.Add(noteNums[i] - noteNums[0]);
        }
        intervals.Sort();
        for (int i = 0; i < intervals.Count; i++)
        {
            if (intervals[i] <= 0)
            {
                intervals[i] += 12;
            }
        }
        intervals.Sort();

        // just for logging
        string intervalsString = "";
        foreach (var interval in intervals) intervalsString += (interval.ToString() + " ");

        Debug.Log($"GetChordName Log:\nRoot Note: {NoteTools.IntToNote[roomNoteNum]}\nNotes Used For Interval Calc: {notesUsedForIntervalCalc}\nSorted Notes: {sortedNotes}\nIntervals: {intervalsString}");

        var chordName = GetIntervalName(intervals);
        if (string.IsNullOrEmpty(chordName))
        {
            return "No Match";
        }
        else
        {
            return $"{NoteTools.IntToNote[roomNoteNum]} {chordName}";
        }
    }

    private static string GetIntervalName(List<int> intervals)
    {
        foreach (var kvp in IntervalToName)
        {
            if (kvp.Key.SequenceEqual(intervals)) return kvp.Value;
        }

        return null;
    }
}
