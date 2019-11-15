using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Transform target;
    public float speed = 5f;
    public float rotateSpeed = 100f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (Random.value < 0.5) {
            target = GameObject.Find("Itztli").transform;
        } else {
            target = GameObject.Find("Tlaloc").transform;
        }
    }

    void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        float rotateMag = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateMag * rotateSpeed;
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("test");
        if (col.gameObject.tag == "Itztli" || col.gameObject.tag == "Tlaloc") {
            GameManager.Instance.ResetScene();
        }

        Destroy(transform.parent.gameObject);
    }
}