using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : SlideInPanel
{
    private const string k_InertiaScrollingKey = "InertiaScrolling";

    [Header("Settings Panel")]
    [SerializeField] private Toggle m_InertiaScrollingToggle;
    [SerializeField] private ScrollRect m_GuitarScrollRect;

    protected override void Initialize()
    {
        InitializeSettings();

        m_InertiaScrollingToggle.onValueChanged.AddListener((bool value) =>
        {
            if (m_GuitarScrollRect.inertia == value) return;

            m_GuitarScrollRect.inertia = value;
            PlayerPrefs.SetInt(k_InertiaScrollingKey, value ? 1 : 0);
            PlayerPrefs.Save();
        });

        UpdateUI();

        base.Initialize();
    }

    protected override void CleanUp()
    {
        m_InertiaScrollingToggle.onValueChanged.RemoveAllListeners();

        base.CleanUp();
    }

    protected override void SetupUIBeforeSlideIn()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        m_InertiaScrollingToggle.SetIsOnWithoutNotify(PlayerPrefs.GetInt(k_InertiaScrollingKey) == 1);
    }

    // Set default settings here
    private void InitializeSettings()
    {
        m_GuitarScrollRect.inertia = PlayerPrefs.GetInt(k_InertiaScrollingKey, 0) == 1;
    }
}
