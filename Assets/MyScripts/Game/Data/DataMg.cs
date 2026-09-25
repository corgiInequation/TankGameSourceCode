using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMg 
{
    //数据管理类，其实就是负责管理数据
    //比如对数据进行初始化
    //为外部提供接口获取数据
    //为外部提供接口修改数据
    private static DataMg instance = new DataMg();
    public static DataMg Instance => instance;

    public MusicData musicData;
    public RankList rankDatas;

    //数据初始化
    public DataMg()
    {
        //加载音乐数据
        musicData = PlayerPrefsMg.Instance.Load(typeof(MusicData), "Music") as MusicData;
        if (!musicData.isNotFirstLoad)
        {
            musicData.isNotFirstLoad = true;
            musicData.musicOpen = true;
            musicData.soundOpen = true;
            musicData.musicSize = 0.5f;
            musicData.soundSize = 0.5f;
        }

        //加载排名数据
        rankDatas = PlayerPrefsMg.Instance.Load(typeof(RankList), "Rank") as RankList;
    }
    //数据修改
    
    //修改排名数据
    public void AddNewRank(string name, int score, int time)
    {
        rankDatas.list.Add(new RankData(name, score, time));
        rankDatas.list.Sort((a, b) => { return a.time <  b.time ? -1 : 1; });
        for(int i = rankDatas.list.Count - 1; i >= 10; i--)
        {
            rankDatas.list.RemoveAt(i);
        }
        PlayerPrefsMg.Instance.Save(rankDatas, "Rank");
    }

    //修改音乐数据
    public void changeMusicOpen(bool value)
    {
        musicData.musicOpen = value; 
        PlayerPrefsMg.Instance.Save(musicData, "Music");
        if (value)
        {
            MusicManager.Instance.OnAudio();
        }
        else
        {
            MusicManager.Instance.OffAudio();
        }
    }
    public void changeSoundOpen(bool value)
    {
        musicData.soundOpen = value; PlayerPrefsMg.Instance.Save(musicData, "Music");
    }
    public void changeMusicSize(float value)
    {
        musicData.musicSize = value; PlayerPrefsMg.Instance.Save(musicData, "Music");
        MusicManager.Instance.ChangeSize(value);
    }
    public void changeSoundSize(float value)
    {
        musicData.soundSize = value; PlayerPrefsMg.Instance.Save(musicData, "Music");
    }
    
}
