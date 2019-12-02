using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Transform target;
    public float speed = 5f;
    public float rotateSpeed = 100f;
    private Rigidbody2D rb;
    private bool active = true;

    public Animator animator;

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
        if (active) {
            Vector2 direction = (Vector2)target.position - rb.position;
            direction.Normalize();
            float rotateMag = Vector3.Cross(direction, transform.up).z;

            rb.angularVelocity = -rotateMag * rotateSpeed;
            rb.velocity = transform.up * speed;
        }

        Debug.LogError(animator.GetBool("Explosion"));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        active = false;

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        animator.SetBool("Explosion", true);
        // speed = 2.5f;

        Debug.Log("test");

        bool dontLoop = true;
        if (dontLoop) {
            FindObjectOfType<AudioManager>().Play("GodotDeath");
            dontLoop = false;
        }

        if (col.gameObject.tag == "Itztli" || col.gameObject.tag == "Tlaloc" && animator.GetBool("Explosion") == false) {
            GameManager.Instance.ResetScene();
        }
    }
}