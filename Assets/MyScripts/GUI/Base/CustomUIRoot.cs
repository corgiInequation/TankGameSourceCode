using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CustomUIRoot : MonoBehaviour
{
    //如果每一种组件的脚本都添加ExecuteAlways属性，会很麻烦
    //因此，使用一个root对象，组件都为其儿子，之后使用getsoncomponents方法获取其全部子类，之后遍历，调用每个子类的绘制GUi的方法即可。
    private CustomGUIControl[] guis;
    private void Awake()
    {
        guis = GetComponentsInChildren<CustomGUIControl>();
    }
    private void OnGUI()
    {
        guis = GetComponentsInChildren<CustomGUIControl>();
        for(int i = 0; i < guis.Length; i++)
        {
            guis[i].DrawGUI();
        }
    }


}
