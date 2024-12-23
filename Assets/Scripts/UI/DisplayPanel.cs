using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPanel : MonoBehaviour
{
    [SerializeField] private List<NoteButton> m_NoteButtons;
    [SerializeField] private TextMeshProUGUI m_ChordNameText;
    [SerializeField] private Button m_StrumButton;
    [SerializeField] private Button m_ResetButton;

    private void Start()
    {
        m_StrumButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.StrumChord(GuitarManager.Instance.GetAllCurrentNotes());
        });

        m_ResetButton.onClick.AddListener(() =>
        {
            if (!GuitarManager.Instance.CurrentPositionIsAllOpen())
            {
                GuitarManager.Instance.SetCurrentPositionToAllOpen();
                UIManager.Instance.ShowNotification("Position Reset");
                return;
            }

            if (ProgressionsManager.Instance.CurrentProgression != null)
            {
                UIManager.Instance.ShowMessage(
                    $"Can't reset Capo or Tuning while a progression is selected. Would you like to deselect the progression <b>{ProgressionsManager.Instance.CurrentProgression.Name}</b>?", 
                    "Yes",
                    true,
                    () =>
                    {
                        ProgressionsManager.Instance.CurrentSelectedProgressionIndex = -1;
                    });
                return;
            }
            
            if (GuitarManager.Instance.CapoPosition > 0)
            {
                GuitarManager.Instance.RemoveCapo();
                GuitarManager.Instance.SetCurrentPositionToAllOpen();
                UIManager.Instance.ShowNotification("Capo and Position Reset");
                return;
            }

            if (!GuitarManager.Instance.Tuning.Equals(Tuning.Default()))
            {
                GuitarManager.Instance.SetTuning(Tuning.Default());
                UIManager.Instance.ShowNotification("Tuning Reset");
                return;
            }
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

            m_NoteButtons[i].SetNote(note);
        }

        m_ChordNameText.text = ChordFinder.GetChordName(GuitarManager.Instance.GetAllCurrentNotes());
    }
}
