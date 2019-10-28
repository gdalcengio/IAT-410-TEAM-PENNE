using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed;
    public float distance;
    int currentPoint;
    SpriteRenderer sprite;

   private float prevX, currX;

   Transform point1, point2;
   GameObject parent;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();


        StartCoroutine("Patrol");

        currX = this.transform.position.x;

        parent = this.transform.parent.gameObject;

        point1 = parent.transform.GetChild(1);
        point2 = parent.transform.GetChild(2);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col != null) {
            // determine which side of wall is collided with...
            Vector3 hit = col.contacts[0].normal; // normal tangent of collision
            float angle = Vector3.Angle(hit, Vector3.up);

            // down
            if (Mathf.Approximately(angle, 0)) {
                Debug.Log("down");
            }

            // up
            if (Mathf.Approximately(angle, 180)) {
                Debug.Log("up");
            }

            // side collision
            if (Mathf.Approximately(angle, 90)) {
                Vector3 cross = Vector3.Cross(Vector3.forward, hit);
                if (cross.y > 0) { // left side of player
                    if (prevX > currX) { // moving left
                        if ((col.gameObject.layer == 9 || col.gameObject.layer == 8) && col.gameObject.transform.position.x < currX) {
                            if (currentPoint >= patrolPoints.Length) {
                                currentPoint = 0; // reset to zero
                            }
                            currentPoint++;
                            // yield return new WaitForSeconds(0.5f);
                            Vector2 newScale = this.transform.localScale;
                            newScale.x *= -1;
                            this.transform.localScale = newScale;
                        }
                    }
                } else { // right side of player
                    if (prevX < currX) { // moving right
                        if ((col.gameObject.layer == 9 || col.gameObject.layer == 8) && col.gameObject.transform.position.x > currX) {
                            if (currentPoint >= patrolPoints.Length) {
                                currentPoint = 0; // reset to zero
                            }
                            currentPoint++;
                            // yield return new WaitForSeconds(0.5f);
                            Vector2 newScale = this.transform.localScale;
                            newScale.x *= -1;
                            this.transform.localScale = newScale;
                        }
                    }
                }
            }
        }
    }

    IEnumerator Patrol()
    {
        yield return new WaitForSeconds(1);
        while (GetComponent<EnemyBehaviour>().getState() == "patrol") { // consistently move
            if (currentPoint >= patrolPoints.Length) {
                currentPoint = 0; // reset to zero
            }

            if (GetComponent<EnemyBehaviour>().getState() == "patrol" && this.transform.position.x == patrolPoints[currentPoint].position.x) {
                currentPoint++;

                yield return new WaitForSeconds(0.5f);
                Vector2 newScale = this.transform.localScale;
                newScale.x *= -1;
                this.transform.localScale = newScale;
            }

            if (currentPoint >= patrolPoints.Length) {
                currentPoint = 0; // reset to zero
            }

            this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(patrolPoints[currentPoint].position.x, transform.position.y), speed*Time.deltaTime);

            prevX = currX;
            currX = this.transform.position.x;

            yield return null;
        }
    }

    public float getPrevX()
    {
        return prevX;
    }

    public float getCurrX()
    {
        return currX;
    }
}
