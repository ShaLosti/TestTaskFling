using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BartenderSkillAnimateContoller : MonoBehaviour
{
    [SerializeField]
    private Image bartenderSkillValue;
    [SerializeField]
    private float minBartenderSkillValue = 0f;
    [SerializeField]
    private float maxBartenderSkillValue = .3f;
    [SerializeField]
    private float animateDuration = .03f;

    public Image BartenderSkillValue { get => bartenderSkillValue; set => bartenderSkillValue = value; }
    public float MinBartenderSkillValue { get => minBartenderSkillValue; }
    public float MaxBartenderSkillValue { get => maxBartenderSkillValue; }
    public float AnimateDuration { get => animateDuration; }

    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        if (bartenderSkillValue == null)
            bartenderSkillValue = FindObjectOfType<BartenderValueController>().GetComponent<Image>();
    }
}
