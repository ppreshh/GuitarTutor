using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PegButton : MonoBehaviour
{
    [SerializeField] private int m_StringNumber;
    [SerializeField] private Button m_Button;
    [SerializeField] private TextMeshProUGUI m_Text;

    private void Start()
    {
        m_Button.onClick.AddListener(() =>
        {
            if (ProgressionsManager.Instance.CurrentProgression == null)
            {
                UIManager.Instance.GetUserInput($"Set tuning for string {m_StringNumber}:", "Set", true, (string value) =>
                {
                    if (NoteWithOctave.TryParse(value, out var note))
                    {
                        GuitarManager.Instance.UpdateTuning(m_StringNumber, note);
                    }
                });
            }
            else
            {
                UIManager.Instance.ShowMessage(
                    $"Can't tune while a progression is selected. Deselect current progression <b>{ProgressionsManager.Instance.CurrentProgression.Name}</b>?",
                    "Yes",
                    true,
                    () =>
                    {
                        ProgressionsManager.Instance.CurrentSelectedProgressionIndex = -1;
                    });
            }
        });

        GuitarManager.Instance.OnTuningUpdated += GuitarManager_OnTuningUpdated;

        UpdateVisuals();
    }

    private void OnDestroy()
    {
        GuitarManager.Instance.OnTuningUpdated -= GuitarManager_OnTuningUpdated;

        m_Button.onClick.RemoveAllListeners();
    }

    private void GuitarManager_OnTuningUpdated()
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        m_Text.text = GuitarManager.Instance.Tuning.Settings[m_StringNumber].ToString();
    }
}
