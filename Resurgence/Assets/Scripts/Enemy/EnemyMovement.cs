using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed;
    public float distance;
    int currentPoint;
    SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        StartCoroutine("Patrol");
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position,transform.localScale.x*Vector3.left, distance); // check in front of him
        Debug.DrawRay(transform.position, new Vector2(transform.position.x+distance, transform.position.y), Color.red);

        // player and catalyst detection
        if (hit.collider != null && (hit.collider.tag == "Itzli" || hit.collider.tag == "Tlaloc" || hit.collider.tag == "Catalyst")) {
            Debug.LogError("object detected");
            StopCoroutine("Patrol");
        }
    }

    IEnumerator Patrol()
    {
        while (true) { // consistently move
            if (this.transform.position.x == patrolPoints[currentPoint].position.x) {
                currentPoint++;

                yield return new WaitForSeconds(0.5f);
            }

            if (currentPoint >= patrolPoints.Length) {
                currentPoint = 0; // reset to zero
            }

            this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(patrolPoints[currentPoint].position.x, transform.position.y), speed);

            if (transform.position.x < patrolPoints[currentPoint].position.x) {
                sprite.flipX = true;
            } else if (transform.position.x > patrolPoints[currentPoint].position.x) {
                sprite.flipX = false;
            }

            yield return null;
        }
    }
}
