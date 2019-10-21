// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;

// public class EnemySight : MonoBehaviour
// {

//     // Use this for initialization
//     public bool playerInRange; // is the player within the enemy's sight range collider (this only checks if the enemy can theoretically see the player if nothing is in the way)
//     [SerializeField]
//     SpriteRenderer spr;

//     [SerializeField]
//     Transform lineOfSightEnd;
//     Transform itztli, tlaloc, catalyst; // a reference to the player for raycasting
//     void Start()
//     {
//         playerInRange = false;
//         // player = GameObject.Find("Player").transform;
//     }


//     void FixedUpdate()
//     {
//         if (CanPlayerBeSeen())
//             spr.color = Color.red;
//         else
//             spr.color = Color.white;
//     }


//     bool CanPlayerBeSeen()
//     {
//         // we only need to check visibility if the player is within the enemy's visual range
//         if (playerInRange)
//         {
//             if (PlayerInFieldOfView())
//                 return (!PlayerHiddenByObstacles());
//             else
//                 return false;

//         }
//         else
//         {
//             // always false if the player is not within the enemy's range
//             return false;
//         }

//         //return playerInRange;

//     }
//     void OnTriggerStay2D(Collider2D other)
//     {
//         // if 'other' is player, the player is seen 
//         // note, we don't really need to check the transform tag since the collision matrix is set to only 'see' collisions with the player layer
//         if (other.transform.tag == "Player")
//             playerInRange = true;
//     }

//     void OnTriggerExit2D(Collider2D other)
//     {
//         // if 'other' is player, the player is seen
//         // note, we don't really need to check the transform tag since the collision matrix is set to only 'see' collisions with the player layer
//         if (other.transform.tag == "Player")
//             playerInRange = false;
//     }

//     bool PlayerInFieldOfView()
//     {
//         // check if the player is within the enemy's field of view
//         // this is only checked if the player is within the enemy's sight range

//         // find the angle between the enemy's 'forward' direction and the player's location and return true if it's within 65 degrees (for 130 degree field of view)

//         Vector2 directionToPlayer = player.position - transform.position; // represents the direction from the enemy to the player    
//         Debug.DrawLine(transform.position, player.position, Color.magenta); // a line drawn in the Scene window equivalent to directionToPlayer

//         Vector2 lineOfSight = lineOfSightEnd.position - transform.position; // the centre of the enemy's field of view, the direction of looking directly ahead
//         Debug.DrawLine(transform.position, lineOfSightEnd.position, Color.yellow); // a line drawn in the Scene window equivalent to the enemy's field of view centre

//         // calculate the angle formed between the player's position and the centre of the enemy's line of sight
//         float angle = Vector2.Angle(directionToPlayer, lineOfSight);

//         // if the player is within 65 degrees (either direction) of the enemy's centre of vision (i.e. within a 130 degree cone whose centre is directly ahead of the enemy) return true
//         if (angle < 65)
//             return true;
//         else
//             return false;
//     }

//     bool PlayerHiddenByObstacles()
//     {

//         float distanceToPlayer = Vector2.Distance(transform.position, player.position);
//         RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, player.position - transform.position, distanceToPlayer);
//         Debug.DrawRay(transform.position, player.position - transform.position, Color.blue); // draw line in the Scene window to show where the raycast is looking
//         List<float> distances = new List<float>();

//         foreach (RaycastHit2D hit in hits)
//         {
//             // ignore the enemy's own colliders (and other enemies)
//             if (hit.transform.tag == "Enemy")
//                 continue;

//             // if anything other than the player is hit then it must be between the player and the enemy's eyes (since the player can only see as far as the player)
//             if (hit.transform.tag != "Player")
//             {
//                 return true;
//             }
//         }

//         // if no objects were closer to the enemy than the player return false (player is not hidden by an object)
//         return false;

//     }

// }






using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    SpriteRenderer spr;
    public bool objectInRange;
    Transform lineOfSightEnd;
    Transform itztli;
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
            Debug.LogError("This is seen");
        } else {
            // spr.color = Color.white;
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

        if (angle < 65f) {
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

        foreach(RaycastHit2D hit in hits) {
            if (hit.transform.tag == "Godot") {
                continue;
            }

            if (hit.transform.tag != "Itztli" || hit.transform.tag != "Tlaloc" || hit.transform.tag != "Catalyst") {
                return true;
            }
        }
        return false;
    }
}
