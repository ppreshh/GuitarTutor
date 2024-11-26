using System;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionsListPanel : Panel
{
    [Header("Progressions List Panel")]
    [SerializeField] private ProgressionsListItem m_ProgressionsListItemPrefab;
    [SerializeField] private Transform m_ProgressionsListParentTransform;

    private List<ProgressionsListItem> m_ProgressionsListItems = new();

    public event Action<int> OnProgressionItemEditButtonClicked;

    private void Start()
    {
        ProgressionsManager.Instance.OnCurrentSelectedProgressionIndexChanged += ProgressionsManager_OnCurrentSelectedProgressionIndexChanged;
    }

    protected override void CleanUp()
    {
        ProgressionsManager.Instance.OnCurrentSelectedProgressionIndexChanged -= ProgressionsManager_OnCurrentSelectedProgressionIndexChanged;

        base.CleanUp();
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

            item.OnSelectedButtonClicked += ProgressionsListItem_OnSelectButtonClicked;
            item.OnEditButtonClicked += ProgressionsListItem_OnEditButtonClicked;
            item.OnDeleteButtonClicked += ProgressionListItem_OnDeleteButtonClicked;

            m_ProgressionsListItems.Add(item);
        }

        if (m_ProgressionsListItems.Count > 0 && ProgressionsManager.Instance.CurrentSelectedProgressionIndex != -1)
        {
            m_ProgressionsListItems[ProgressionsManager.Instance.CurrentSelectedProgressionIndex].SelectButton.SetIsOnWithoutNotify(true);
        }

        Show(0f);
    }

    private void ProgressionListItem_OnDeleteButtonClicked(int index)
    {
        UIManager.Instance.ShowMessage($"Are you sure you want to delete progression <b>{ProgressionsManager.Instance.Progressions[index].Name}</b>?", "Yes", true, () =>
        {
            ProgressionsManager.Instance.DeleteProgression(index);
            RefreshUI();
        });
    }

    private void ProgressionsListItem_OnEditButtonClicked(int index)
    {
        OnProgressionItemEditButtonClicked?.Invoke(index);
    }

    private void ProgressionsListItem_OnSelectButtonClicked(object sender, ProgressionsListItem.SelectButtonClickedEventArgs e)
    {
        ProgressionsManager.Instance.CurrentSelectedProgressionIndex = e.IsSelecting ? e.Index : -1;
    }

    private void ClearItems()
    {
        foreach (var item in m_ProgressionsListItems)
        {
            item.OnSelectedButtonClicked -= ProgressionsListItem_OnSelectButtonClicked;
            item.OnEditButtonClicked -= ProgressionsListItem_OnEditButtonClicked;
            item.OnDeleteButtonClicked -= ProgressionListItem_OnDeleteButtonClicked;

            Destroy(item.gameObject);
        }
        m_ProgressionsListItems.Clear();
    }
}
