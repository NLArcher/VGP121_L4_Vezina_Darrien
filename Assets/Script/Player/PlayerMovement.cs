using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private Animator myAnim;
    public float speed;
    public float jumpforce;
    public bool isGrounded;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;
    private bool attack;
    private void HandleAttacks()
    {
        if (attack)
        {
            myAnim.SetTrigger("Attack");
        }
    }
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            attack = true;
        }
    }

    private void ResetValues()
    {
        attack = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();

        if (speed <= 0)
        {
            speed = 5.0f;
        }

        if (jumpforce <= 0)
        {
            jumpforce = 100.0f;
        }

        if (!groundCheck)
        {
            Debug.Log("Groundcheck does not exist, please set a transform value for groundcheck");
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.2f;
        }
    }
    void FixedUpdate()
    {
        HandleAttacks();
        ResetValues();
    }
    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
        Debug.Log(horizontalInput);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            myRigidbody.velocity = Vector2.zero;

            myRigidbody.AddForce(Vector2.up * jumpforce);
        }
        {
            HandleInput();
        }

        myRigidbody.velocity = new Vector2(horizontalInput, myRigidbody.velocity.y);

        myAnim.SetFloat("moveValue", Mathf.Abs(horizontalInput));
        myAnim.SetBool("Jump", !isGrounded);


    }
}
