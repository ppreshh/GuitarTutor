using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Progression
{
    public string Name;
    public Tuning Tuning;
    public int CapoPosition;
    public List<Dictionary<int, int>> Positions = new();

    public Progression(string name, Tuning tuning, int capoPosition)
    {
        Name = name;
        Tuning = tuning;
        CapoPosition = capoPosition;
    }

    public bool ContainsPosition(Dictionary<int, int> positionToCheck)
    {
        foreach (var position in Positions)
        {
            if (position.SequenceEqual(positionToCheck)) return true;
        }

        return false;
    }
}
