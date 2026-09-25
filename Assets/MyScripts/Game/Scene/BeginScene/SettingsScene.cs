using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsScene : SceneBase<SettingsScene>
{
    public Button exitBtn;
    public Toggle musicToggle;
    public Toggle soundToggle;
    public Slider musicSlider;
    public Slider soundSlider;

    void Start()
    {
        exitBtn.btnEvents += () => { this.HideMe(); if(SceneManager.GetActiveScene().name == "BeginScene") BeginScene.Instance.ShowMe(); };
        musicToggle.toggleEvents += (value) => DataMg.Instance.changeMusicOpen(value); 
        soundToggle.toggleEvents += (value) => DataMg.Instance.changeSoundOpen(value);
        musicSlider.sliderEvents += (value) => DataMg.Instance.changeMusicSize(value);
        soundSlider.sliderEvents += (value) => DataMg.Instance.changeSoundSize(value);
        InitMusicData();
        HideMe();
    }
    void InitMusicData()
    {
        musicToggle.isSel = DataMg.Instance.musicData.musicOpen;
        soundToggle.isSel = DataMg.Instance.musicData.soundOpen;
        musicSlider.nowValue = DataMg.Instance.musicData.musicSize;
        soundSlider.nowValue = DataMg.Instance.musicData.soundSize;
    }
    public override void ShowMe()
    {
        base.ShowMe();
        if (SceneManager.GetActiveScene().name == "GameScene")
            Time.timeScale = 0;
    }
    public override void HideMe()
    {
        base.HideMe();
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }
}
