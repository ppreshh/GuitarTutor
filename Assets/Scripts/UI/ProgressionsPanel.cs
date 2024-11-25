using UnityEngine;
using UnityEngine.UI;

public class ProgressionsPanel : SlideInPanel
{
    [Header("Progression Panel")]
    [SerializeField] private ProgressionsListPanel m_ProgressionsListPanel;
    [SerializeField] private Button m_AddProgressionButton;

    protected override void Initialize()
    {
        m_AddProgressionButton.onClick.AddListener(() =>
        {
            UIManager.Instance.GetUserInput($"Tuning: {GuitarManager.Instance.Tuning}\nCapo Position: {GuitarManager.Instance.CapoPosition}\nNew Progression Name:", "Create", true, (string name) =>
            {
                ProgressionsManager.Instance.CreateProgression(name);
            });
        });

        base.Initialize();
    }

    protected override void CleanUp()
    {
        m_AddProgressionButton.onClick.RemoveAllListeners();

        base.CleanUp();
    }

    protected override void SetupUIBeforeSlideIn()
    {
        m_ProgressionsListPanel.RefreshUI();
    }
}
