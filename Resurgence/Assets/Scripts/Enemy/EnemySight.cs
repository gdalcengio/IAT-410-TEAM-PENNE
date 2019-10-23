using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    SpriteRenderer spr;
    public bool objectInRange;
    Transform lineOfSightEnd;
    Transform itztli, tlaloc, catalyst;

    Transform target;
    //, tlaloc, catalyst, target;
    Transform seenTarget;

    void Start()
    {
        objectInRange = false;
        // lineOfSightEnd = transform.parent.GetChild(1).GetComponent<Transform>();
        lineOfSightEnd = GetComponentInChildren<Transform>();
        // objects that trigger the enemy
        itztli = GameObject.FindWithTag("Itztli").transform;
        tlaloc = GameObject.FindWithTag("Tlaloc").transform;
        catalyst = GameObject.FindWithTag("Catalyst").transform;
        //target = this.gameObject.transform;
        // GetComponent<EnemyBehaviour>().setState("patrol");
    }

    void FixedUpdate()
    {
        // Debug.LogError(!ObjectHiddenByObstacles());
        if (CanObjectBeSeen()) {
            if (GetComponentInParent<EnemyBehaviour>().getState() == "patrol") StopCoroutine("Patrol");
            // spr.color = Color.red;
            // Debug.LogError(GetComponent<EnemyBehaviour>().state);
            GetComponentInParent<EnemyBehaviour>().ChaseTarget(seenTarget);
        } else {
            // Debug.LogError(GetComponentInParent<EnemyBehaviour>().getState());
            if (GetComponentInParent<EnemyBehaviour>().getState() == "enraged") {
                GetComponentInParent<EnemyBehaviour>().StopChase(seenTarget);
            }
        }
        
        // if (!objectInRange || !ObjectInFOV()) {
        //     // spr.color = Color.white;
        //     // Debug.LogError("out of sight");
        //     if (GetComponentInParent<EnemyBehaviour>().getState() == "enraged") {
        //         Debug.LogError("now patrol");
        //         // GetComponentInParent<EnemyBehaviour>().setState("patrol");
        //         GetComponentInParent<EnemyBehaviour>().StopChase(target);
        //     }
        // }
    }

    bool CanObjectBeSeen()
    {
        if (objectInRange) {
            if (ObjectInFOV()) {
                // Debug.LogError("can't reach");
                if (!ObjectHiddenByObstacles()) {
                    // Debug.LogError("not hidden");
                    return true;
                }
            } 
        } 
        return false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Itztli" || col.transform.tag == "Tlaloc" || col.transform.tag == "Catalyst") {
            target = col.transform;
            objectInRange = true;
            // Debug.Log("object is in range");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Itztli" || col.transform.tag == "Tlaloc" || col.transform.tag == "Catalyst") {
            // itztli = null;
            objectInRange = false;
            // Debug.Log("object our of range");
        }
    }

    bool ObjectInFOV()
    {
        // direction from enemy to target
        Vector2 directionToTarget = target.position - transform.position;
        Debug.DrawLine(transform.position, target.position, Color.magenta);

        // the centre of the enemy's field of view, the direction of looking directly ahead
        Vector2 lineOfSight = lineOfSightEnd.position - transform.position;
        Debug.DrawLine(transform.position, lineOfSightEnd.position, Color.yellow);

        // angle between target position and enemy's centre fov
        float angle = Vector2.Angle(directionToTarget, lineOfSight);
        Debug.LogError(angle);

        if (angle < 40f) {
            return true;
        }

        return false;
    }

    bool ObjectHiddenByObstacles()
    {
        float distanceToTarget = Vector2.Distance(transform.position, target.position);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, target.position - transform.position, distanceToTarget);
        Debug.DrawRay(transform.position, target.position - transform.position, Color.blue);
        // List<float> distances = new List<float>();
        // target = itztli.transform;
        foreach(RaycastHit2D hit in hits) {
            // if (hit.transform.tag != "Godot") Debug.LogError(hit.transform.name);
            // Debug.LogError(hit.transform.tag);
            if (hit.transform.tag == "Godot") {
                continue;
            }

            if (hit.transform.CompareTag("Itztli") || hit.transform.CompareTag("Tlaloc") || hit.transform.tag == "Catalyst") {
                seenTarget = hit.transform;
                return false;
            } else {
                return true;
            }

            // if (!hit.transform.CompareTag("Itztli") || !hit.transform.CompareTag("Tlaloc") || hit.transform.tag != "Catalyst") {
            //     // if (hit.transform.gameObject.name == "Itztli") {
            //         Debug.LogError(hit.transform.tag);
            //         return true;
            //     // }
            // }
            // seenTarget = hit.transform;
        }
        // Debug.LogError("should reach this");
        return true;
    }
}
