using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SlideInPanel : Panel
{
    [Header("Slide In Panel")]
    [SerializeField] private Side m_Side;
    [SerializeField] private RectTransform m_RectTransform;
    [SerializeField] private Button m_BackButton;

    private enum Side
    {
        Left,
        Right
    }

    private float m_OriginalLocalPosX;

    private bool m_IsVisible = false;

    private void Awake()
    {
        var width = Screen.width;
        float posX = m_Side == Side.Left ? -1f * width : width;
        var localPos = m_RectTransform.localPosition;

        m_RectTransform.sizeDelta = new(width, m_RectTransform.sizeDelta.y);
        m_RectTransform.localPosition = new(posX, localPos.y, localPos.z);

        m_OriginalLocalPosX = m_RectTransform.localPosition.x;

        m_BackButton.onClick.AddListener(() => { Hide(); });
    }

    public void Show()
    {
        if (m_IsVisible) return;

        m_RectTransform.DOLocalMoveX(0f, 0.2f).SetEase(Ease.InOutSine);
        m_IsVisible = true;
    }

    public void Hide()
    {
        if (!m_IsVisible) return;

        m_RectTransform.DOLocalMoveX(m_OriginalLocalPosX, 0.2f).SetEase(Ease.InOutSine);
        m_IsVisible = false;
    }
}
