using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StarAnimateController : MonoBehaviour
{

    [SerializeField]
    private List<Image> starFills;

    [SerializeField]
    private float minScaleValueDependence = .1f;

    [SerializeField]
    private float maxScaleValueDependence = .1f;

    [SerializeField]
    private int popUpCount = 3;

    [SerializeField]
    private float popUpAnimationDuration = 0.1f;

    [SerializeField]
    private int maxStarsFilledCount = 2;

    [SerializeField]
    private float colorFillDuration = 0.03f;

    [SerializeField]
    private float maxColorFillValue = 1;

    public float MinScaleValueDependence { get => minScaleValueDependence; }
    public float MaxScaleValueDependence { get => maxScaleValueDependence; }
    public int PopUpCount { get => popUpCount; }
    public List<Image> StarFills { get => starFills; }
    public float PopUpAnimationDuration { get => popUpAnimationDuration; }
    public float ColorFillDuration { get => colorFillDuration; }
    public float MaxColorFillValue { get => maxColorFillValue; }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        starFills = new List<Image>();
        if (StarFills.Count == 0)
        {
            StarController[] _starFills = GetComponentsInChildren<StarController>().ToArray();

            StarController temp;
            for (int i = 0; i < _starFills.Length - 1; i++)
            {
                for (int j = 0; j < _starFills.Length - i - 1; j++)
                {
                    if (_starFills[j + 1].order < _starFills[j].order)
                    {
                        temp = _starFills[j + 1];
                        _starFills[j + 1] = _starFills[j];
                        _starFills[j] = temp;
                    }
                }
            }

            foreach (var item in _starFills)
            {
                if (maxStarsFilledCount == 0)
                    break;
                StarFills.Add(item.gameObject.GetComponent<Image>());
                maxStarsFilledCount--;
            }
        }
    }

}
