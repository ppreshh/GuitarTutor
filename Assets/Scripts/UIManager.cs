using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputFieldPanel m_InputFieldPanel;

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void ShowInputFieldPopup(string message, string submitButtonText, Action<string> onSubmitButtonClicked)
    {
        m_InputFieldPanel.Show(message, submitButtonText, onSubmitButtonClicked);
    }
}
