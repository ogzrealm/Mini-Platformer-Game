using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private float bulletVelocity;
    [SerializeField] private float bulletSpeed = 20f;
    
    private PlayerMovement playerMovement;
    private CinemachineShake[] myCameraShake;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        bulletVelocity = playerMovement.transform.localScale.x * bulletSpeed;
        myCameraShake=FindObjectsOfType<CinemachineShake>();
        myRigidbody.linearVelocity = new Vector2(bulletVelocity, 0);

    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        
        if (other.gameObject.layer == 11)
        {
            foreach (var shake in myCameraShake)
            {
                shake.CameraShake(4f,0.1f);
            }
            Destroy(other.gameObject);
            
        }

    }
}
