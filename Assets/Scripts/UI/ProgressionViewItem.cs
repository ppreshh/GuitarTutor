using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionViewItem : MonoBehaviour
{
    [SerializeField] private Button m_Button;
    [SerializeField] private TextMeshProUGUI m_ChordNameText;
    [SerializeField] private TextMeshProUGUI m_PositionText;

    private Progression m_Progression;
    private int m_PositionIndex = -1;

    private void Awake()
    {
        m_Button.onClick.AddListener(() =>
        {
            var position = m_Progression.Positions[m_PositionIndex];

            AudioManager.Instance.StrumChord(NoteTools.GetNotesFromPosition(m_Progression.Tuning, position));
        });
    }

    private void OnDestroy()
    {
        m_Button.onClick.RemoveAllListeners();
    }

    public void Setup(Progression progression, int positionIndex)
    {
        m_Progression = progression;
        m_PositionIndex = positionIndex;

        var position = m_Progression.Positions[m_PositionIndex];

        string chordName = ChordFinder.GetChordName(NoteTools.GetNotesFromPosition(m_Progression.Tuning, position));

        string positionText = "";
        foreach (var kvp in position)
        {
            if (kvp.Value == -1)
            {
                positionText += " x ";
            }
            else if (kvp.Value == m_Progression.CapoPosition)
            {
                positionText += " o ";
            }
            else
            {
                positionText += $" <b>{kvp.Value}</b> ";
            }
        }

        m_ChordNameText.text = chordName;
        m_PositionText.text = positionText;
    }
}
