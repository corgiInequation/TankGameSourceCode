using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroy : MonoBehaviour
{
    public GameObject deadEff;
    public GameObject[] awards;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            GameObject eff = Instantiate(deadEff, transform.position, transform.rotation);
            AudioSource ads = eff.GetComponent<AudioSource>();
            ads.mute = !DataMg.Instance.musicData.soundOpen;
            ads.volume = DataMg.Instance.musicData.soundSize;
            ads.Play();
            int awardIndex = Random.Range(0, awards.Length);
            GameObject award = Instantiate(awards[awardIndex], transform.position + new Vector3(0, 1.5f, 0), awards[awardIndex].transform.rotation);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
