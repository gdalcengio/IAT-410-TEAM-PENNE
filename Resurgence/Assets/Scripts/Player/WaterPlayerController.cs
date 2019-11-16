using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPlayerController : MonoBehaviour
{
    public enum State {                                   //easy way to deal with cutscenes, ability charging, etc...
        Ready,
        Busy,
    }

    // Animations
    public Animator animator;

    public State abilityState;

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
    // private bool jumped = false;

    Camera cam;
    private Vector2 screenBounds;
    private float playerWidth, playerHeight;
    Vector3 viewPos;

    void Awake() {

    }

    //on start, gets the rigidbody of the player
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        abilityState = State.Ready;                   //making sure the player starts ready
        cam = Camera.main;
        screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
        playerWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate() {
        if (abilityState == State.Busy) return;       //disabling everything else

        //considered objects ground to jump off of
        isGrounded = (Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround))
                  || (Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsObject));

        moveInput = Input.GetAxis("T_Horizontal"); //1 or -1
        // Debug.Log(moveInput);

        //movement
        if (canMove) {
            // rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            rb.AddForce(new Vector2(moveInput * speed, rb.velocity.y));
        }

        //flip the script
        if (((!facingRight && moveInput > 0) || (facingRight && moveInput < 0)) && canMove) flip();

        //screen bounds
        if (transform.position.x <= -screenBounds.x+playerWidth) {
            transform.position = new Vector2(-screenBounds.x+playerWidth, transform.position.y);
        } else if (transform.position.x >= screenBounds.x-playerWidth) {
            transform.position = new Vector2(screenBounds.x-playerWidth, transform.position.y);
        }
    }

    //just for jumping for now
    void Update() {
        // animations
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("T_Horizontal")*34f));

        // if (isGrounded) jumped = false;
        if (abilityState == State.Busy) return;       //disabling everything else

        if ((Input.GetButtonDown("T_Jump") || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            jump();
        };
    }

    void jump() {
        if (canMove)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
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
