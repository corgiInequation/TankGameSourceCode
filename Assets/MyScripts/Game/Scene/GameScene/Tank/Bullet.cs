using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

//之所以需要这个结构体，是为了防止一种情况，就是子弹射出去了，但是子弹的父亲被销毁了，这个时候再去获取父亲坦克的属性就会出问题。
//所以要提前把父亲坦克的属性记录好
struct FatherTankInfo
{
    public int attack;
    public string tag;
}

public class Bullet : MonoBehaviour
{
    public GameObject destroyEff;
    public float nowTime = 0;
    public float liveTime = 10f;
    public int moveSpeed;
    private FatherTankInfo fatherTankInfo;
    
    public void SetFather(TankBase fatherTank)
    {
        fatherTankInfo.attack = fatherTank.attack;
        fatherTankInfo.tag = fatherTank.tag;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        nowTime += Time.deltaTime;
        if(nowTime > liveTime)
        {
            BulletPool.Instance.ReturnBullet(this);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Player") && fatherTankInfo.tag == "Monster" || 
            other.CompareTag("Monster") && fatherTankInfo.tag == "Player")
        {
            if(other.CompareTag("Monster") || other.CompareTag("Player"))
            {
                other.GetComponent<TankBase>().Wound(fatherTankInfo.attack);
            }
            GameObject eff = Instantiate(destroyEff, transform.position, transform.rotation);
            AudioSource audio = eff.GetComponent<AudioSource>();
            audio.mute = !DataMg.Instance.musicData.soundOpen;
            audio.volume = DataMg.Instance.musicData.soundSize;
            audio.Play();
            //这里就不需要Destroy子弹了，而是直接把子弹放回内存池即可
            BulletPool.Instance.ReturnBullet(this);
        }
    }
}
