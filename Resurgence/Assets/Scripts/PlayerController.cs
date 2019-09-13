using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private Rigidbody2D rb;
    public float speed = 1f;
    //private Vector2 horizontalForce;
    private float translation = 0f;

    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        translation = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        transform.Translate(translation, 0f, 0f, Space.World);
        // horizontalForce.x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        // // Debug.Log(horizontalForce);

        // if (rb.velocity.x < 10f && rb.velocity.x > -10f)
        //     rb.AddForce(horizontalForce, ForceMode2D.Impulse);
        // //Debug.Log(rb.velocity);
    }
}
