using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginScene : SceneBase<BeginScene>
{
    public Button beginBtn;
    public Button settingBtn;
    public Button exitBtn;
    public Button rankBtn;

    private void Start()
    {
        beginBtn.btnEvents += () => { SceneManager.LoadScene("GameScene"); };
        settingBtn.btnEvents += () => { HideMe(); SettingsScene.Instance.ShowMe(); };
        exitBtn.btnEvents += () => { Application.Quit(); };
        rankBtn.btnEvents += () => { HideMe(); RankScene.Instance.ShowMe(); };
        Time.timeScale = 1f;
    }
}
