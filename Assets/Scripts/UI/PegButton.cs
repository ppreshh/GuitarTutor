using System;
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
            UIManager.Instance.GetUserInput($"Set tuning for string {m_StringNumber}:", "Set", (string value) =>
            {
                if (NoteWithOctave.TryParse(value, out var note))
                {
                    GuitarManager.Instance.UpdateTuning(m_StringNumber, note);
                }
            });
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
        m_Text.text = GuitarManager.Instance.Tuning[m_StringNumber].ToString();
    }
}
