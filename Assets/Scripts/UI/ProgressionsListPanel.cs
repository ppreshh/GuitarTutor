using System;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionsListPanel : Panel
{
    [Header("Progressions List Panel")]
    [SerializeField] private ProgressionsListButton m_ProgressionsListButtonPrefab;
    [SerializeField] private Transform m_ProgressionsListParentTransform;

    private List<ProgressionsListButton> m_ProgressionsListButtons = new();

    public event Action<int> OnProgressionsListButtonClicked;

    public void RefreshUI()
    {
        ClearUI();

        for (int i = 0; i < ProgressionsManager.Instance.Progressions.Count; i++)
        {
            var button = Instantiate(m_ProgressionsListButtonPrefab, m_ProgressionsListParentTransform);
            button.Setup(ProgressionsManager.Instance.Progressions[i].Name, i);

            m_ProgressionsListButtons.Add(button);
        }

        Show(0f);
    }

    private void ClearUI()
    {
        foreach (var button in m_ProgressionsListButtons)
        {
            Destroy(button.gameObject);
        }
        m_ProgressionsListButtons.Clear();
    }
}
