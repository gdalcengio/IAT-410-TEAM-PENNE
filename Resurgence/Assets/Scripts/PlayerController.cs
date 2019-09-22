using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*movement controls */
    public float speed;
    public float jumpForce;
    private float moveInput;


    /*auxillary stuff */
    private Rigidbody2D rb;

    [HideInInspector]
    public bool facingRight = true;
    public bool canMove = true;

    /*jump object references and variables */
    private bool isGrounded;        //is the player on the ground?
    public Transform groundCheck;   //to check if plyer is on the ground
    public float checkRadius;       //leeway for floor
    public LayerMask whatIsGround;  //to choose what layer is considered ground
    public LayerMask whatIsObject;  //ditta but for the objects

    /*single jump flag */
    private bool jumped = false;

    //on start, gets the rigidbody of the player
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        //considered objects ground to jump off of
        isGrounded = (Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround))
                  || (Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsObject));

        moveInput = Input.GetAxis("Horizontal");
        //Debug.Log(moveInput);
        if (canMove) rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (((!facingRight && moveInput > 0) || (facingRight && moveInput < 0)) && canMove) flip();
    }

    //just for jumping for now
    void Update() {
        if (isGrounded) jumped = false;

        if (Input.GetKeyDown(KeyCode.UpArrow) && !jumped && canMove) {
            rb.velocity = Vector2.up * jumpForce;
        }
            jumped = true;
    }

    //flipping the character direction
    private void flip() {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    //freezes player position
    public void freeze() {
        rb.bodyType = RigidbodyType2D.Static;
    }

    //unfreezes player position
    public void unfreeze() {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

}
