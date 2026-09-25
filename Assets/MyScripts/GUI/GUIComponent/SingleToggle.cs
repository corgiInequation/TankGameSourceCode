using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleToggle : MonoBehaviour
{
    //单选框的实现方法为，使用一个组件，去管理多个多选框
    public Toggle[] toggles;
    private Toggle oldToggle;
    void Start()
    {
        Debug.Log(toggles.Length);
        for (int i = 0; i < toggles.Length; i++)
        {
            Toggle nowToggle = toggles[i];
            nowToggle.toggleEvents += (value) =>
            {
                if (value)
                {
                    for(int j = 0; j < toggles.Length; j++)
                    {
                        if(toggles[j] != nowToggle)
                        {
                            toggles[j].isSel = false;
                        }
                    }
                    oldToggle = nowToggle;
                }
                else
                {
                    if(nowToggle == oldToggle)
                    {
                        nowToggle.isSel = true;
                    }
                }
            };
        }
    }

   
}
