using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuestionEarnedAnimateContoller : MonoBehaviour
{
    [SerializeField]
    private List<Image> questionImages;

    [SerializeField]
    private float minScaleQuestionValueDependence = 0f;

    [SerializeField]
    private float maxScaleQuestionValueDependence = .1f;

    [SerializeField]
    private int popUpCount = 1;

    [SerializeField]
    private float popUpAnimationDuration = .04f;

    [SerializeField]
    private int filledQuestionCount = 0;

    public float MinScaleQuestionValueDependence { get => minScaleQuestionValueDependence; }
    public float MaxScaleQuestionValueDependence { get => maxScaleQuestionValueDependence; }
    public int PopUpCount { get => popUpCount; }
    public List<Image> QuestionImages { get => questionImages; }
    public float PopUpAnimationDuration { get => popUpAnimationDuration; }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        questionImages = new List<Image>();
        foreach (var item in GetComponentsInChildren<Image>())
        {
            if (filledQuestionCount == 0) break;
            questionImages.Add(item);
            filledQuestionCount--;
        }
    }

}
