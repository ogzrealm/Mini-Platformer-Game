using UnityEngine;

public class Enemies : MonoBehaviour
{
    private Rigidbody2D myRigidbody;

    [SerializeField] private float moveSpeed = 5f;

    private void Start()
    {
        myRigidbody=GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        myRigidbody.linearVelocity=new Vector2(moveSpeed, myRigidbody.linearVelocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        moveSpeed=-moveSpeed;
        FlipEnemy();
    }

    private void FlipEnemy()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.linearVelocity.x)),1);
    }

}
