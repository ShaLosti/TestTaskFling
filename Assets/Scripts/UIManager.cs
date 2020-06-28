using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Star animation")]
    [SerializeField]
    private List<Image> starFills;

    [SerializeField]
    private float minScaleValueDependence = .1f;

    [SerializeField]
    private float maxScaleValueDependence = .1f;

    [SerializeField]
    private int popUpCount = 3;

    [Header("Numbers increase")]
    [SerializeField]
    private TextMeshProUGUI costValue;
    [SerializeField]
    private TextMeshProUGUI tipsValue;
    [SerializeField]
    private TextMeshProUGUI finesValue;
    [SerializeField]
    private TextMeshProUGUI totalValue;

    [Header("Numbers increase")]
    [SerializeField]
    private Image bartenderSkillValue;
    [SerializeField]
    private float minBartenderSkillValue = .1f;
    [SerializeField]
    private float maxBartenderSkillValue = .1f;

    [Header("Question earned")]
    [SerializeField]
    private Image[] questionsImage;
    [SerializeField]
    private float minScaleQuestionValueDependence = .1f;
    [SerializeField]
    private float maxScaleQuestionValueDependence = .1f;
    [SerializeField]
    private int popUpQuestion = 1;


    private IEnumerator coroutine;
    private void Start()
    {
        Initialize();
        coroutine = CompleteLvlUIAnimation();
    }

    private void Initialize()
    {
        if (starFills == null)
        {
            StarController[] _starFills = FindObjectsOfType<StarController>().ToArray();
            _starFills.OrderBy(i => i.order);
            foreach (var item in _starFills)
            {
                starFills.Add(item.gameObject.GetComponent<Image>());
            }
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StopAllCoroutines();
            StartCoroutine(coroutine);
        }
    }

    IEnumerator CompleteLvlUIAnimation()
    {

        coroutine = AnimateStars();
        yield return StartCoroutine(coroutine);

        coroutine = AnimateScore();
        yield return StartCoroutine(coroutine);

        yield return new WaitForSeconds(1f);
        coroutine = AnimateBartenderSkill();
        yield return StartCoroutine(coroutine);

        yield return new WaitForSeconds(.3f);
        coroutine = AnimateQuestions();
        yield return StartCoroutine(coroutine);
    }

    IEnumerator AnimateStars()
    {
        foreach (Image starFill in starFills)
        {
            yield return StartCoroutine(ColorFill(starFill, starFill.fillAmount));

            StartCoroutine(PopUp(starFill.transform.parent.parent,
                new Vector3(starFill.transform.localScale.x - minScaleValueDependence,
                starFill.transform.localScale.y - minScaleValueDependence,
                starFill.transform.localScale.z),
                new Vector3(starFill.transform.localScale.x + maxScaleValueDependence,
                starFill.transform.localScale.y + maxScaleValueDependence,
                starFill.transform.localScale.z), popUpCount));
            yield return new WaitForSeconds(.2f);
        }
    }
    IEnumerator AnimateScore()
    {
        StartCoroutine(IncreaseNumber(costValue, 0, 20));
        StartCoroutine(IncreaseNumber(tipsValue, 0, 20));
        StartCoroutine(IncreaseNumber(finesValue, 0, 20));
        yield return null;
    }
    IEnumerator AnimateBartenderSkill()
    {
        yield return StartCoroutine(ColorFill(bartenderSkillValue, minBartenderSkillValue, maxBartenderSkillValue));
    }
    IEnumerator AnimateQuestions()
    {
        foreach (Image question in questionsImage)
        {
            StartCoroutine(ChangeRGBChannel(question));

            yield return StartCoroutine(PopUp(question.transform,
                new Vector3(question.transform.localScale.x - minScaleQuestionValueDependence,
                question.transform.localScale.y - minScaleQuestionValueDependence,
                question.transform.localScale.z),
                new Vector3(question.transform.localScale.x + maxScaleQuestionValueDependence,
                question.transform.localScale.y + maxScaleQuestionValueDependence,
                question.transform.localScale.z), popUpQuestion));
        }
    }
    IEnumerator ColorFill(Image UIObject, float minColorFill = 0f, float maxColorFill = 1f, float duration = .03f)
    {
        UIObject.fillAmount = minColorFill;
        while (UIObject.fillAmount < maxColorFill)
        {
            UIObject.fillAmount += duration;
            yield return null;
        }
    }
    IEnumerator PopUp(Transform UIObject, Vector3 minScale, Vector3 maxScale, int popUpCount = 1, float animationDuration = .1f)
    {
        Vector3 newScale;
        float t;
        Vector3 maxScaleDependence;
        Vector3 minScaleDependence;

        for (int i = 1; i <= popUpCount; i++)
        {
            t = 0;

            maxScaleDependence = (maxScale - UIObject.transform.localScale) / i;

            newScale = new Vector3(UIObject.transform.localScale.x + maxScaleDependence.x,
                UIObject.transform.localScale.y + maxScaleDependence.y,
                UIObject.transform.localScale.z);

            while (t < 1)
            {
                UIObject.transform.localScale = Vector3.Lerp(UIObject.transform.localScale, newScale, t * t);
                t += Time.deltaTime / animationDuration;
                yield return null;
            }

            t = 0;

            minScaleDependence = (UIObject.transform.localScale - minScale) / i;

            newScale = new Vector3(UIObject.transform.localScale.x - minScaleDependence.x,
                UIObject.transform.localScale.y - minScaleDependence.y,
                UIObject.transform.localScale.z);

            while (t < 1)
            {
                UIObject.transform.localScale = Vector3.Lerp(UIObject.transform.localScale, newScale, t * t);
                t += Time.deltaTime / animationDuration;
                yield return null;
            }
        }
    }
    IEnumerator IncreaseNumber(TextMeshProUGUI score, int minValue, int maxValue, int duration = 1)
    {
        score.text = minValue.ToString();
        for (; minValue <= maxValue; minValue += duration)
        {
            score.text = minValue.ToString();
            totalValue.text = (Int32.Parse(totalValue.text) + duration).ToString();
            yield return new WaitForSeconds(.03f);
        }
    }
    IEnumerator ChangeRGBChannel(Image UIObject, int targetRGBChanel = 255, float duration = .5f)
    {
        Color newColor = UIObject.color;
        while (newColor.r < targetRGBChanel)
        {
            newColor.r += duration;
            newColor.g += duration;
            newColor.b += duration;
            UIObject.color = newColor;
            yield return null;
        }

    }
}
