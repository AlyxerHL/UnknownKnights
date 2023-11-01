using TMPro;
using UnityEngine;

public class SkillQuote : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    public string Text
    {
        get => text.text;
        set => text.text = value;
    }
}
