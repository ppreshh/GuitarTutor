using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private MessagePanel m_MessagePanel;
    [SerializeField] private InputFieldPanel m_InputFieldPanel;
    [SerializeField] private NotificationPanel m_NotificationPanel;

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;

        Application.targetFrameRate = 60;
    }

    public void ShowMessage(string message, string buttonText, bool showCancelButton = false, Action onButtonClicked = null)
    {
        m_MessagePanel.Show(message, buttonText, showCancelButton, onButtonClicked);
    }

    public void GetUserInput(string message, string buttonText, bool showCancelButton = true, Action<string> onButtonClicked = null)
    {
        m_InputFieldPanel.Show(message, buttonText, showCancelButton, onButtonClicked);
    }

    public void ShowNotification(string message)
    {
        m_NotificationPanel.Show(message);
    }
}
