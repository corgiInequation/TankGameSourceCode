using UnityEngine;
using UnityEngine.Events;

public class InputPassword : CustomGUIControl
{
    public string text;
    public char passwordStyle = '*';
    private string oldText;
    public event UnityAction<string> TextChanged;
    protected override void DrawWithoutStyle()
    {
        text = GUI.PasswordField(pos.Rect, text, passwordStyle);
        if (text != oldText)
        {
            TextChanged?.Invoke(text);
            oldText = text;
        }
    }

    protected override void DrawWithStyle()
    {
        text = GUI.PasswordField(pos.Rect, text, passwordStyle, style);
        if (text != oldText)
        {
            TextChanged?.Invoke(text);
            oldText = text;
        }
    }
}
