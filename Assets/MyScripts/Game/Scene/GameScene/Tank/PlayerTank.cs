using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTank : TankBase
{
    public Weapon weapon;
    public Transform weaponPos;
    public GameObject tankBody;
    public bool isDead = false;

    private void Update()
    {
        if (!isDead)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical"));
            if (Input.GetKey(KeyCode.S))
            {
                transform.Rotate(-Vector3.up * rotateSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
            }
            else
            {
                transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
            }
            tankHead.transform.Rotate(Vector3.up * headRotateSpeed * Time.deltaTime * Input.GetAxis("Mouse X"));
            if (Input.GetMouseButtonDown(0) && Time.timeScale != 0)
            {
                Fire();
            }
        }

    }
    public override void Fire()
    {
        if(weapon != null) 
            weapon.Fire();
    }
    public override void Dead()
    {
        //base.Dead();
        isDead = true;
        tankBody.SetActive(false);
        tankHead.SetActive(false);
        GameObject eff = Instantiate(deadEffect, transform.position, transform.rotation);
        AudioSource ads = eff.GetComponent<AudioSource>();
        ads.mute = !DataMg.Instance.musicData.soundOpen;
        ads.volume = DataMg.Instance.musicData.soundSize;
        ads.Play();
        StartCoroutine(DelayPause());
    }
    IEnumerator DelayPause()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0;
        LosePanel.Instance.ShowMe();
    }
    public override void Wound(int attack)
    {
        base.Wound(attack);
        GamePanel.Instance.UpdateHp(maxHp, nowHp);
    }
    public void ChangeWeapon(GameObject newWeapon)
    {
        if(weapon != null)
        {
            Destroy(weapon.gameObject);
        }
        GameObject trueNewWeapon = Instantiate(newWeapon, weaponPos, false);
        weapon =trueNewWeapon.GetComponent<Weapon>();
        weapon.SetFatherTank(this);
    }
}
