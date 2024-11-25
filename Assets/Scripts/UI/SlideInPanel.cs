using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class SlideInPanel : Panel
{
    [Header("Slide In Panel")]
    [SerializeField] private Side m_Side;
    [SerializeField] private RectTransform m_RectTransform;
    [SerializeField] private Button m_BackButton;

    protected abstract void SetupUIBeforeSlideIn();

    private enum Side
    {
        Left,
        Right
    }

    private float m_OriginalLocalPosX;
    private bool m_IsSlidIn = false;

    public event Action OnSlideOutCompleted;

    protected override void Initialize()
    {
        var width = Screen.width;
        float posX = m_Side == Side.Left ? -1f * width : width;
        var localPos = m_RectTransform.localPosition;

        m_RectTransform.sizeDelta = new(width, m_RectTransform.sizeDelta.y);
        m_RectTransform.localPosition = new(posX, localPos.y, localPos.z);

        m_OriginalLocalPosX = m_RectTransform.localPosition.x;

        m_BackButton.onClick.AddListener(() => { SlideOut(); });

        base.Initialize();
    }

    protected override void CleanUp()
    {
        m_BackButton.onClick.RemoveAllListeners();

        base.CleanUp();
    }

    public void SlideIn()
    {
        if (m_IsSlidIn) return;

        SetupUIBeforeSlideIn();

        m_RectTransform.DOLocalMoveX(0f, 0.2f).SetEase(Ease.InOutSine);
        m_IsSlidIn = true;
    }

    public void SlideOut()
    {
        if (!m_IsSlidIn) return;

        m_RectTransform.DOLocalMoveX(m_OriginalLocalPosX, 0.2f).SetEase(Ease.InOutSine).OnComplete(() => OnSlideOutCompleted?.Invoke());
        m_IsSlidIn = false;
    }
}
