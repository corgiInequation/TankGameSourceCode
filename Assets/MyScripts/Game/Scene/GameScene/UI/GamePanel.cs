using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : SceneBase<GamePanel>
{
    public Button settingBtn;
    public Button leaveBtn;
    public Label scoreLabel;
    public Label timeLabel;
    public MyTexture hpTex;
    public MyTexture hpBgTex;

    [HideInInspector]
    public float nowTime = 0;

    [HideInInspector]
    public int nowScore = 0;

    public float hpWidth = 350;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        settingBtn.btnEvents += () => { SettingsScene.Instance.ShowMe(); };
        leaveBtn.btnEvents += () => { QuitPanel.Instance.ShowMe(); };
        hpBgTex.pos.width = hpWidth;
        hpTex.pos.width = hpWidth;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        nowTime += Time.deltaTime;
        UpdateTime(nowTime);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void AddScore(int score)
    {
        nowScore += score;
        scoreLabel.content.text = nowScore.ToString();
    }
    public void UpdateTime(float nowTime)
    {
        int time = (int)nowTime;
        if(time >= 3600)
        {
            timeLabel.content.text = string.Format("{0}Ê±{1}·Ö{2}Ãë", time / 3600, time % 3600 / 60, time % 60);
        }
        else if(time >= 60 && time < 3600)
        {
            timeLabel.content.text = string.Format("{0}·Ö{1}Ãë", time % 3600 / 60, time % 60);
        }
        else
        {
            timeLabel.content.text = string.Format("{0}Ãë", time % 60);
        }
    }
    public void UpdateHp(int maxHp, int nowHp)
    {
        hpBgTex.pos.width = hpWidth;
        hpTex.pos.width = (nowHp * 1.0f / maxHp) * hpWidth;
    }
}
