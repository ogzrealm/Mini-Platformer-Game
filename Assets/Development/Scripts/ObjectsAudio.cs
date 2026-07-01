using UnityEngine;

public class ObjectsAudio : MonoBehaviour
{
    public static ObjectsAudio instance;
    
    private AudioSource audioSource;
    
    [SerializeField] private AudioClip drowningSound, spikeSound;
    [SerializeField] private AudioClip leverSound;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }
    
    public void PlayDrowningSound()
    {
        audioSource.PlayOneShot(drowningSound);
    }

    public void StopDrowningSound()
    {
        audioSource.Stop();
    }

    public void PlaySpikeSound()
    {
        audioSource.PlayOneShot(spikeSound);
    }

    public void PlayLeverSound()
    {
        audioSource.PlayOneShot(leverSound);

    }
}
