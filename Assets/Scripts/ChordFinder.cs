using System;
using System.Collections.Generic;
using System.Linq;

// THIS IS ALL CHATGPT's DOING -.-

public static class ChordFinder
{
    // Define the standard notes in an octave
    private static readonly string[] Notes = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

    // Define the basic intervals for major and minor chords
    private static readonly Dictionary<string, int> IntervalSemitones = new Dictionary<string, int>
    {
        { "major", 4 },   // Major third
        { "minor", 3 },   // Minor third
        { "perfect", 5 }, // Perfect fifth
        { "major7", 11 }, // Major 7th
        { "minor7", 10 }, // Minor 7th
        { "dominant7", 10 }, // Dominant 7th (same as minor7)
    };

    // Main function to detect chord name based on input notes
    public static string GetChordName(List<string> inputNotes)
    {
        // Split the input string into notes and octaves
        List<NoteWithOctave> parsedNotes = inputNotes.Select(note => ParseNoteWithOctave(note)).ToList();

        // Sort the notes by their pitch (note + octave)
        parsedNotes = parsedNotes.OrderBy(note => Array.IndexOf(Notes, note.Note) + note.Octave * 12).ToList();

        // Detect the root note (the first note in the sorted list)
        string rootNote = parsedNotes[0].Note;

        // Analyze intervals between the root note and the other notes
        List<int> intervals = new List<int>();
        foreach (var parsedNote in parsedNotes.Skip(1))
        {
            int interval = GetIntervalBetweenNotes(rootNote, parsedNote.Note);
            intervals.Add(interval);
        }

        // Identify the chord based on intervals
        return IdentifyChord(rootNote, intervals);
    }

    // Function to parse note with octave (e.g., "E2", "B3")
    private static NoteWithOctave ParseNoteWithOctave(string noteWithOctave)
    {
        string note = new string(noteWithOctave.Where(char.IsLetter).ToArray()); // Extract the note (e.g., "E", "C#")
        int octave = int.Parse(new string(noteWithOctave.Where(char.IsDigit).ToArray())); // Extract the octave number (e.g., "2", "3")

        return new NoteWithOctave(note, octave);
    }

    // Function to calculate the interval between two notes
    private static int GetIntervalBetweenNotes(string note1, string note2)
    {
        int index1 = Array.IndexOf(Notes, note1);
        int index2 = Array.IndexOf(Notes, note2);
        int interval = (index2 - index1 + 12) % 12;  // Ensure positive interval
        return interval;
    }

    // Function to identify chord based on root note and intervals
    private static string IdentifyChord(string rootNote, List<int> intervals)
    {
        // E Minor Chord: root + minor third + perfect fifth
        if (intervals.Contains(IntervalSemitones["minor"]) && intervals.Contains(IntervalSemitones["perfect"]))
        {
            return $"{rootNote} Minor";
        }

        // Major Chord: root + major third + perfect fifth
        if (intervals.Contains(IntervalSemitones["major"]) && intervals.Contains(IntervalSemitones["perfect"]))
        {
            if (intervals.Contains(IntervalSemitones["major7"]))
                return $"{rootNote} Major 7th";
            else
                return $"{rootNote} Major";
        }

        // Minor 7th Chord
        if (intervals.Contains(IntervalSemitones["minor"]) && intervals.Contains(IntervalSemitones["perfect"]) && intervals.Contains(IntervalSemitones["minor7"]))
        {
            return $"{rootNote} Minor 7th";
        }

        // Diminished Chord: root + minor third + diminished fifth
        if (intervals.Contains(IntervalSemitones["minor"]) && intervals.Contains(IntervalSemitones["perfect"]))
        {
            return $"{rootNote} Diminished";
        }

        // Dominant 7th Chord
        if (intervals.Contains(IntervalSemitones["major"]) && intervals.Contains(IntervalSemitones["perfect"]) && intervals.Contains(IntervalSemitones["dominant7"]))
        {
            return $"{rootNote} Dominant 7th";
        }

        // If no known chord structure is found, return "Unknown"
        return "Unknown Chord";
    }

    // Helper class to hold a note and its octave
    private class NoteWithOctave
    {
        public string Note { get; }
        public int Octave { get; }

        public NoteWithOctave(string note, int octave)
        {
            Note = note;
            Octave = octave;
        }
    }
}
