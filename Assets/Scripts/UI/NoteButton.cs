using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteButton : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private int m_StringNumber;

    [Header("UI Elements")]
    [SerializeField] private Button m_Button;
    [SerializeField] private TextMeshProUGUI m_Text;
    [SerializeField] private Image m_BorderImage;

    private NoteWithOctave? m_Note = null;

    private void Awake()
    {
        m_Button.onClick.AddListener(() =>
        {
            if (!m_Note.HasValue) return;

            AudioManager.Instance.PlayNote(m_StringNumber, m_Note.Value);
        });
    }

    private void OnDestroy()
    {
        m_Button.onClick.RemoveAllListeners();
    }

    public void SetNote(NoteWithOctave? note)
    {
        if (note.HasValue)
        {
            m_Note = note.Value;
            m_Text.text = note.Value.ToString();
            m_BorderImage.SetAlpha(0.8f);
        }
        else
        {
            m_Note = null;
            m_Text.text = "-";
            m_BorderImage.SetAlpha(0.2f);
        }
    }
}
