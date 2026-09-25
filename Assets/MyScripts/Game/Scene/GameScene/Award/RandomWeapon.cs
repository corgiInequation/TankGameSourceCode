using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWeapon : MonoBehaviour
{
    public int rotateSpeed = 30;
    public GameObject[] weapons;
    public GameObject disappearEff;

    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int weaponIndex = Random.Range(0, weapons.Length);
            other.gameObject.GetComponent<PlayerTank>().ChangeWeapon(weapons[weaponIndex]);
            Destroy(gameObject);
            GameObject eff = Instantiate(disappearEff, transform.position, transform.rotation);
            AudioSource ad = eff.GetComponent<AudioSource>();
            ad.mute = !DataMg.Instance.musicData.soundOpen;
            ad.volume = DataMg.Instance.musicData.soundSize;
            ad.Play();
        }
    }
}
