using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Toggle : CustomGUIControl
{
    public bool isSel ;
    private bool isOldSel;
    public event UnityAction<bool> toggleEvents;
    protected override void DrawWithoutStyle() 
    {
        isSel = GUI.Toggle(pos.Rect, isSel, content);
        if(isOldSel != isSel)
        {
            toggleEvents?.Invoke(isSel);
            isOldSel = isSel;
        }
    }
    protected override void DrawWithStyle()
    {
        isSel = GUI.Toggle(pos.Rect, isSel, content, style);
        if (isOldSel != isSel)
        {
            toggleEvents?.Invoke(isSel);
            isOldSel = isSel;
        }
    }
}
