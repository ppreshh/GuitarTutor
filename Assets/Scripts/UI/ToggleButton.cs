using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    [Header("Toggle Button")]

    [Header("UI Elements")]
    [SerializeField] private Button m_Button;
    [SerializeField] private TextMeshProUGUI m_Text;
    [SerializeField] private Image m_BackgroundImage;

    [Header("Settings")]
    [SerializeField] private string m_OnText;
    [SerializeField] private string m_OffText;
    [Space(5)]
    [SerializeField] private Color m_TextOnColor;
    [SerializeField] private Color m_TextOffColor;
    [Space(5)]
    [SerializeField] private Color m_ImageOnColor;
    [SerializeField] private Color m_ImageOffColor;
    [Space(10)]
    [SerializeField] private bool m_IsOn = false;
    public bool IsOn { get => m_IsOn; }

    public event Action<bool> OnValueChanged;

    private void Awake()
    {
        m_Button.onClick.AddListener(() =>
        {
            SetIsOn(!m_IsOn);
        });

        UpdateVisuals();
    }

    private void OnDestroy()
    {
        m_Button.onClick.RemoveAllListeners();
    }

    public void SetIsOn(bool isOn)
    {
        if (isOn == m_IsOn) return;

        m_IsOn = isOn;
        UpdateVisuals();
        OnValueChanged?.Invoke(m_IsOn);
    }

    public void SetIsOnWithoutNotify(bool isOn)
    {
        if (isOn == m_IsOn) return;

        m_IsOn = isOn;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        m_Text.text = m_IsOn ? m_OnText : m_OffText;
        m_Text.color = m_IsOn ? m_TextOnColor : m_TextOffColor;
        m_BackgroundImage.color = m_IsOn ? m_ImageOnColor : m_ImageOffColor;
    }
}
