using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class SingleInstanceBase<T> where T : new()
{
    private static T instance = new T();    
    public static T Instance
    {
        get { return instance; }
    }
}

public class PlayerPrefsMg : SingleInstanceBase<PlayerPrefsMg>
{
    public void Save(object obj, string keyName)
    {
        //首先是获取obj的type
        Type type = obj.GetType();
        //之后根据获取的type, 获得类中的所有字段信息
        FieldInfo[] fieldInfos = type.GetFields();
        //之后，遍历这些字段，进行存储
        for(int i = 0; i < fieldInfos.Length; i++)
        {
            FieldInfo fieldInfo = fieldInfos[i];
            object value = fieldInfo.GetValue(obj);
            string key = keyName + "_" + type.Name + "_" + fieldInfo.Name;
            SaveValue(value, key);
        }
    }
    public void SaveValue(object obj, string key)
    {
        Type type = obj.GetType();
        if(type == typeof(int))
        {
            PlayerPrefs.SetInt(key, (int)obj);
        }
        else if (type == typeof(string))
        {
            PlayerPrefs.SetString(key, (string)obj);
        }
        else if (type == typeof(float))
        {
            PlayerPrefs.SetFloat(key, (float)obj);
        }
        else if(type == typeof(bool))
        {
            PlayerPrefs.SetInt(key, (bool)obj? 1 : 0);
        }
        else if (typeof(IList).IsAssignableFrom(type))
        {
            IList tList = obj as IList;
            PlayerPrefs.SetInt(key, tList.Count);
            for(int i = 0; i < tList.Count; i++)
            {
                SaveValue(tList[i], key + "_" + i);
            }
        }
        else if (typeof(IDictionary).IsAssignableFrom(type))
        {
            IDictionary tDict = obj as IDictionary;
            PlayerPrefs.SetInt(key, tDict.Count);
            int index = 0;
            foreach(object item in  tDict.Keys)
            {
                SaveValue(item, key + "_key" + "_" + index);
                SaveValue(tDict[item], key + "_value" + "_" + index);
                index++;
            }
        }
        //如果都不是上面的字段，那么我们可以判断其为自定义类型。（假设我们已经在上面把所有基本类型都实现了）
        else
        {
            Save(obj, key);
        }
    }

    public object Load(Type type, string keyName)
    {
        //初始化一个type类型的对象
        object res = Activator.CreateInstance(type);

        //首先，获取这个Type所有字段信息
        FieldInfo[] fieldInfos = type.GetFields();
        
        string key;
        FieldInfo fieldInfo;
        for(int i = 0; i < fieldInfos.Length; i++)
        {
            fieldInfo = fieldInfos[i];
            key = keyName + "_" + type.Name + "_" + fieldInfo.Name;
            fieldInfo.SetValue(res, LoadValue(fieldInfo.FieldType, key));
        }
        return res;
    }
    private object LoadValue(Type type, string key)
    {
        if (type == typeof(int))
        {
            return PlayerPrefs.GetInt(key, default);
        }
        else if (type == typeof(float))
        {
            return PlayerPrefs.GetFloat(key, default);
        }
        else if (type == typeof(string))
        {
            return PlayerPrefs.GetString(key, default);
        }
        else if(type == typeof(bool))
        {
            return PlayerPrefs.GetInt(key, default) == 1 ? true : false;
        }
        else if (typeof(IList).IsAssignableFrom(type))
        {
            int len = PlayerPrefs.GetInt(key, default);
            IList list = Activator.CreateInstance(type) as IList;
            for(int i = 0; i < len; i++)
            {
                list.Add(LoadValue(type.GetGenericArguments()[0], key + "_" + i));
            }
            return list;
        }
        else if (typeof(IDictionary).IsAssignableFrom(type))
        {
            int len = PlayerPrefs.GetInt(key, default);
            IDictionary dict = Activator.CreateInstance(type) as IDictionary;
            for(int i = 0; i < len; ++i)
            {
                object tKey = LoadValue(type.GetGenericArguments()[0], key + "_key_" + i);
                object tValue = LoadValue(type.GetGenericArguments()[1], key + "_value_" + i);
                dict.Add(tKey, tValue);
            }
            return dict;
        }
        else
        {
            return Load(type, key);
        }
    }

}
