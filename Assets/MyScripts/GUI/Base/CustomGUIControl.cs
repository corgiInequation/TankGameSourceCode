using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum E_UseStyle
{
    On,
    off
}

public abstract class CustomGUIControl : MonoBehaviour
{
    //这个是GUI控件基类。主要是有控件位置大小，content, style和是否启用style
    
    public CustomUIPos pos;
    public GUIContent content;
    public GUIStyle style;
    public E_UseStyle useStyle;

    public void DrawGUI()
    {
        switch (useStyle)
        {
            case E_UseStyle.On:
                DrawWithStyle();
                break;
            case E_UseStyle.off:
                DrawWithoutStyle();
                break;
            default:
                break;
        }
    }

    protected abstract void DrawWithStyle();

    protected abstract void DrawWithoutStyle();
    
}
