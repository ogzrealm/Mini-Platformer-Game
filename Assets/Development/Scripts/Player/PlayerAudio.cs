using System;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public static PlayerAudio instance;
    
    private AudioSource audioSource;
    
    [SerializeField] private AudioClip jumpSound, fireSound, bumpSound, coinPickUpSound, deathSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound);
    }
    
    public void PlayFireSound()
    {
        audioSource.PlayOneShot(fireSound);
    }
    public void PlayBumpSound()
    {
        audioSource.PlayOneShot(bumpSound);
    }

    public void PlayCoinPickUp()
    {
        audioSource.PlayOneShot(coinPickUpSound);
    }
    public void PlayDeathSound()
    {
        audioSource.PlayOneShot(deathSound);
    }
}
