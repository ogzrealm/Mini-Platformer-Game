using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    private Animator myAnimator;
    private CapsuleCollider2D bodyCollider;
    private BoxCollider2D feetCollider;
    
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction;

    private Vector2 moveVector;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private float mJumpForce = 5f;

    private float gravityAtStart;

    private bool stillJumping = false;
    [HideInInspector] public bool canMove = true;

    private void Awake()
    {
        moveAction=InputSystem.actions.FindAction("Move");
        jumpAction=InputSystem.actions.FindAction("Jump");
    }
    private void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        gravityAtStart = myRigidbody2D.gravityScale;
        myAnimator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider=GetComponent<BoxCollider2D>();
        
    }
    private void OnEnable()
    {
        jumpAction.performed += Jump;
    }

    private void OnDisable()
    {
        jumpAction.performed -= Jump;
    }
    
    private void FixedUpdate()
    {
        Movement();
        Flipping();
        Climbing();
        MushRoomJumping();
    }

    private void Movement()
    {
        if (!canMove)
        {
            return;
            
        }
        moveVector = moveAction.ReadValue<Vector2>();
        float xMovement=moveVector.x*moveSpeed;
        myRigidbody2D.linearVelocity=new Vector2(xMovement,myRigidbody2D.linearVelocity.y);
        bool isRunning=Mathf.Abs(moveVector.x)>Mathf.Epsilon;
        myAnimator.SetBool("isRunning",isRunning);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!canMove)
        {
            return;
        }
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Platform", "Mushroom")))
        {
            return;
        }
        PlayerAudio.instance.PlayJumpSound();
        myRigidbody2D.linearVelocity += new Vector2(0f, 5f);
        stillJumping=true;
        myAnimator.SetTrigger("isJumping");
    }

    private void MushRoomJumping()
    {
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Mushroom")))
        {
            myRigidbody2D.AddForce(new Vector2(0f,mJumpForce));
        }
    }
    

    private void Flipping()
    {
        bool isFlipped=Mathf.Abs(moveVector.x)>Mathf.Epsilon;
        if (isFlipped)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody2D.linearVelocity.x),1);
        }
        
    }

    private void Climbing()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myAnimator.SetBool("isClimbing",false);
            myRigidbody2D.gravityScale = gravityAtStart;
            return;
        }
        else
        {
            myRigidbody2D.gravityScale = 0;
            Vector2 climbVector=new Vector2(myRigidbody2D.linearVelocity.x,moveVector.y*climbSpeed);
            myRigidbody2D.linearVelocity=climbVector;
            bool isClimbing=Mathf.Abs(myRigidbody2D.linearVelocity.y)>Mathf.Epsilon;
            myAnimator.SetBool("isClimbing",isClimbing);
        }
        
    }
    
}
