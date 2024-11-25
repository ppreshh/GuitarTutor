using System;
using System.Collections.Generic;

[Serializable]
public class Tuning : IEquatable<Tuning>
{
    // Guitar String Number, NoteWithOctave
    public Dictionary<int, NoteWithOctave> Settings;

    public Tuning (Dictionary<int, NoteWithOctave> settings)
    {
        Settings = settings;
    }

    public bool Equals(Tuning other)
    {
        for (int i = 1; i <= 6; i++)
        {
            if (!Settings[i].Equals(other.Settings[i])) return false;
        }

        return true;
    }

    public Tuning Copy()
    {
        Tuning tuning = new(new(Settings));
        return tuning;
    }

    public override string ToString()
    {
        string tuning = "";

        for (int i = 1; i <= 6; i++)
        {
            tuning += Settings[i].Note + " ";
        }

        return tuning[..^1];
    }

    public static Tuning Default()
    {
        return new(new()
        {
            { 1, new("E", 2) },
            { 2, new("A", 2) },
            { 3, new("D", 3) },
            { 4, new("G", 3) },
            { 5, new("B", 3) },
            { 6, new("E", 4) },
        });
    }
}
