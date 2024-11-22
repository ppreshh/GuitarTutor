public class NoteWithOctave
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
}
