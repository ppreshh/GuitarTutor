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
        m_BackButton.onClick.AddListener(() => { SlideOut(); });

        base.Initialize();
    }

    private void Start()
    {
        var canvas = m_RectTransform.GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Canvas not found");
            return;
        }

        // Get the scale factor from the Canvas Scaler
        float canvasScaleFactor = canvas.scaleFactor;

        // Get the screen width in world space
        float screenWidthInCanvasUnits = Screen.width / canvasScaleFactor;

        // Calculate the initial off-screen position based on side
        float posX = m_Side == Side.Left ? -1f * screenWidthInCanvasUnits : screenWidthInCanvasUnits;

        // Save the original position of the panel
        var localPos = m_RectTransform.localPosition;
        m_RectTransform.sizeDelta = new Vector2(screenWidthInCanvasUnits, m_RectTransform.sizeDelta.y);
        m_RectTransform.localPosition = new Vector3(posX, localPos.y, localPos.z);

        m_OriginalLocalPosX = m_RectTransform.localPosition.x;
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
