using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TankBase : MonoBehaviour
{
    public int attack;
    public int defence;
    public int maxHp;
    public int nowHp;

    public float moveSpeed = 10;
    public float rotateSpeed = 100;
    public float headRotateSpeed = 100;
    public GameObject deadEffect;
    public GameObject tankHead;

    public abstract void Fire();
    public virtual void Dead()
    {
        Destroy(this.gameObject);
        if(deadEffect != null)
        {
            GameObject gameObject = Instantiate(deadEffect, transform.position, transform.rotation);
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.mute = !DataMg.Instance.musicData.soundOpen;
            audioSource.volume = DataMg.Instance.musicData.soundSize;
            audioSource.Play();
        }
    }
    public virtual void Wound(TankBase other)
    {
        int damage = other.attack - this.defence;
        if (damage <= 0)
            return;
        nowHp -= damage;
        if (nowHp <= 0)
        {
            nowHp = 0;
            Dead();
        }
    }
}
