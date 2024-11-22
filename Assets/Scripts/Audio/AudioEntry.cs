using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "Scriptable Objects/Audio/Audio Entry")]
public class AudioEntry : ScriptableObject
{
    [SerializeField] private string m_Note;
    public string Note { get => m_Note; }

    [SerializeField] private AudioClip m_AudioClip;
    public AudioClip AudioClip { get => m_AudioClip; }
}
