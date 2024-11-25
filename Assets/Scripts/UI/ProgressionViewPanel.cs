using System;
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
        m_NameText.text = progression.Name;
        m_TuningText.text = "Tuning: " + progression.Tuning.ToString();
        m_CapoText.text = "Capo Position: " + (progression.CapoPosition == 0 ? " -- " : "Fret " + progression.CapoPosition.ToString());

        Show(0.1f);
    }
}
