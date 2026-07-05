using UnityEngine;
using System.Collections;

public class HorizontalElevator : MonoBehaviour
{
    [SerializeField] private Vector2 leftPoint, rightPoint;
    [SerializeField] private float elevatorSpeed = 5f;
    [SerializeField] private BoxCollider2D playerCollider;

    private Rigidbody2D rigidBody;
    private Rigidbody2D playerRigidbody;
    private bool playerOnElevator = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerRigidbody = playerCollider.GetComponent<Rigidbody2D>();
        StartCoroutine(ElevatorMovement());
    }

    void CheckPlayerContact()
    {
        playerOnElevator = playerCollider.IsTouchingLayers(LayerMask.GetMask("Elevator"));
    }

    void MovePlatform(Vector2 targetPoint)
    {
        Vector2 oldPos = rigidBody.position;
        Vector2 newPos = Vector2.MoveTowards(oldPos, targetPoint, elevatorSpeed * Time.deltaTime);
        rigidBody.MovePosition(newPos);

        if (playerOnElevator)
        {
            Vector2 delta = newPos - oldPos;
            playerRigidbody.MovePosition(playerRigidbody.position + delta);
        }
    }

    IEnumerator ElevatorMovement()
    {
        while (true)
        {
            while ((Vector2)transform.position != leftPoint)
            {
                CheckPlayerContact();
                MovePlatform(leftPoint);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(1.5f);

            while ((Vector2)transform.position != rightPoint)
            {
                CheckPlayerContact();
                MovePlatform(rightPoint);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(1.5f);
        }
    }
}