using UnityEngine;
using UnityEngine.UI;

public class ProgressionsPanel : SlideInPanel
{
    [Header("Progression Panel")]
    [SerializeField] private ProgressionsListPanel m_ProgressionsListPanel;
    [SerializeField] private Button m_AddProgressionButton;
    [SerializeField] private ProgressionViewPanel m_ProgressionViewPanel;

    protected override void Initialize()
    {
        m_ProgressionsListPanel.OnProgressionItemEditButtonClicked += ProgressionsListPanel_OnProgressionItemEditButtonClicked;

        m_AddProgressionButton.onClick.AddListener(() =>
        {
            UIManager.Instance.GetUserInput($"Tuning: {GuitarManager.Instance.Tuning}\nCapo Position: {GuitarManager.Instance.CapoPosition}\nNew Progression Name:", "Create", true, (string name) =>
            {
                ProgressionsManager.Instance.CreateProgression(name);
            });
        });

        m_ProgressionViewPanel.OnBackButtonClicked += ProgressionViewPanel_OnBackButtonClicked;

        base.Initialize();
    }

    private void Start()
    {
        ProgressionsManager.Instance.OnProgressionNameUpdated += ProgressionsManager_OnProgressionNameUpdated;
    }

    protected override void CleanUp()
    {
        m_ProgressionsListPanel.OnProgressionItemEditButtonClicked -= ProgressionsListPanel_OnProgressionItemEditButtonClicked;

        m_AddProgressionButton.onClick.RemoveAllListeners();

        m_ProgressionViewPanel.OnBackButtonClicked -= ProgressionViewPanel_OnBackButtonClicked;

        ProgressionsManager.Instance.OnProgressionNameUpdated -= ProgressionsManager_OnProgressionNameUpdated;

        base.CleanUp();
    }

    protected override void SetupUIBeforeSlideIn()
    {
        m_ProgressionViewPanel.Hide(0f);

        m_ProgressionsListPanel.RefreshUI();
        m_ProgressionsListPanel.Show(0f);
    }

    private void ProgressionsListPanel_OnProgressionItemEditButtonClicked(int index)
    {
        m_ProgressionsListPanel.Hide(0f);
        m_ProgressionViewPanel.Show(ProgressionsManager.Instance.Progressions[index]);
    }

    private void ProgressionViewPanel_OnBackButtonClicked()
    {
        m_ProgressionViewPanel.Hide(0f);
        m_ProgressionsListPanel.Show(0.1f);
    }

    private void ProgressionsManager_OnProgressionNameUpdated()
    {
        m_ProgressionsListPanel.RefreshUI();
    }
}
