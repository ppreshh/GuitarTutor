using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<AudioSource> m_AudioSources;
    [SerializeField] private List<AudioLibrary> m_KremonaAudioLibrary;

    private Coroutine m_StrumChordCoroutine = null;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void StrumChord(List<NoteWithOctave?> notes)
    {
        if (m_StrumChordCoroutine != null)
        {
            StopCoroutine(m_StrumChordCoroutine);
            m_StrumChordCoroutine = null;
        }

        StopAllAudio();

        m_StrumChordCoroutine = StartCoroutine(StrumChordCoroutine(notes));
    }

    private IEnumerator StrumChordCoroutine(List<NoteWithOctave?> notes)
    {
        for (int i = 0; i < notes.Count; i++)
        {
            if (notes[i] == null) continue;

            PlayNote(i + 1, notes[i].Value);

            yield return new WaitForSeconds(0.05f);
        }
    }

    public void PlayNote(int stringNumber, NoteWithOctave note)
    {
        if (!m_KremonaAudioLibrary[stringNumber - 1].Entries.ContainsKey(note.ToString()))
        {
            Debug.LogWarning($"No sample for string {stringNumber}, note {note}");
            return;
        }

        var audioSource = m_AudioSources[stringNumber - 1];

        audioSource.clip = m_KremonaAudioLibrary[stringNumber - 1].Entries[note.ToString()].AudioClip;
        audioSource.Play();
    }

    private void StopAllAudio()
    {
        foreach(var audioSource in m_AudioSources)
        {
            audioSource.Stop();
        }
    }
}
