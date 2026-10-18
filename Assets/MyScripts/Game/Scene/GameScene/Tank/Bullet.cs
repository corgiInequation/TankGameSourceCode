using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

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
            BulletPool.Instance.ReturnBullet(this);
        }
    }
}
