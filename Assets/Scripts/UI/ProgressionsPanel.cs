using UnityEngine;
using UnityEngine.UI;

public class ProgressionsPanel : SlideInPanel
{
    [Header("Progression Panel")]
    [SerializeField] private ProgressionsListPanel m_ProgressionsListPanel;
    [SerializeField] private Button m_AddProgressionButton;

    private void Awake()
    {
        m_AddProgressionButton.onClick.AddListener(() =>
        {
            UIManager.Instance.GetUserInput("New Progression Name:", "Create", (string name) =>
            {
                ProgressionsManager.Instance.CreateProgression(name);
                m_ProgressionsListPanel.RefreshUI();
            });
        });
    }

    protected override void SetupUIBeforeSlideIn()
    {
        m_ProgressionsListPanel.RefreshUI();
    }
}
