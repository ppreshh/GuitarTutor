using TMPro;
using UnityEngine;

public class BuildVersionItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Text;

    private void Awake()
    {
        m_Text.text = $"v{Application.version}";
    }
}
