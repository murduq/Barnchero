using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Text hpText;

    private void Awake()
    {
        
    }

    private void Update()
    {
        hpText.text = slider.value.ToString();
    }

    public void SetMaxHP(int hp)
    {
        slider.maxValue = hp;
        SetHP(hp);
    }

    public void SetHP(int hp)
    {
        slider.value = hp;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
