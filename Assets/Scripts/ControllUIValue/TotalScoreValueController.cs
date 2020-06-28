using System;
using TMPro;
using UnityEngine;

public class TotalScoreValueController : MonoBehaviour
{
    public TextMeshProUGUI score;

    void Start()
    {
        score = GetComponent<TextMeshProUGUI>();
    }
}
