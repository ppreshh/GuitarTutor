using DG.Tweening;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private CanvasGroup m_CanvasGroup;
    [Space(5)]
    [SerializeField] private bool m_StartVisible = true;

    private bool m_IsVisible = true;

    /// <summary>
    /// Runs on Awake()
    /// </summary>
    protected virtual void Initialize()
    {
        if (!m_StartVisible) Hide(0f);
    }

    private void Awake()
    {
        Initialize();
    }

    public void SetInteractable(bool interactable)
    {
        m_CanvasGroup.interactable = interactable;
    }

    public void Show()
    {
        if (m_IsVisible) return;

        m_CanvasGroup.DOFade(1f, 0.1f);
        m_CanvasGroup.interactable = true;
        m_CanvasGroup.blocksRaycasts = true;

        m_IsVisible = true;
    }

    public void Hide(float fadeTime = 0.1f)
    {
        if (!m_IsVisible) return;

        m_CanvasGroup.DOFade(0f, fadeTime);
        m_CanvasGroup.interactable = false;
        m_CanvasGroup.blocksRaycasts = false;

        m_IsVisible = false;
    }
}
