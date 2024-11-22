using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayPanel : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> m_NoteTexts;
    [SerializeField] private TextMeshProUGUI m_ChordNameText;

    private void Start()
    {
        GuitarManager.Instance.OnCurrentPositionUpdated += GuitarManager_OnCurrentPositionUpdated;

        Initialize();
    }

    private void OnDestroy()
    {
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
            m_NoteTexts[i].text = GuitarManager.Instance.GetCurrentNoteForString(i + 1).ToString();
        }

        m_ChordNameText.text = ChordFinder.GetChordName(GuitarManager.Instance.GetAllCurrentNotes());
    }
}
