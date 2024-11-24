using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Text;
    [SerializeField] private CanvasGroup m_CanvasGroup;
    [SerializeField] private RectTransform m_RectTransform;

    private Coroutine m_ShowCoroutine = null;
    private Tweener m_FadeInTween = null;
    private Tweener m_FadeOutTween = null;

    public void Show(string message)
    {
        StopAll();

        m_CanvasGroup.alpha = 0f;
        m_Text.text = message;
        LayoutRebuilder.ForceRebuildLayoutImmediate(m_RectTransform);

        m_ShowCoroutine = StartCoroutine(ShowCoroutine(2f));
    }

    private void StopAll()
    {
        if (m_ShowCoroutine != null)
        {
            StopCoroutine(m_ShowCoroutine);
            m_ShowCoroutine = null;
        }

        if (m_FadeInTween != null)
        {
            DOTween.Kill(m_FadeInTween);
            m_FadeInTween = null;
        }

        if (m_FadeOutTween != null)
        {
            DOTween.Kill(m_FadeOutTween);
            m_FadeOutTween = null;
        }
    }

    private IEnumerator ShowCoroutine(float timeToFadeOut)
    {
        m_FadeInTween = m_CanvasGroup.DOFade(1f, 0.1f);

        yield return new WaitForSeconds(timeToFadeOut);

        m_FadeOutTween = m_CanvasGroup.DOFade(0f, 0.2f).OnComplete(() =>
        {
            m_ShowCoroutine = null;
        });
    }
}
