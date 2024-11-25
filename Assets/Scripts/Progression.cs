using System.Collections.Generic;

[System.Serializable]
public class Progression
{
    public string Name;
    public Dictionary<int, NoteWithOctave> Tuning;
    public int CapoPosition;
    public List<Dictionary<int, int>> Positions = new();

    public Progression(string name, Dictionary<int, NoteWithOctave> tuning, int capoPosition)
    {
        Name = name;
        Tuning = tuning;
        CapoPosition = capoPosition;
    }
}
