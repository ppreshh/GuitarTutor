using System;
using System.Collections.Generic;
using System.Linq;

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
        return Settings.SequenceEqual(other.Settings);
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
}
