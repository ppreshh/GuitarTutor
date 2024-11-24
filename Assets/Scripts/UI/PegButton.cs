using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PegButton : MonoBehaviour
{
    [SerializeField] private int m_StringNumber;
    [SerializeField] private Button m_Button;
    [SerializeField] private TextMeshProUGUI m_Text;

    public event EventHandler<ClickedEventArgs> OnClicked;
    public class ClickedEventArgs : EventArgs
    {
        public int StringNumber;
    }

    private void Awake()
    {
        m_Button.onClick.AddListener(() =>
        {
            OnClicked?.Invoke(this, new ClickedEventArgs { StringNumber = m_StringNumber });
        });
    }

    private void Start()
    {
        GuitarManager.Instance.OnTuningUpdated += GuitarManager_OnTuningUpdated;

        UpdateVisuals();
    }

    private void OnDestroy()
    {
        GuitarManager.Instance.OnTuningUpdated -= GuitarManager_OnTuningUpdated;

        m_Button.onClick.RemoveAllListeners();
    }

    private void GuitarManager_OnTuningUpdated()
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        m_Text.text = GuitarManager.Instance.Tuning[m_StringNumber].ToString();
    }
}
