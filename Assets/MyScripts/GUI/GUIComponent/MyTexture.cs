using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTexture : CustomGUIControl
{
    public Texture tex;
    public ScaleMode scaleMode = ScaleMode.StretchToFill;
    protected override void DrawWithoutStyle()
    {
        GUI.DrawTexture(pos.Rect, tex, scaleMode);
    }

    protected override void DrawWithStyle()
    {
        GUI.DrawTexture(pos.Rect, tex, scaleMode);

    }
}
