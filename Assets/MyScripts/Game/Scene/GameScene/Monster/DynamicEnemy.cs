using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicEnemy : TankBase
{
    public Transform[] movePos;
    private Transform targetPos;
    public Transform lookAtObj;
    private int lastIndex;
    public GameObject bullet;
    public float fireTime = 0.5f;
    public float nowTime = 0;
    public Transform firePos;

    public Texture hpBgTex;
    private Rect hpBgTexRect;
    public Texture hpTex;
    private Rect hpTexRect;
    private float maxWidth = 2500;
    private float maxHeight = 500;
    private float showTime = 0;



    void Awake()
    {
        SetRandomTgPos();
    }

    void Update()
    {
        transform.LookAt(targetPos);
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        tankHead.transform.LookAt(lookAtObj);
        if (Vector3.Distance(transform.position, targetPos.position) <= 0.5f)
        {
            SetRandomTgPos();
        }
        if (lookAtObj == null)
        {
            return;
        }
        if(Vector3.Distance(transform.position, lookAtObj.position) <= 10)
        {
            nowTime += Time.deltaTime;
            if (nowTime >= fireTime)
            {
                Fire();
                nowTime = 0;
            }
        }
        
   }
    private void OnGUI()
    {
        if (showTime >= 0)
        {
            showTime -= Time.deltaTime;
            //首先，获取当前坦克位置
            //之后，将坦克位置转换为屏幕位置
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            //之后，将屏幕坐标转换为GUI的坐标
            screenPos.y = Screen.height - screenPos.y;
            //之后，获取GUI坐标后，画即可
            screenPos.x -= 50;
            screenPos.y -= 80;
            hpBgTexRect.x = screenPos.x;
            hpBgTexRect.y = screenPos.y;
            hpBgTexRect.width = maxWidth * (1.0f / (1 + screenPos.z));
            hpBgTexRect.height = maxHeight * (1.0f / (1 + screenPos.z));


            GUI.DrawTexture(hpBgTexRect, hpBgTex);

            hpTexRect.x = screenPos.x;
            hpTexRect.y = screenPos.y;
            hpTexRect.width = nowHp * 1.0f / maxHp * hpBgTexRect.width;
            hpTexRect.height = hpBgTexRect.height;

            GUI.DrawTexture(hpTexRect, hpTex);
        }
    }

    public void SetRandomTgPos()
    {
        if (movePos == null)
            return;
        int tIndex = Random.Range(0, movePos.Length);
        while(tIndex == lastIndex)
        {
            tIndex = Random.Range(0, movePos.Length);
        }
        targetPos = movePos[tIndex];
        lastIndex = tIndex;
    }

    public override void Fire()
    {
        GameObject tBullet = Instantiate(bullet, firePos.position, firePos.rotation);
        Bullet bulletctr = tBullet.GetComponent<Bullet>();
        bulletctr.SetFather(this);
    }
    public override void Dead()
    {
        base.Dead();
        GamePanel.Instance.AddScore(10);
    }
    public override void Wound(TankBase other)
    {
        base.Wound(other);
        showTime = 3;
    }
    
}
