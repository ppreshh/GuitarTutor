using UnityEngine;
using UnityEngine.UI;

public class SafeAreaHandler : MonoBehaviour
{
    [SerializeField] private CanvasScaler m_CanvasScaler;
    [SerializeField] private RectTransform m_MainPanelTopPanel;
    [SerializeField] private RectTransform m_MainPanelScrollViewContent;
    [SerializeField] private RectTransform m_ProgressionsPanelContainer;
    [SerializeField] private RectTransform m_SettingsPanelContainer;

    private const float k_TopPadding = 25f;

    private void Start()
    {
        Rect safeArea = Screen.safeArea;

        float scaleFactor = m_CanvasScaler.scaleFactor;

        float safeAreaTop = safeArea.yMax / scaleFactor;
        float screenHeight = Screen.height / scaleFactor;

        float verticalAdjustment = screenHeight - safeAreaTop;

        if (verticalAdjustment > 0)
        {
            AdjustRectTransformHeight(m_MainPanelTopPanel, verticalAdjustment);

            m_MainPanelScrollViewContent.anchoredPosition = new(m_MainPanelScrollViewContent.anchoredPosition.x, m_MainPanelScrollViewContent.anchoredPosition.y - verticalAdjustment - k_TopPadding);

            AdjustRectTransformTop(m_SettingsPanelContainer, verticalAdjustment);
            AdjustRectTransformTop(m_ProgressionsPanelContainer, verticalAdjustment);
        }
    }

    private void AdjustRectTransformHeight(RectTransform rectTransform, float adjustment)
    {
        rectTransform.sizeDelta = new(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y + adjustment + k_TopPadding);
    }

    private void AdjustRectTransformTop(RectTransform rectTransform, float adjustment)
    {
        rectTransform.anchoredPosition = new(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y - adjustment - k_TopPadding);
    }
}
