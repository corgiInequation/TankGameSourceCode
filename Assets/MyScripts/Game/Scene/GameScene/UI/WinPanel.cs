using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : SceneBase<WinPanel>
{
    public Button confirmButton;
    public InputText inputText;
    // Start is called before the first frame update
    void Start()
    {
        
        confirmButton.btnEvents += () =>
        {
            string userName = inputText.text;
            DataMg.Instance.AddNewRank(userName, GamePanel.Instance.nowScore, (int)GamePanel.Instance.nowTime);
        };
        confirmButton.btnEvents += () =>
        {
            SceneManager.LoadScene("BeginScene");
            BulletPool.Instance.ReturnAllActiveBullet();
        };
        HideMe();
        
    }

    public override void ShowMe()
    {
        base.ShowMe();
        Cursor.lockState = CursorLockMode.None;
    }
}
