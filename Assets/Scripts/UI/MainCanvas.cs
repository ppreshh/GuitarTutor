using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] private Button m_SettingsButton;
    [SerializeField] private Button m_ProgressionsButton;
    [SerializeField] private MainPanel m_MainPanel;
    [SerializeField] private SettingsPanel m_SettingsPanel;
    [SerializeField] private ProgressionsPanel m_ProgressionsPanel;

    private void Awake()
    {
        m_SettingsButton.onClick.AddListener(() =>
        {
            m_SettingsPanel.Show();
        });

        m_ProgressionsButton.onClick.AddListener(() =>
        {
            m_ProgressionsPanel.Show();
        });
    }

    private void OnDestroy()
    {
        m_SettingsButton.onClick.RemoveAllListeners();
        m_ProgressionsButton.onClick.RemoveAllListeners();
    }
}
