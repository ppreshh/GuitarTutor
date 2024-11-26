using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Progression
{
    public string Name;
    public Tuning Tuning;
    public int CapoPosition;
    public List<Dictionary<int, int>> Positions = new();

    public event Action OnProgressionUpdated;

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

    public bool TryMovePositionUpInProgression(int index)
    {
        if (index <= 0) return false;

        Positions.Swap(index, index - 1);
        OnProgressionUpdated?.Invoke();
        return true;
    }

    public bool TryMovePositionDownInProgression(int index)
    {
        if (index >= Positions.Count - 1) return false;

        Positions.Swap(index, index + 1);
        OnProgressionUpdated?.Invoke();
        return true;
    }

    public void DuplicatePosition(int index)
    {
        Positions.Insert(index, new(Positions[index]));
        OnProgressionUpdated?.Invoke();
    }

    public void DeletePosition(int index)
    {
        Positions.RemoveAt(index);
        OnProgressionUpdated?.Invoke();
    }
}
