using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public string state = "patrol";

    private float speed, savedSpeed;

    bool blocked = false;

    public int health = 1;

    public float prevX, currX;

    void Start()
    {
        state = "patrol";
        speed = savedSpeed = GetComponent<EnemyMovement>().speed;
    }

    void Update() 
    {
        // if (state == "enraged") Debug.LogError(state);
        death();
    }

    void death()
    {
        if (health == 0) {
            Destroy(this.transform.parent.gameObject);
        }
    }

    public string getState()
    {
        return state;
    }

    // state should only equal "patrol" or "enraged"
    public void setState(string input)
    {
        state = input;
    }

    public void ChaseTarget(Transform target)
    {
        // GetComponent<EnemyMovement>().StopCoroutine("Patrol");
        state = "enraged";
        StartCoroutine(Delay(1));
        StartCoroutine(Chase(target));
    }

    public void StopChase(Transform target)
    {
        state = "patrol";
        StartCoroutine(Delay(1));
        StopCoroutine(Chase(target));
        GetComponent<EnemyMovement>().speed = 0.1f;
        GetComponent<EnemyMovement>().StartCoroutine("Patrol");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col != null && (col.gameObject.layer == 9)) {
            blocked = true;
        }
    }

    void OnCollisionExit2D(Collision2D col) {
        if(col != null && (col.gameObject.layer == 9)) {
            blocked = false;
        }
    }

    IEnumerator Chase(Transform target)
    {
        while (state == "enraged") {
            // if (state == "patrol") Debug.LogError("no longer enraged");
            if (!blocked) {
                prevX = GetComponent<EnemyMovement>().getPrevX();
                currX = GetComponent<EnemyMovement>().getCurrX();
                this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(target.position.x, transform.position.y), 0.15f);
                if (target.position.x > this.transform.position.x && prevX > currX) {
                    Vector2 newScale = this.transform.localScale;
                    newScale.x *= -1;
                    this.transform.localScale = newScale;
                } else if (target.position.x < this.transform.position.x && prevX < currX) {
                    Vector2 newScale = this.transform.localScale;
                    newScale.x *= -1;
                    this.transform.localScale = newScale;
                }
            }
            yield return null;
        }
    }

    IEnumerator Delay(float sec)
    {
        yield return new WaitForSeconds(sec);
    }
}
