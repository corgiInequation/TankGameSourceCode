using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitPanel : SceneBase<QuitPanel>
{
    // Start is called before the first frame update
    public Button exitBtn;
    public Button confirmBtn;
    public Button continueBtn;

    void Start()
    {
        exitBtn.btnEvents += () =>
        {
            HideMe();
        };
        continueBtn.btnEvents += () =>
        {
            HideMe();
        };
        confirmBtn.btnEvents += () =>
        {
            SceneManager.LoadScene("BeginScene");
            BulletPool.Instance.ReturnAllActiveBullet();
        };
        base.HideMe();
    }

    public override void HideMe()
    {
        base.HideMe();
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public override void ShowMe()
    {
        base.ShowMe();
        Time.timeScale = 0;
    }
}
