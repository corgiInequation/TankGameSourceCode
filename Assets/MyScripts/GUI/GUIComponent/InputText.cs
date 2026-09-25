using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputText : CustomGUIControl
{
    public string text;
    private string oldText;
    public event UnityAction<string> TextChanged;
    protected override void DrawWithoutStyle()
    {
        text = GUI.TextField(pos.Rect, text);
        if(text != oldText)
        {
            TextChanged?.Invoke(text);
            oldText = text;
        }
    }

    protected override void DrawWithStyle()
    {
        GUIStyle style = new GUIStyle(GUI.skin.textField);
        style.fontSize = 32;
        text = GUI.TextField(pos.Rect, text, style);
        if (text != oldText)
        {
            TextChanged?.Invoke(text);
            oldText = text;
        }
    }
}
