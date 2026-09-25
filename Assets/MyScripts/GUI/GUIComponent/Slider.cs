using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum E_SliderType
{
    Horizontal,
    Vertical
}

public class Slider : CustomGUIControl
{
    public float minValue = 0;
    public float maxValue = 10;
    public float nowValue = 0;
    private float oldValue = 0;
    public E_SliderType sliderType = E_SliderType.Horizontal;
    public event UnityAction<float> sliderEvents;
    public GUIStyle thumbStyle;
    protected override void DrawWithoutStyle()
    {
        switch (sliderType)
        {
            case E_SliderType.Horizontal:
                nowValue = GUI.HorizontalSlider(pos.Rect, nowValue, minValue, maxValue);
                break;
            case E_SliderType.Vertical:
                nowValue = GUI.VerticalSlider(pos.Rect, nowValue, minValue, maxValue);
                break;
            default:
                break;
        }
        if (oldValue != nowValue)
        {
            oldValue = nowValue;
            sliderEvents?.Invoke(nowValue);
        }
    }

    protected override void DrawWithStyle()
    {
        switch (sliderType)
        {
            case E_SliderType.Horizontal:
                nowValue = GUI.HorizontalSlider(pos.Rect, nowValue, minValue, maxValue, style, thumbStyle);
                break;
            case E_SliderType.Vertical:
                nowValue = GUI.VerticalSlider(pos.Rect, nowValue, minValue, maxValue, style, thumbStyle);
                break;
            default:
                break;
        }
        if (oldValue != nowValue)
        {
            oldValue = nowValue;
            sliderEvents?.Invoke(nowValue);
        }
    }
}
