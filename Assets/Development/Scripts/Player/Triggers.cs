using System.Collections;
using Unity.Cinemachine;
using UnityEngine;


public class Triggers : MonoBehaviour
{
    private Animator myAnimator;
    private Rigidbody2D myRigidbody2D;

    [SerializeField] private Vector2 kickForce;
    
    private PlayerMovement playerMovement;

    [SerializeField] private GameObject stateDrivenCamera;
    private BoxCollider2D playerCollider;
    private CapsuleCollider2D playerCollider2;
    
    private CinemachineShake[]  myCameraShake;

    [HideInInspector] public bool isTriggered = false;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        
        playerCollider = GetComponent<BoxCollider2D>();
        playerCollider2 = GetComponent<CapsuleCollider2D>();
        
        playerMovement = GetComponent<PlayerMovement>();

        myCameraShake = FindObjectsOfType<CinemachineShake>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(isTriggered) return;
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            GameManager.instance.DeathState();
            StartCoroutine(drownEffect());
            isTriggered = true;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            isTriggered = true;
            GameManager.instance.DeathState();
            playerMovement.canMove = false;
            PlayerAudio.instance.PlayDeathSound();
            foreach (var shake in myCameraShake)
            {
                shake.CameraShake(5f, 0.3f);
            }

            myAnimator.SetTrigger("Dying");
            DeathKick();
            playerCollider.enabled = false;
            playerCollider2.enabled = false;
            StartCoroutine(DisableFollowCameras());
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Spikes"))
        {
            isTriggered = true;
            GameManager.instance.DeathState();
            if (!ObjectsAudio.instance.GetComponent<AudioSource>().isPlaying)
            {
                ObjectsAudio.instance.PlaySpikeSound();
            }

            playerMovement.canMove = false;
            foreach (var shake in myCameraShake)
            {
                shake.CameraShake(6f, 0.3f);
            }

            myAnimator.SetTrigger("Dying");
            DeathKick();
            playerCollider.enabled = false;
            playerCollider2.enabled = false;
            StartCoroutine(DisableFollowCameras());
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Doorway"))
        {
            playerMovement.canMove = false;
            playerCollider.enabled = false;
            myRigidbody2D.linearVelocity = Vector3.zero;
            myAnimator.enabled = false;
            playerMovement.enabled = false;
            GameManager.instance.NextLevel();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            PlayerAudio.instance.PlayBumpSound();
            myRigidbody2D.AddForce(new Vector2(0,200f));
            Destroy(other.gameObject);
        }
    }

    private void DeathKick()
    {
        myRigidbody2D.AddForce(kickForce);
    }

    IEnumerator drownEffect()
    {
        myAnimator.SetBool("isRunning", false);
        playerMovement.canMove = false;
        ObjectsAudio.instance.PlayDrowningSound();
        yield return new WaitForSecondsRealtime(0.8f);
        myAnimator.SetTrigger("Dying");
        ObjectsAudio.instance.StopDrowningSound();
        yield return new WaitForSecondsRealtime(0.2f);
    }

    IEnumerator DisableFollowCameras()
    {
        yield return new WaitForSeconds(0.3f);
        stateDrivenCamera.GetComponent<CinemachineStateDrivenCamera>().enabled = false;
        yield return new WaitForSecondsRealtime(1.5f);
    }
    
    
}
