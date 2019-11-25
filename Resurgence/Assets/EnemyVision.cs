using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public float fieldOfViewAngle = 110f;
    public bool playerInSight;
    public Vector3 personalLastSighting;

    // private NavMeshAgent nav;
    private CircleCollider2D col;
    private Animator anim;
    private LastPlayerSighting lastPlayerSighting;
    private GameObject Itztli, Tlaloc;
    private Animator itztliAnim, tlalocAnim;
    private Vector3 previousSighting;

    void Awake()
    {
        // nav = GetComponent<NavMeshAgent>();
        col = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        // lastPlayerSighting= GameObject.Find
        itztliAnim = Itztli.GetComponent<Animator>();
        tlalocAnim = Tlaloc.GetComponent<Animator>();
        // playerHealth
        // hash

        personalLastSighting = lastPlayerSighting.resetPosition;
        previousSighting = lastPlayerSighting.resetPosition;
    }

    void Update()
    {
        if (lastPlayerSighting.position != previousSighting) personalLastSighting = lastPlayerSighting.position;

        previousSighting = lastPlayerSighting.position;

        // if (playerHealth)


    }

    void OnTriggerStay(CircleCollider2D other)
    {
        if (other.gameObject == Itztli || other.gameObject == Tlaloc) {
            playerInSight = false;

            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fieldOfViewAngle*0.5f) {
                RaycastHit hit;

                if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius)) {
                    playerInSight = true;
                    lastPlayerSighting.position = other.transform.position;
                }
            }
        }
    }

    void OnTriggerExit(CircleCollider2D other) 
    {
        if (other.gameObject == Itztli || other.gameObject == Tlaloc) playerInSight = false;
    }
}
