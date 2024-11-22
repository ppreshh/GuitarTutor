using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] private TMP_InputField m_NotesInputField;
    [SerializeField] private Button m_SubmitButton;
    [SerializeField] private TextMeshProUGUI m_ChordNameText;

    private void Start()
    {
        m_SubmitButton.onClick.AddListener(() =>
        {
            if (string.IsNullOrEmpty(m_NotesInputField.text))
            {
                m_ChordNameText.text = "Invalid Input";
                return;
            }

            List<string> notes = new(m_NotesInputField.text.Split(' '));
            var chordName = ChordFinder.GetChordName(notes);

            m_ChordNameText.text = chordName;
        });
    }
}
