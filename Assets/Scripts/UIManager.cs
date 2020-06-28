using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Star animation")]
    [SerializeField]
    private StarAnimateController starAnimateController;

    [Header("Numbers increase")]
    private ScoreAnimateController scoreAnimateController;

    [Header("Bartender skill")]
    [SerializeField]
    private BartenderSkillAnimateContoller bartenderSkillAnimateContoller;

    [Header("Question earned")]
    [SerializeField]
    private QuestionEarnedAnimateContoller questionEarnedAnimateContoller;

    private IEnumerator coroutine;
    private void Start()
    {
        Initialize();
        coroutine = CompleteLvlUIAnimation();
        StopAllCoroutines();
        StartCoroutine(coroutine);
    }

    private void Initialize()
    {
        if (starAnimateController == null)
            starAnimateController = FindObjectOfType<StarAnimateController>();
        if (scoreAnimateController == null)
            scoreAnimateController = FindObjectOfType<ScoreAnimateController>();
        if (bartenderSkillAnimateContoller == null)
            bartenderSkillAnimateContoller = FindObjectOfType<BartenderSkillAnimateContoller>();
        if (questionEarnedAnimateContoller == null)
            questionEarnedAnimateContoller = FindObjectOfType<QuestionEarnedAnimateContoller>();
    }

    IEnumerator CompleteLvlUIAnimation()
    {
        float t = 0;
        while (t < 1)
        {
            GetComponent<RectTransform>().localPosition = Vector3.Lerp(GetComponent<RectTransform>().localPosition,
                Vector3.zero, t * t);

            t += Time.deltaTime / 1f;
            yield return null;
        }

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
        foreach (Image starFill in starAnimateController.StarFills)
        {
            yield return StartCoroutine(ColorFill(starFill, starFill.fillAmount,
                starAnimateController.MaxColorFillValue, starAnimateController.ColorFillDuration));

            StartCoroutine(PopUp(starFill.transform.parent.parent,
                new Vector3(starFill.transform.localScale.x - starAnimateController.MinScaleValueDependence,
                starFill.transform.localScale.y - starAnimateController.MinScaleValueDependence,
                starFill.transform.localScale.z),
                new Vector3(starFill.transform.localScale.x + starAnimateController.MaxScaleValueDependence,
                starFill.transform.localScale.y + starAnimateController.MaxScaleValueDependence,
                starFill.transform.localScale.z), starAnimateController.PopUpCount,
                starAnimateController.PopUpAnimationDuration));
            yield return new WaitForSeconds(.2f);
        }
    }
    IEnumerator AnimateScore()
    {
        StartCoroutine(IncreaseNumber(scoreAnimateController.CostValue.score,
            scoreAnimateController.CostValue.minValue, scoreAnimateController.CostValue.maxValue));
        StartCoroutine(IncreaseNumber(scoreAnimateController.TipsValue.score,
             scoreAnimateController.TipsValue.minValue, scoreAnimateController.TipsValue.maxValue));
        StartCoroutine(IncreaseNumber(scoreAnimateController.FinesValue.score,
            scoreAnimateController.FinesValue.minValue, scoreAnimateController.FinesValue.maxValue));
        yield return null;
    }
    IEnumerator AnimateBartenderSkill()
    {
        yield return StartCoroutine(ColorFill(bartenderSkillAnimateContoller.BartenderSkillValue,
            bartenderSkillAnimateContoller.MinBartenderSkillValue,
            bartenderSkillAnimateContoller.MaxBartenderSkillValue,
            bartenderSkillAnimateContoller.AnimateDuration));
    }
    IEnumerator AnimateQuestions()
    {
        foreach (Image question in questionEarnedAnimateContoller.QuestionImages)
        {
            StartCoroutine(ChangeRGBChannel(question));

            yield return StartCoroutine(PopUp(question.transform,
                new Vector3(question.transform.localScale.x - questionEarnedAnimateContoller.MinScaleQuestionValueDependence,
                question.transform.localScale.y - questionEarnedAnimateContoller.MinScaleQuestionValueDependence,
                question.transform.localScale.z),
                new Vector3(question.transform.localScale.x + questionEarnedAnimateContoller.MaxScaleQuestionValueDependence,
                question.transform.localScale.y + questionEarnedAnimateContoller.MaxScaleQuestionValueDependence,
                question.transform.localScale.z), questionEarnedAnimateContoller.PopUpCount,
                questionEarnedAnimateContoller.PopUpAnimationDuration));
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
    IEnumerator IncreaseNumber(TextMeshProUGUI score, int minValue, int maxValue)
    {
        score.text = minValue.ToString();
        int sign = maxValue < 0 ? -1 : 1;
        while (minValue != maxValue)
        {
            minValue += sign;
            score.text = minValue.ToString();
            scoreAnimateController.TotalValue.score.text = (Int32.Parse(scoreAnimateController.TotalValue.score.text) + sign).ToString();
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
