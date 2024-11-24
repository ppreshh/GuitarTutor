using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldPanel : Panel
{
    [Header("Input Field Panel")]
    [SerializeField] private TextMeshProUGUI m_MessageText;
    [SerializeField] private TMP_InputField m_InputField;
    [SerializeField] private Button m_SubmitButton;
    [SerializeField] private Button m_CancelButton;
    [SerializeField] private Transform m_LineImage;
    [SerializeField] private TextMeshProUGUI m_SubmitButtonText;

    private Action<string> m_OnSubmitButtonClicked = null;

    protected override void Initialize()
    {
        m_SubmitButton.onClick.AddListener(() => { m_OnSubmitButtonClicked?.Invoke(m_InputField.text); Hide(); });
        m_CancelButton.onClick.AddListener(() => { Hide(); });

        base.Initialize();
    }

    private void OnDestroy()
    {
        m_SubmitButton.onClick.RemoveAllListeners();
        m_CancelButton.onClick.RemoveAllListeners();
    }

    public void Show(string message, string submitButtonText, bool showCancelButton = true, Action<string> onSubmitButtonClicked = null)
    {
        m_InputField.text = string.Empty;

        m_LineImage.gameObject.SetActive(showCancelButton);
        m_CancelButton.gameObject.SetActive(showCancelButton);

        m_MessageText.text = message;
        m_SubmitButtonText.text = submitButtonText;
        m_OnSubmitButtonClicked = onSubmitButtonClicked;

        Show();
    }
}
