using TMPro;
using UnityEngine;

public class TipsValueController : MonoBehaviour
{
    public TextMeshProUGUI score;
    public int minValue = 0;
    public int maxValue = 0;

    void Start()
    {
        score = GetComponent<TextMeshProUGUI>();
    }
}
