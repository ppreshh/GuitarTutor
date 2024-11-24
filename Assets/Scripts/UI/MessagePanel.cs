using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : Panel
{
    [Header("Message Panel")]
    [SerializeField] private TextMeshProUGUI m_MessageText;
    [SerializeField] private Button m_SubmitButton;
    [SerializeField] private TextMeshProUGUI m_SubmitButtonText;

    private Action m_OnSubmitButtonClicked = null;

    protected override void Initialize()
    {
        m_SubmitButton.onClick.AddListener(() => { m_OnSubmitButtonClicked?.Invoke(); Hide(); });

        base.Initialize();
    }

    private void OnDestroy()
    {
        m_SubmitButton.onClick.RemoveAllListeners();
    }

    public void Show(string message, string submitButtonText, Action onSubmitButtonClicked = null)
    {
        m_MessageText.text = message;
        m_SubmitButtonText.text = submitButtonText;
        m_OnSubmitButtonClicked = onSubmitButtonClicked;

        Show();
    }
}