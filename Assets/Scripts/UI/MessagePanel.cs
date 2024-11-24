using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : Panel
{
    [Header("Message Panel")]
    [SerializeField] private TextMeshProUGUI m_MessageText;
    [SerializeField] private Button m_SubmitButton;
    [SerializeField] private Button m_CancelButton;
    [SerializeField] private Transform m_LineImage;
    [SerializeField] private TextMeshProUGUI m_SubmitButtonText;

    private Action m_OnSubmitButtonClicked = null;

    protected override void Initialize()
    {
        m_SubmitButton.onClick.AddListener(() => { m_OnSubmitButtonClicked?.Invoke(); Hide(); });
        m_CancelButton.onClick.AddListener(() => { Hide(); });

        base.Initialize();
    }

    private void OnDestroy()
    {
        m_SubmitButton.onClick.RemoveAllListeners();
        m_CancelButton.onClick.RemoveAllListeners();
    }

    public void Show(string message, string submitButtonText, bool showCancelButton = false, Action onSubmitButtonClicked = null)
    {
        m_LineImage.gameObject.SetActive(showCancelButton);
        m_CancelButton.gameObject.SetActive(showCancelButton);

        m_MessageText.text = message;
        m_SubmitButtonText.text = submitButtonText;
        m_OnSubmitButtonClicked = onSubmitButtonClicked;

        Show();
    }
}