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
    [SerializeField] private Button m_DeleteButton;

    private int m_Index = -1;
    public int Index { get =>  m_Index; }

    public event EventHandler<SelectButtonClickedEventArgs> OnSelectedButtonClicked;
    public class SelectButtonClickedEventArgs : EventArgs
    {
        public bool IsSelecting;
        public int Index;
    }

    public event Action<int> OnEditButtonClicked;
    public event Action<int> OnDeleteButtonClicked;

    private void Awake()
    {
        m_SelectButton.OnValueChanged += SelectButton_OnValueChanged;

        m_EditButton.onClick.AddListener(() => OnEditButtonClicked?.Invoke(m_Index));
        m_DeleteButton.onClick.AddListener(() => OnDeleteButtonClicked?.Invoke(m_Index));
    }

    private void OnDestroy()
    {
        m_SelectButton.OnValueChanged -= SelectButton_OnValueChanged;

        m_EditButton.onClick.RemoveAllListeners();
        m_DeleteButton.onClick.RemoveAllListeners();
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
