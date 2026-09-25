using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_AlignmentType
{
    LeftUp,
    LeftCenter,
    LeftDown,
    MidUp,
    MidCenter,
    MidDown,
    RightUp,
    RightCenter,
    RightDown
}

[System.Serializable]
public class CustomUIPos 
{
    //这个基类主要是用于确定组件在屏幕中的坐标
    //可调节的东西为：相对于屏幕的九宫格的位置，相对于组件九宫格的位置，偏移位置，组件大小
    public E_AlignmentType screenPos = E_AlignmentType.MidCenter;
    public E_AlignmentType componentPos = E_AlignmentType.MidCenter;
    public Vector2 offsetPos;
    public float width = 100;
    public float height = 100;

    private Vector2 screenOffset;
    private Vector2 componentOffset;

    private Rect rPos = new Rect(0, 0, 100, 100);

    private void CountScreenOffset(E_AlignmentType screenPos)
    {
        switch (screenPos)
        {
            case E_AlignmentType.LeftUp:
                screenOffset.x = 0;
                screenOffset.y = 0;
                break;
            case E_AlignmentType.LeftCenter:
                screenOffset.x = 0;
                screenOffset.y = Screen.height/2;
                break;
            case E_AlignmentType.LeftDown:
                screenOffset.x = 0;
                screenOffset.y = Screen.height;
                break;
            case E_AlignmentType.MidUp:
                screenOffset.x = Screen.width/2;
                screenOffset.y = 0;
                break;
            case E_AlignmentType.MidCenter:
                screenOffset.x = Screen.width/2;
                screenOffset.y = Screen.height / 2;
                break;
            case E_AlignmentType.MidDown:
                screenOffset.x = Screen.width/2;
                screenOffset.y = Screen.height;
                break;
            case E_AlignmentType.RightUp:
                screenOffset.x = Screen.width;
                screenOffset.y = 0;
                break;
            case E_AlignmentType.RightCenter:
                screenOffset.x = Screen.width;
                screenOffset.y = Screen.height / 2;
                break;
            case E_AlignmentType.RightDown:
                screenOffset.x = Screen.width;
                screenOffset.y = Screen.height;
                break;
            default:
                break;
        }
    }
    private void CountComponentOffset(E_AlignmentType componentPos)
    {
        switch (componentPos)
        {
            case E_AlignmentType.LeftUp:
                componentOffset.x = 0;
                componentOffset.y = 0;
                break;
            case E_AlignmentType.LeftCenter:
                componentOffset.x = 0;
                componentOffset.y = -height / 2;
                break;
            case E_AlignmentType.LeftDown:
                componentOffset.x = 0;
                componentOffset.y = -height;
                break;
            case E_AlignmentType.MidUp:
                componentOffset.x = -width / 2;
                componentOffset.y = 0;
                break;
            case E_AlignmentType.MidCenter:
                componentOffset.x = -width / 2;
                componentOffset.y = -height / 2;
                break;
            case E_AlignmentType.MidDown:
                componentOffset.x = -width / 2;
                componentOffset.y = -height;
                break;
            case E_AlignmentType.RightUp:
                componentOffset.x = -width;
                componentOffset.y = 0;
                break;
            case E_AlignmentType.RightCenter:
                componentOffset.x = -width;
                componentOffset.y = -height / 2;
                break;
            case E_AlignmentType.RightDown:
                componentOffset.x = -width;
                componentOffset.y = -height;
                break;
            default:
                break;
        }
    }

    public Rect Rect
    {
        get
        {
            CountComponentOffset(componentPos);
            CountScreenOffset(screenPos);
            rPos = new Rect(screenOffset.x + componentOffset.x + offsetPos.x, screenOffset.y + componentOffset.y + offsetPos.y, width, height);
            return rPos;
        }
    }

}
