using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class ScoreAnimateController : MonoBehaviour
{
    [SerializeField]
    private CostValueController costValue;
    [SerializeField]
    private TipsValueController tipsValue;
    [SerializeField]
    private FinesValueController finesValue;
    [SerializeField]
    private TotalScoreValueController totalValue;

    public CostValueController CostValue { get => costValue; set => costValue = value; }
    public TipsValueController TipsValue { get => tipsValue; set => tipsValue = value; }
    public FinesValueController FinesValue { get => finesValue; set => finesValue = value; }
    public TotalScoreValueController TotalValue { get => totalValue; set => totalValue = value; }

    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        CostValue = FindObjectOfType<CostValueController>();
        TipsValue = FindObjectOfType<TipsValueController>();
        FinesValue = FindObjectOfType<FinesValueController>();
        TotalValue = FindObjectOfType<TotalScoreValueController>();
    }
}
