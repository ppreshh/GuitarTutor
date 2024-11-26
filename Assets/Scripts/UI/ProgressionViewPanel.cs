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
    private Progression m_Progression = null;

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

        if (m_Progression != null)
        {
            m_Progression.OnProgressionUpdated -= Progression_OnProgressionUpdated;
        }

        m_Progression = progression;
        m_Progression.OnProgressionUpdated += Progression_OnProgressionUpdated;

        m_NameText.text = m_Progression.Name;
        m_TuningText.text = "Tuning: " + m_Progression.Tuning.ToString();
        m_CapoText.text = "Capo Position: " + (m_Progression.CapoPosition == 0 ? " -- " : "Fret " + m_Progression.CapoPosition.ToString());

        SetupItems();

        Show(0.1f);
    }

    private void SetupItems()
    {
        for (int i = 0; i < m_Progression.Positions.Count; i++)
        {
            var item = Instantiate(m_ProgressionViewItemPrefab, m_ProgressionViewItemsParent);
            item.Setup(m_Progression, i);

            m_ProgressionViewItems.Add(item);
        }
    }

    private void ClearItems()
    {
        foreach (var item in m_ProgressionViewItems)
        {
            Destroy(item.gameObject);
        }
        m_ProgressionViewItems.Clear();
    }

    private void Progression_OnProgressionUpdated()
    {
        RefreshItems();
    }

    private void RefreshItems()
    {
        ClearItems();
        SetupItems();
    }
}
