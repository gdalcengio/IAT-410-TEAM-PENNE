using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    SpriteRenderer spr;
    public bool objectInRange;
    Transform lineOfSightEnd;
    Transform itzli, tlaloc, catalyst;
    Transform target = null;

    void Start()
    {
        objectInRange = false;

        // objects that trigger the enemy
        itzli = GameObject.FindWithTag("Itzli").transform;
        tlaloc = GameObject.FindWithTag("Tlaloc").transform;
        catalyst = GameObject.FindWithTag("Catalyst").transform;
    }

    void FixedUpdate()
    {
        if (CanObjectBeSeen()) {
            spr.color = Color.red;
        } else {
            spr.color = Color.white;
        }
    }

    bool CanObjectBeSeen()
    {
        if (objectInRange) {
            if (ObjectInFOV()) {
                return (!ObjectHiddenByObstacles());
            } else {
                return false;
            }
        }
        return false;
    }

    void onTriggerStay2D(Collider2D col)
    {
        if (col.transform.tag == "Itzli" || col.transform.tag == "Tlaloc" || col.transform.tag == "Catalyst") {
            target = col.transform;
            objectInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.tag == "Itzli" || col.transform.tag == "Tlaloc" || col.transform.tag == "Catalyst") {
            target = null;
            objectInRange = false;
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

        if (angle < 65) {
            return true;
        }
        return false;
    }

    bool ObjectHiddenByObstacles()
    {
        float distanceToTarget = Vector2.Distance(transform.position, target.position);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, target.position - transform.position, distanceToTarget);
        Debug.DrawRay(transform.position, target.position - transform.position, Color.blue);
        List<float> distances = new List<float>();

        foreach(RaycastHit2D hit in hits) {
            if (hit.transform.tag == "Godot") {
                continue;
            }

            if (hit.transform.tag != "Itzli" || hit.transform.tag != "Tlaloc" || hit.transform.tag != "Catalyst") {
                return true;
            }
        }
        return false;
    }
}
