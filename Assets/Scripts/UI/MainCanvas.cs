using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] private Button m_SettingsButton;
    [SerializeField] private Button m_ProgressionsButton;
    [SerializeField] private MainPanel m_MainPanel;
    [SerializeField] private SettingsPanel m_SettingsPanel;
    [SerializeField] private ProgressionsPanel m_ProgressionsPanel;
    [SerializeField] private Button m_AddPositionToProgressionButton;

    private void Awake()
    {
        m_SettingsButton.onClick.AddListener(() =>
        {
            m_SettingsPanel.SlideIn();
            m_MainPanel.SetInteractable(false);
        });

        m_ProgressionsButton.onClick.AddListener(() =>
        {
            m_ProgressionsPanel.SlideIn();
            m_MainPanel.SetInteractable(false);
        });

        m_AddPositionToProgressionButton.onClick.AddListener(() =>
        {
            ProgressionsManager.Instance.AddCurrentGuitarPosition();
        });

        m_SettingsPanel.OnSlideOutCompleted += SettingsPanel_OnSlideOutCompleted;
        m_ProgressionsPanel.OnSlideOutCompleted += ProgressionsPanel_OnSlideOutCompleted;
    }

    private void Update()
    {
        m_AddPositionToProgressionButton.interactable = ProgressionsManager.Instance.CurrentSelectedProgressionIndex != -1;
    }

    private void OnDestroy()
    {
        m_SettingsButton.onClick.RemoveAllListeners();
        m_ProgressionsButton.onClick.RemoveAllListeners();
        m_AddPositionToProgressionButton.onClick.RemoveAllListeners();

        m_SettingsPanel.OnSlideOutCompleted -= SettingsPanel_OnSlideOutCompleted;
        m_ProgressionsPanel.OnSlideOutCompleted -= ProgressionsPanel_OnSlideOutCompleted;
    }

    private void SettingsPanel_OnSlideOutCompleted()
    {
        m_MainPanel.SetInteractable(true);
    }

    private void ProgressionsPanel_OnSlideOutCompleted()
    {
        m_MainPanel.SetInteractable(true);
    }
}
