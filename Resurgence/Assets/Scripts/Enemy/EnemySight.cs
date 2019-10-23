using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    SpriteRenderer spr;
    public bool objectInRange;
    Transform lineOfSightEnd;
    Transform itztli;

    Transform target;
    //, tlaloc, catalyst, target;

    void Start()
    {
        objectInRange = false;
        // lineOfSightEnd = transform.parent.GetChild(1).GetComponent<Transform>();
        lineOfSightEnd = GetComponentInChildren<Transform>();
        // objects that trigger the enemy
        itztli = GameObject.FindWithTag("Itztli").transform;
        //tlaloc = GameObject.FindWithTag("Tlaloc").transform;
        //catalyst = GameObject.FindWithTag("Catalyst").transform;
        //target = this.gameObject.transform;
    }

    void FixedUpdate()
    {
        if (CanObjectBeSeen()) {
            // spr.color = Color.red;
            // Debug.LogError("This is seen");
            GetComponentInParent<EnemyBehaviour>().ChaseTarget(itztli);
        } else {
            // spr.color = Color.white;
            // Debug.LogError("out of sight");
            if (GetComponentInParent<EnemyBehaviour>().getState() == "enraged") {
                GetComponentInParent<EnemyBehaviour>().setState("patrol");
                GetComponentInParent<EnemyBehaviour>().StopChase(itztli);
            }
        }
    }

    bool CanObjectBeSeen()
    {
        if (objectInRange) {
            Debug.LogError("good1");
            if (ObjectInFOV()) {
                return true;
                Debug.LogError("good2");
                if (!ObjectHiddenByObstacles()) return true;
            } 
        } 
        return false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Itztli" || col.transform.tag == "Tlaloc" || col.transform.tag == "Catalyst") {
            // itztli = col.transform;
            objectInRange = true;
            Debug.Log("object is in range");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Itztli" || col.transform.tag == "Tlaloc" || col.transform.tag == "Catalyst") {
            // itztli = null;
            objectInRange = false;
            Debug.Log("object our of range");
        }
    }

    bool ObjectInFOV()
    {
        // direction from enemy to target
        Vector2 directionToTarget = itztli.position - transform.position;
        Debug.DrawLine(transform.position, itztli.position, Color.magenta);

        // the centre of the enemy's field of view, the direction of looking directly ahead
        Vector2 lineOfSight = lineOfSightEnd.position - transform.position;
        Debug.DrawLine(transform.position, lineOfSightEnd.position, Color.yellow);

        // angle between target position and enemy's centre fov
        float angle = Vector2.Angle(directionToTarget, lineOfSight);

        if (angle < 40f) {
            return true;
        }

        return false;
    }

    bool ObjectHiddenByObstacles()
    {
        float distanceToTarget = Vector2.Distance(transform.position, itztli.position);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, itztli.position - transform.position, distanceToTarget);
        Debug.DrawRay(transform.position, itztli.position - transform.position, Color.blue);
        List<float> distances = new List<float>();
        target = itztli.transform;
        foreach(RaycastHit2D hit in hits) {
            // Debug.LogError(hit.transform.tag);
            if (hit.transform.tag == "Godot") {
                continue;
            }

            if (hit.transform.tag != "Itztli" || hit.transform.tag != "Tlaloc" || hit.transform.tag != "Catalyst") {
                Debug.LogError(hit.transform.tag);
                return true;
            } else {
                return false;
            }
        }
        return false;
    }
}
