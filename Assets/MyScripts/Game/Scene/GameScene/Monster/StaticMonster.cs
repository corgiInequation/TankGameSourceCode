using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticMonster : TankBase
{
    // Start is called before the first frame update
    public GameObject bullet;
    public float fireTime = 2;
    public float nowTime = 0;
    public Transform[] firePos;

    // Update is called once per frame
    void Update()
    {
        tankHead.transform.Rotate(Vector3.up *  headRotateSpeed * Time.deltaTime);
        nowTime += Time.deltaTime;
        if(nowTime >= fireTime)
        {
            nowTime = 0;
            Fire();
        }

    }

    public override void Fire()
    {
        for(int i = 0; i < firePos.Length; i++)
        {
            Bullet tBullet =  BulletPool.Instance.UseBullet(bullet.gameObject.name);
            tBullet.gameObject.transform.position = firePos[i].position;
            tBullet.gameObject.transform.rotation = firePos[i].rotation;
            tBullet.SetFather(this);
        }
    }
    public override void Wound(TankBase other)
    {
        
    }

}
