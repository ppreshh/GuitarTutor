using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionsListItem : MonoBehaviour
{
    [SerializeField] private ToggleButton m_SelectButton;
    public ToggleButton SelectButton { get => m_SelectButton; }

    [SerializeField] private TextMeshProUGUI m_Text;
    [SerializeField] private Button m_EditButton;

    private int m_Index = -1;
    public int Index { get =>  m_Index; }

    public event EventHandler<SelectButtonClickedEventArgs> OnSelectedButtonClicked;
    public class SelectButtonClickedEventArgs : EventArgs
    {
        public bool IsSelecting;
        public int Index;
    }

    private void Awake()
    {
        m_SelectButton.OnValueChanged += SelectButton_OnValueChanged;
    }

    private void OnDestroy()
    {
        m_SelectButton.OnValueChanged -= SelectButton_OnValueChanged;
    }

    private void SelectButton_OnValueChanged(bool isOn)
    {
        OnSelectedButtonClicked?.Invoke(this, new SelectButtonClickedEventArgs { IsSelecting = isOn, Index = m_Index });
    }

    public void Setup(string text, int index)
    {
        m_Text.text = text;
        m_Index = index;
    }
}
