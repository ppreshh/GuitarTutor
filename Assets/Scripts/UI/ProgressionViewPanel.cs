using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionViewPanel : Panel
{
    [Header("Progression View Panel")]
    [SerializeField] private TextMeshProUGUI m_NameText;
    [SerializeField] private TextMeshProUGUI m_TuningText;
    [SerializeField] private TextMeshProUGUI m_CapoText;
    [SerializeField] private Button m_BackButton;
    [SerializeField] private ProgressionViewItem m_ProgressionViewItemPrefab;
    [SerializeField] private Transform m_ProgressionViewItemsParent;

    private List<ProgressionViewItem> m_ProgressionViewItems = new();

    public event Action OnBackButtonClicked;

    protected override void Initialize()
    {
        m_BackButton.onClick.AddListener(() => OnBackButtonClicked?.Invoke());

        base.Initialize();
    }

    protected override void CleanUp()
    {
        m_BackButton.onClick.RemoveAllListeners();

        base.CleanUp();
    }

    public void Show(Progression progression)
    {
        ClearItems();

        m_NameText.text = progression.Name;
        m_TuningText.text = "Tuning: " + progression.Tuning.ToString();
        m_CapoText.text = "Capo Position: " + (progression.CapoPosition == 0 ? " -- " : "Fret " + progression.CapoPosition.ToString());

        for (int i = 0; i < progression.Positions.Count; i++)
        {
            var item = Instantiate(m_ProgressionViewItemPrefab, m_ProgressionViewItemsParent);
            item.Setup(progression, i);

            m_ProgressionViewItems.Add(item);
        }

        Show(0.1f);
    }

    private void ClearItems()
    {
        foreach (var item in m_ProgressionViewItems)
        {
            Destroy(item.gameObject);
        }
        m_ProgressionViewItems.Clear();
    }
}
