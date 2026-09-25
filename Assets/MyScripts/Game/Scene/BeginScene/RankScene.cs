using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankScene : SceneBase<RankScene>
{
    public Button exitButton;
    public List<Label> names = new List<Label>();
    public List<Label> scores = new List<Label>();
    public List<Label> times = new List<Label>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            names.Add(transform.Find("NameLabels/Label (" + i + ")").GetComponent<Label>());
            scores.Add(transform.Find("ScoreLabels/Label (" + i + ")").GetComponent<Label>());
            times.Add(transform.Find("TimeLabels/Label (" + i + ")").GetComponent<Label>());
        }
        exitButton.btnEvents += () => { HideMe(); BeginScene.Instance.ShowMe(); };
        HideMe();
    }

    private void UpdateData()
    {
        List<RankData> rankDatas = DataMg.Instance.rankDatas.list;
        for(int i = 0; i < rankDatas.Count;i++)
        {
            names[i].content.text = rankDatas[i].name;
            scores[i].content.text = rankDatas[i].score.ToString();
            int time = rankDatas[i].time;
            string timeStr = string.Format("{0}Ê±{1}·Ö{2}Ãë", time/3600, time%3600/60, time%60);
            times[i].content.text = timeStr;
        }
    }
    public override void ShowMe()
    {
        base.ShowMe();
        UpdateData();
    }
}
