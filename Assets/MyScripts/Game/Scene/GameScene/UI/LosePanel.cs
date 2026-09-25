using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : SceneBase<LosePanel>
{
    public Button againButton;
    public Button returnButton;
    // Start is called before the first frame update
    void Start()
    {
        againButton.btnEvents += () => SceneManager.LoadScene("GameScene");
        returnButton.btnEvents += () => SceneManager.LoadScene("BeginScene");
        HideMe();
    }
    public override void ShowMe()
    {
        base.ShowMe();
        Cursor.lockState = CursorLockMode.None;
    }
}