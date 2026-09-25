using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_Prop
{
    Hp,
    MaxHp,
    Attack, 
    Defence
}

public class PropAward : MonoBehaviour
{
    public int rotateSpeed = 60;
    public E_Prop prop = E_Prop.Attack;
    public int changeValue = 2;
    public GameObject disappearEff;
    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerTank playerTank = other.GetComponent<PlayerTank>();
            switch (prop)
            {
                case E_Prop.Hp:
                    playerTank.nowHp += changeValue;
                    if (playerTank.nowHp > playerTank.maxHp)
                        playerTank.nowHp = playerTank.maxHp;
                    GamePanel.Instance.UpdateHp(playerTank.maxHp, playerTank.nowHp);
                    break;
                case E_Prop.MaxHp:
                    GamePanel.Instance.hpWidth = (playerTank.maxHp + changeValue)*1.0f / playerTank.maxHp * GamePanel.Instance.hpWidth;
                    playerTank.maxHp += changeValue;
                    GamePanel.Instance.UpdateHp(playerTank.maxHp, playerTank.nowHp);
                    break;
                case E_Prop.Attack:
                    playerTank.attack += changeValue;
                    break;
                case E_Prop.Defence:
                    playerTank.defence += changeValue;
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
            GameObject eff = Instantiate(disappearEff, transform.position, transform.rotation);
            AudioSource ads = eff.GetComponent<AudioSource>();
            ads.mute = !DataMg.Instance.musicData.soundOpen;
            ads.volume = DataMg.Instance.musicData.soundSize;
            ads.Play();
        }
    }
}
