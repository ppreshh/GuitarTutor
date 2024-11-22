using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "Scriptable Objects/Audio/Audio Library")]
public class AudioLibrary : ScriptableObjectLibrary<string, AudioEntry>
{
    protected override string ExtractKey(AudioEntry value)
    {
        return value.Note;
    }
}
