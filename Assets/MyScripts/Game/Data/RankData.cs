using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankData 
{
    public string name;
    public int score;
    public int time;
    public RankData(string name, int score, int time)
    {
        this.name = name;
        this.score = score;
        this.time = time;
    }
    public RankData() { }
}
public class RankList
{
    public List<RankData> list;
}
