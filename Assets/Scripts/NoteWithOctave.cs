using System;
using System.Text.RegularExpressions;

public struct NoteWithOctave : IEquatable<NoteWithOctave>
{
    public string Note { get; set; }
    public int Octave { get; set; }

    public NoteWithOctave(string note, int octave)
    {
        Note = note;
        Octave = octave;
    }

    public override string ToString()
    {
        return $"{Note}{Octave}";
    }

    public bool Equals(NoteWithOctave other)
    {
        return Note == other.Note && Octave == other.Octave;
    }

    public static bool TryParse(string value, out NoteWithOctave note)
    {
        note = default;
        if (string.IsNullOrEmpty(value)) return false;

        // Define a regex pattern to match notes with optional sharp (#) and octave
        var pattern = @"^([A-G](#?))(\d)$"; // Matches: A-G followed by optional # and a single digit octave
        var match = Regex.Match(value, pattern);

        if (match.Success)
        {
            string notePart = match.Groups[1].Value + match.Groups[2].Value;  // Combine base note and sharp
            int octave;
            if (int.TryParse(match.Groups[3].Value, out octave))
            {
                note = new NoteWithOctave(notePart, octave);
                return true;
            }
        }

        return false;
    }
}
