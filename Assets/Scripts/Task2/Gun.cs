using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public float speed = 40;  
    public GameObject bullet; 
    public Transform bulletHole;  

    private AudioSource audioSource;
    public AudioClip audioClip;

    
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    public void Fire()
    {
        GameObject spawnedBullet = Instantiate(bullet, bulletHole.position, bulletHole.rotation);

        spawnedBullet.GetComponent<Rigidbody>().velocity = speed * bulletHole.forward;

        audioSource.PlayOneShot(audioClip);

        Destroy(spawnedBullet, 2);

    }
}
