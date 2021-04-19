using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void SetValue(float value)
    {
        slider.value = value;
    }

    public void Init(float min, float max)
    {
        slider.minValue = min;
        slider.maxValue = max;
    }
}
