using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionsListButton : MonoBehaviour
{
    [SerializeField] private Button m_Button;
    [SerializeField] private TextMeshProUGUI m_Text;

    private int m_Index = -1;

    public event Action<int> OnClicked;

    private void Awake()
    {
        m_Button.onClick.AddListener(() =>
        {
            OnClicked?.Invoke(m_Index);
        });
    }

    private void OnDestroy()
    {
        m_Button.onClick.RemoveAllListeners();
    }

    public void Setup(string text, int index)
    {
        m_Text.text = text;
        m_Index = index;
    }
}
