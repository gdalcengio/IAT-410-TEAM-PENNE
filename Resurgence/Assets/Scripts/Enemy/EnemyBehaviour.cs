using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public string state;

    private float speed;

    bool blocked = false;

    void Start()
    {
        state = "patrol";
        speed = GetComponent<EnemyMovement>().speed;
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
        GetComponent<EnemyMovement>().StopCoroutine("Patrol");
        StartCoroutine(Delay(2));
        state = "enraged";
        StartCoroutine(Chase(target));
    }

    public void StopChase(Transform target)
    {
        StartCoroutine(Delay(2));
        StopCoroutine(Chase(target));
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
            if (!blocked) this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(target.position.x, transform.position.y), speed);
            yield return null;
        }
    }

    IEnumerator Delay(float sec)
    {
        yield return new WaitForSeconds(sec);
    }
}
