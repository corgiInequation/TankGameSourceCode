using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Bullet bullet;
    public Transform[] bulletPos;
    public TankBase fatherTank;

    public void SetFatherTank(TankBase fatherTank)
    {
        this.fatherTank = fatherTank;
    }

    public void Fire()
    {
        for(int i = 0; i < bulletPos.Length; i++)
        {
            Bullet tBullet =  BulletPool.Instance.UseBullet(bullet.gameObject.name, bulletPos[i]);
            tBullet.SetFather(fatherTank);
        }
    }

}
