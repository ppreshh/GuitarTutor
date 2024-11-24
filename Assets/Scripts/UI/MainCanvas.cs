using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] private Button m_SettingsButton;
    [SerializeField] private Button m_ProgressionsButton;
    [SerializeField] private MainPanel m_MainPanel;
    [SerializeField] private SettingsPanel m_SettingsPanel;
    [SerializeField] private ProgressionsPanel m_ProgressionsPanel;
    [SerializeField] private InputFieldPanel m_InputFieldPanel;
    [SerializeField] private List<PegButton> m_PegButtons;

    private void Awake()
    {
        m_SettingsButton.onClick.AddListener(() =>
        {
            m_SettingsPanel.SlideIn();
        });

        m_ProgressionsButton.onClick.AddListener(() =>
        {
            m_ProgressionsPanel.SlideIn();
        });

        foreach (var button in m_PegButtons)
        {
            button.OnClicked += PegButton_OnClicked;
        }
    }

    private void OnDestroy()
    {
        m_SettingsButton.onClick.RemoveAllListeners();
        m_ProgressionsButton.onClick.RemoveAllListeners();

        foreach (var button in m_PegButtons)
        {
            button.OnClicked -= PegButton_OnClicked;
        }
    }

    private void PegButton_OnClicked(object sender, PegButton.ClickedEventArgs e)
    {
        m_InputFieldPanel.Show($"Set tuning for string {e.StringNumber}:", "Set", (string value) =>
        {
            if (NoteWithOctave.TryParse(value, out var note))
            {
                GuitarManager.Instance.UpdateTuning(e.StringNumber, note);
            }
        });
    }
}
