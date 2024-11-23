using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_CanvasGroup;
    [SerializeField] private TextMeshProUGUI m_MessageText;
    [SerializeField] private TMP_InputField m_InputField;
    [SerializeField] private Button m_SubmitButton;
    [SerializeField] private TextMeshProUGUI m_SubmitButtonText;

    private bool m_IsVisible = false;
    private Action<string> m_OnSubmitButtonClicked = null;

    private void Awake()
    {
        m_SubmitButton.onClick.AddListener(() => { m_OnSubmitButtonClicked(m_InputField.text); Hide(); });
    }

    private void OnDestroy()
    {
        m_SubmitButton.onClick.RemoveAllListeners();
    }

    private void Show()
    {
        if (m_IsVisible) return;

        m_CanvasGroup.DOFade(1f, 0.1f);
        m_CanvasGroup.interactable = true;
        m_CanvasGroup.blocksRaycasts = true;

        m_IsVisible = true;
    }

    private void Hide()
    {
        if (!m_IsVisible) return;

        m_CanvasGroup.DOFade(0f, 0.1f);
        m_CanvasGroup.interactable = false;
        m_CanvasGroup.blocksRaycasts = false;

        m_IsVisible = false;
    }

    public void Show(string message, string submitButtonText, Action<string> onSubmitButtonClicked)
    {
        m_MessageText.text = message;
        m_SubmitButtonText.text = submitButtonText;
        m_OnSubmitButtonClicked = onSubmitButtonClicked;

        Show();
    }
}
