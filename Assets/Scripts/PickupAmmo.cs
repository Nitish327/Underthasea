using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoPickup : MonoBehaviour
{
    public int pickupAmount;
    public Projectile gunScript;
    public AudioSource pickupSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gunScript.ammo = gunScript.ammo + pickupAmount;
            Destroy(this.gameObject);
            pickupSound.Play();
        }
    }
}