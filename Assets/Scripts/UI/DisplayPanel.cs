using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPanel : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> m_NoteTexts;
    [SerializeField] private TextMeshProUGUI m_ChordNameText;
    [SerializeField] private Button m_StrumButton;
    [SerializeField] private Button m_ResetButton;

    private void Start()
    {
        m_StrumButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.StrumChord(GuitarManager.Instance.GetAllCurrentNotesWithOctaves());
        });

        m_ResetButton.onClick.AddListener(() =>
        {
            GuitarManager.Instance.SetCurrentPositionToAllOpen();
        });

        GuitarManager.Instance.OnCurrentPositionUpdated += GuitarManager_OnCurrentPositionUpdated;

        Initialize();
    }

    private void OnDestroy()
    {
        m_StrumButton.onClick.RemoveAllListeners();
        m_ResetButton.onClick.RemoveAllListeners();

        GuitarManager.Instance.OnCurrentPositionUpdated -= GuitarManager_OnCurrentPositionUpdated;
    }

    private void GuitarManager_OnCurrentPositionUpdated()
    {
        UpdateVisuals();
    }

    private void Initialize()
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        for (int i = 0; i < 6; i++)
        {
            var note = GuitarManager.Instance.GetCurrentNoteForString(i + 1);

            m_NoteTexts[i].text = note == null ? " - " : note.ToString();
        }

        m_ChordNameText.text = ChordFinder.GetChordName(GuitarManager.Instance.GetAllCurrentNotes());
    }
}
