using System;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionsListPanel : Panel
{
    [Header("Progressions List Panel")]
    [SerializeField] private ProgressionsListItem m_ProgressionsListItemPrefab;
    [SerializeField] private Transform m_ProgressionsListParentTransform;

    private List<ProgressionsListItem> m_ProgressionsListItems = new();

    private void Start()
    {
        ProgressionsManager.Instance.OnCurrentSelectedProgressionIndexChanged += ProgressionsManager_OnCurrentSelectedProgressionIndexChanged;
    }

    private void OnDestroy()
    {
        ProgressionsManager.Instance.OnCurrentSelectedProgressionIndexChanged -= ProgressionsManager_OnCurrentSelectedProgressionIndexChanged;
    }

    private void ProgressionsManager_OnCurrentSelectedProgressionIndexChanged(int index)
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        ClearItems();

        for (int i = 0; i < ProgressionsManager.Instance.Progressions.Count; i++)
        {
            var item = Instantiate(m_ProgressionsListItemPrefab, m_ProgressionsListParentTransform);
            item.Setup(ProgressionsManager.Instance.Progressions[i].Name, i);

            item.OnSelectedButtonClicked += ProgressionsListButton_OnSelectButtonClicked;

            m_ProgressionsListItems.Add(item);
        }

        if (m_ProgressionsListItems.Count > 0 && ProgressionsManager.Instance.CurrentSelectedProgressionIndex != -1)
        {
            m_ProgressionsListItems[ProgressionsManager.Instance.CurrentSelectedProgressionIndex].SelectButton.SetIsOnWithoutNotify(true);
        }

        Show(0f);
    }

    private void ProgressionsListButton_OnSelectButtonClicked(object sender, ProgressionsListItem.SelectButtonClickedEventArgs e)
    {
        ProgressionsManager.Instance.CurrentSelectedProgressionIndex = e.IsSelecting ? e.Index : -1;
    }

    private void ClearItems()
    {
        foreach (var item in m_ProgressionsListItems)
        {
            item.OnSelectedButtonClicked -= ProgressionsListButton_OnSelectButtonClicked;

            Destroy(item.gameObject);
        }
        m_ProgressionsListItems.Clear();
    }
}
