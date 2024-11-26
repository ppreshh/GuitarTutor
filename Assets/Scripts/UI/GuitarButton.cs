using DG.Tweening;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuitarButton : MonoBehaviour
{
    [SerializeField] private Button m_Button;
    [SerializeField] private CanvasGroup m_CanvasGroup;
    [SerializeField] private TextMeshProUGUI m_Text;

    private int m_StringNumber;
    private int m_FretNumber;

    private bool m_IsOn = false;
    public bool IsOn { get => m_IsOn; }

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        GuitarManager.Instance.OnCurrentPositionUpdated += GuitarManager_OnCurrentPositionUpdated;

        m_Button.onClick.AddListener(() =>
        {
            GuitarManager.Instance.UpdateCurrentPosition(m_StringNumber, m_FretNumber, m_IsOn);
        });

        if (m_FretNumber == 0)
        {
            UpdateVisuals();
        }
    }

    private void OnDestroy()
    {
        GuitarManager.Instance.OnCurrentPositionUpdated -= GuitarManager_OnCurrentPositionUpdated;

        m_Button.onClick.RemoveAllListeners();
    }

    private void GuitarManager_OnCurrentPositionUpdated()
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        bool shouldBeOn = GuitarManager.Instance.CurrentPosition[m_StringNumber] == m_FretNumber;
        if (shouldBeOn != m_IsOn)
        {
            m_IsOn = shouldBeOn;

            m_CanvasGroup.DOFade(m_IsOn ? 1f : 0f, 0.2f);
        }

        if (GuitarManager.Instance.CurrentPosition[m_StringNumber] == m_FretNumber)
        {
            m_Text.text = NoteTools.GetNote(GuitarManager.Instance.Tuning.Settings[m_StringNumber], m_FretNumber).ToString();
        }
    }

    private void Initialize()
    {
        string parentName = transform.parent.gameObject.name;
        if (parentName[parentName.Length - 1] == ')')
        {
            string pattern = @"\((\d+)\)";

            Match match = Regex.Match(parentName, pattern);

            m_StringNumber = int.Parse(match.Groups[1].Value) + 1;
        }
        else
        {
            m_StringNumber = 1;
        }

        if (gameObject.name[gameObject.name.Length - 1] == ')')
        {
            string pattern = @"\((\d+)\)";

            Match match = Regex.Match(gameObject.name, pattern);

            m_FretNumber = int.Parse(match.Groups[1].Value);
        }
        else
        {
            m_FretNumber = 0;
        }
    }
}
