using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CapoPanel : MonoBehaviour
{
    [SerializeField] private RectTransform m_CapoPanelRectTransform;
    [SerializeField] private CanvasGroup m_CapoPanelCanvasGroup;
    [SerializeField] private Button m_UpButton;
    [SerializeField] private Button m_DownButton;
    [SerializeField] private Button m_UseCapoButton;
    [SerializeField] private TextMeshProUGUI m_UseCapoButtonText;

    private bool m_IsVisible = false;

    private void Start()
    {
        GuitarManager.Instance.OnCapoPositionUpdated += GuitarManager_OnCapoPositionUpdated;

        m_UpButton.onClick.AddListener(() =>
        {
            GuitarManager.Instance.UpdateCapoPosition(GuitarManager.Instance.CapoPosition - 1);
        });

        m_DownButton.onClick.AddListener(() =>
        {
            GuitarManager.Instance.UpdateCapoPosition(GuitarManager.Instance.CapoPosition + 1);
        });

        m_UseCapoButton.onClick.AddListener(() =>
        {
            if (GuitarManager.Instance.CapoPosition > 0)
            {
                GuitarManager.Instance.RemoveCapo();
            }
            else
            {
                GuitarManager.Instance.UseCapo();
            }
        });
    }

    private void OnDestroy()
    {
        GuitarManager.Instance.OnCapoPositionUpdated -= GuitarManager_OnCapoPositionUpdated;

        m_UpButton.onClick.RemoveAllListeners();
        m_DownButton.onClick.RemoveAllListeners();
        m_UseCapoButton.onClick.RemoveAllListeners();
    }

    private void GuitarManager_OnCapoPositionUpdated()
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        bool isUsingCapo = GuitarManager.Instance.CapoPosition > 0;

        m_UseCapoButtonText.text = isUsingCapo ? "Remove Capo" : "Use Capo";
        m_CapoPanelCanvasGroup.interactable = isUsingCapo;

        if (m_IsVisible != isUsingCapo)
        {
            if (m_IsVisible)
            {
                m_CapoPanelCanvasGroup.DOFade(0f, 0.1f);
                m_IsVisible = false;
            }
            else
            {
                m_CapoPanelCanvasGroup.DOFade(1f, 0.1f);
                m_IsVisible = true;
            }
        }

        m_CapoPanelRectTransform.DOLocalMoveY(-145f - ((GuitarManager.Instance.CapoPosition - 1) * 290.91f), 0.2f);
    }
}
