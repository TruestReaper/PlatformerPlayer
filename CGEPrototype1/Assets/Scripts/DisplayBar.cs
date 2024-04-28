using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
// Import the UnityEngine.UI library
using UnityEngine.UI;

public class DisplayBar : MonoBehaviour
{
    // slider for the health bar
    public Slider slider;

    // gradient for the health bar
    public Gradient gradient;

    // image for fill of health bar
    public Image fill;

    public void SetValue(float value)
    {
        // set value of the slider
        slider.value = value;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    // function to set the max value of slider
    public void SetMaxValue(float value)
    {
        // set the max value of the slider
        slider.maxValue = value;

        // set the current value of the slider to max value
        slider.value = value;

        fill.color = gradient.Evaluate(1f);
    }
}
