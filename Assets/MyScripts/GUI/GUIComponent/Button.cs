using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : CustomGUIControl
{
    public event UnityAction btnEvents;
    protected override void DrawWithoutStyle()
    {
        if (GUI.Button(pos.Rect, content))
        {
            btnEvents?.Invoke();
        }
    }

    protected override void DrawWithStyle()
    {
        if(GUI.Button(pos.Rect, content, style))
        {
            btnEvents?.Invoke();
        }
    }
}
