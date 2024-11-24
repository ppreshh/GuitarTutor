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
    [SerializeField] private TextMeshProUGUI m_SubmitButtonText;

    private Action<string> m_OnSubmitButtonClicked = null;

    protected override void Initialize()
    {
        m_SubmitButton.onClick.AddListener(() => { m_OnSubmitButtonClicked(m_InputField.text); Hide(); });

        base.Initialize();
    }

    private void OnDestroy()
    {
        m_SubmitButton.onClick.RemoveAllListeners();
    }

    public void Show(string message, string submitButtonText, Action<string> onSubmitButtonClicked)
    {
        m_MessageText.text = message;
        m_SubmitButtonText.text = submitButtonText;
        m_OnSubmitButtonClicked = onSubmitButtonClicked;

        Show();
    }
}
