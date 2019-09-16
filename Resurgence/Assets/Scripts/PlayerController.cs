using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public LayerMask whatIsObject;

    private bool jumped = false;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        //considered objects ground to jump off of
        isGrounded = (Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround))
                  || (Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsObject));

        moveInput = Input.GetAxis("Horizontal");
        Debug.Log(moveInput);
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if ((!facingRight && moveInput > 0) || (facingRight && moveInput < 0)) flip();
    }

    void Update() {
        if (isGrounded) jumped = false;

        if (Input.GetKeyDown(KeyCode.UpArrow) && !jumped) {
            rb.velocity = Vector2.up * jumpForce;
        }
            jumped = true;
    }

    private void flip() {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    // private Rigidbody2D rb;
    // public float maxSpeed;
    // public float moveForce = 0f;
    // public float jumpForce;
    // private int direction = 1;
    // //private float jump;
    // private float translation = 0f;

    // void Start()
    // {
    //     rb = GetComponent<Rigidbody2D>();
    // }

    // void Update()
    // {
        
    //     Debug.Log(translation);
    //     if (Input.GetKey("d")) {
    //         direction = 1;
    //         transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
            
    //     } else if (Input.GetKey("a")) {
    //         direction = -1;
    //         transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
    //     }

    //     // Debug.LogError("direction: " + direction);
    //     // Debug.LogError("velocity: " + rb.velocity.sqrMagnitude);
    //     // Debug.LogError("translation: " + translation);
        
    //     if (transform.localScale.x < 0 && rb.velocity.x > 0 || transform.localScale.x > 0 && rb.velocity.x < 0) {          //if player is going right/l but wants left/r
    //         rb.velocity = new Vector2(-rb.velocity.x/2, rb.velocity.y);
    //     }

        

    //     // if (Input.GetKeyDown("w")) {
    //     //     StartCoroutine(example());
    //     // }
    //     //Vector2 grav = new Vector2(jumpForce*0.1f, -9.8f * rb.mass);
    //     //rb.AddForce(Vector2.down * 22.8f);
    //     movePlayer(); //actually moves the player
    // }

    // // private IEnumerator reference() {
        
    // //     while (true) {
    // //         Debug.Log("test");
    // //         yield return new WaitForSeconds(1);
    // //     }
    // //     //yield return 0;
    // // }

    // void OnCollisionEnter2D(Collision2D col) {
    //     if (col.gameObject.tag == "Ground") {

    //     }
    // }

    // private void movePlayer() {
    //     //Limiting velocity
    //     //bool canImpulse = true;
    //     // if (!(Input.GetKeyUp("a") && (Input.GetKeyUp("d")) && direction < 0) {
    //     //     canImpulse = false;
    //     // }   
        
    //          //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //jumping
    //          translation = 0;
    //         if (rb.velocity.sqrMagnitude > -maxSpeed && rb.velocity.sqrMagnitude < maxSpeed) {
    //             //if (canImpulse) {
    //                 //rb.AddForce(Vector2.right * translation, ForceMode2D.Impulse);
    //             //} else {
    //                 translation = Input.GetAxis("Horizontal") * moveForce; //setting up movement
    //             //}
    //         }
    //         Vector2 jumpSpeed = new Vector2(translation, 0);
    //         if (Input.GetKeyDown("w")) jumpSpeed = new Vector2(translation, jumpForce); //jumping
    //         rb.velocity = jumpSpeed;
        
    // }
}
