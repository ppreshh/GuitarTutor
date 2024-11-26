using System.Collections.Generic;

public static class NoteTools
{
    public static readonly Dictionary<string, int> NoteToInt = new()
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

    public static readonly Dictionary<int, string> IntToNote = new()
    {
        { 0, "C" },
        { 1 , "C#" },
        { 2 , "D" },
        { 3 , "D#" },
        { 4 , "E" },
        { 5 , "F" },
        { 6 , "F#" },
        { 7 , "G" },
        { 8 , "G#" },
        { 9 , "A" },
        { 10 , "A#" },
        { 11 , "B" },
    };

    /// <summary>
    /// Returns the note given the string's tuning and the fret number that's "pressed".
    /// </summary>
    /// <param name="note"></param>
    /// <param name="fretNumber"></param>
    /// <returns></returns>
    public static NoteWithOctave GetNote(NoteWithOctave note, int fretNumber)
    {
        string stringNote = note.Note;
        int octave = note.Octave;

        var stringNoteNumber = NoteToInt[stringNote];
        stringNoteNumber += fretNumber;

        int count = 0;
        while (stringNoteNumber > 11)
        {
            count++;
            stringNoteNumber -= 12;
        }

        octave += count;

        return new(IntToNote[stringNoteNumber], octave);
    }

    /// <summary>
    /// 'position' must be a Dictionary with 6 elements, one for each string
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static List<NoteWithOctave?> GetNotesFromPosition(Tuning tuning, Dictionary<int, int> position)
    {
        List<NoteWithOctave?> notes = new();
        for (int i = 1; i <= 6; i++)
        {
            if (position[i] == -1)
            {
                notes.Add(null);
            }
            else
            {
                notes.Add(GetNoteForString(tuning, position, i).Value);
            }
        }

        return notes;
    }

    /// <summary>
    /// 'position' must be a Dictionary with 6 elements, one for each string
    /// </summary>
    /// <param name="tuning"></param>
    /// <param name="position"></param>
    /// <param name="stringNumber"></param>
    /// <returns></returns>
    public static NoteWithOctave? GetNoteForString(Tuning tuning, Dictionary<int, int> position, int stringNumber)
    {
        if (position[stringNumber] == -1)
        {
            return null;
        }
        else
        {
            return GetNote(tuning.Settings[stringNumber], position[stringNumber]);
        }
    }
}
