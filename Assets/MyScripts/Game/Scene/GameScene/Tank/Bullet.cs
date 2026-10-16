using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public TankBase fatherTank;
    public GameObject destroyEff;
    private float nowTime = 0;
    private float liveTime = 10f;
    public int moveSpeed;
    public void SetFather(TankBase fatherTank)
    {
        this.fatherTank = fatherTank ;
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
        if (other.CompareTag("Wall") || other.CompareTag("Player") && fatherTank.CompareTag("Monster") || 
            other.CompareTag("Monster") && fatherTank.CompareTag("Player"))
        {
            if(other.CompareTag("Monster") || other.CompareTag("Player"))
            {
                other.GetComponent<TankBase>().Wound(fatherTank);
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
