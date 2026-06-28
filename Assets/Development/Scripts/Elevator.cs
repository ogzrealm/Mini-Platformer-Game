using System.Collections;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Elevator : MonoBehaviour
{
    [SerializeField] private Vector2 topPoint, bottomPoint;
    [SerializeField] private float elevatorSpeed=5f;
    
    private Rigidbody2D rigidBody;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        StartCoroutine(ElevatorMovement());
        
    }

    IEnumerator ElevatorMovement()
    {
        while (true)
        {
            while ((Vector2)transform.position != topPoint)
            {
                Vector2 newPos=Vector2.MoveTowards(rigidBody.position,topPoint,elevatorSpeed*Time.deltaTime);
                rigidBody.MovePosition(newPos);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(1);
            while ((Vector2)transform.position != bottomPoint)
            {
                Vector2 newPos=Vector2.MoveTowards(rigidBody.position,bottomPoint,elevatorSpeed*Time.deltaTime);
                rigidBody.MovePosition(newPos);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(1);
        }
    }
        
}
