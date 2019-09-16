using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float maxSpeed;
    public float moveForce = 0f;
    public float jumpForce;
    private int direction = 1;
    //private float jump;
    private float translation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        

        translation = Input.GetAxisRaw("Horizontal") * moveForce; //setting up movement
        Debug.Log(translation);
        if (Input.GetKey("d")) {
            direction = 1;
            transform.localScale = new Vector3(2.456f, transform.localScale.y, transform.localScale.z);
            
        }   

        if (Input.GetKey("a")) {
            direction = -1;
            transform.localScale = new Vector3(-2.456f, transform.localScale.y, transform.localScale.z);
        }

        // Debug.LogError("direction: " + direction);
        // Debug.LogError("velocity: " + rb.velocity.sqrMagnitude);
        // Debug.LogError("translation: " + translation);
        
            if (transform.localScale.x < 0 && rb.velocity.x > 0 || transform.localScale.x > 0 && rb.velocity.x < 0) {          //if player is going right but wants left
                
                rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
            }

        // if (Input.GetKeyDown("w")) {
        //     StartCoroutine(example());
        // }
        movePlayer(); //actually moves the player
    }

    // private IEnumerator reference() {
        
    //     while (true) {
    //         Debug.Log("test");
    //         yield return new WaitForSeconds(1);
    //     }
    //     //yield return 0;
    // }


    private void movePlayer() {
        //Limiting velocity
        if (rb.velocity.sqrMagnitude > -maxSpeed && rb.velocity.sqrMagnitude < maxSpeed) rb.AddForce(Vector2.right * translation, ForceMode2D.Impulse);
        if (Input.GetKeyDown("w")) rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //jumping
    }
}
