using System.Collections.Generic;

public class Progression
{
    public string Name;
    public Dictionary<int, NoteWithOctave> Tuning;
    public List<Dictionary<int, int>> Positions = new();

    public Progression(string name, Dictionary<int, NoteWithOctave> tuning)
    {
        Name = name;
        Tuning = tuning;
    }
}
