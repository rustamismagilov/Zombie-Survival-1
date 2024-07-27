using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoAmount = 5;
    [SerializeField] AmmoType ammoType;
    [SerializeField] AudioClip pickupSound; // Add this line to allow setting the sound clip in the editor

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);
            PlayPickupSound(other.gameObject);
            Destroy(gameObject);
        }
    }

    void PlayPickupSound(GameObject player)
    {
        if (pickupSound != null)
        {
            AudioSource audioSource = player.GetComponentInChildren<AudioSource>();
            if (audioSource != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }
            else
            {
                Debug.LogWarning("No AudioSource component found.");
            }
        }
    }
}
