using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Label : CustomGUIControl
{
    protected override void DrawWithoutStyle()
    {
        GUI.Label(pos.Rect, content);
    }

    protected override void DrawWithStyle()
    {
        GUI.Label(pos.Rect, content, style);
    }
}

