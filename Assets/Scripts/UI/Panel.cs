using UnityEngine;

public class Panel : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private CanvasGroup m_CanvasGroup;

    public void SetInteractable(bool interactable)
    {
        m_CanvasGroup.interactable = interactable;
    }
}
