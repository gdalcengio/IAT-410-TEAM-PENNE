using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public float fieldOfViewAngle = 110f;
    public bool playerInSight;
    public Vector3 personalLastSighting;
    public GameObject seenPlayer;

    // private NavMeshAgent nav;
    private CircleCollider2D col;
    private Animator anim;
    private Vector3 lastPlayerSighting;
    private GameObject Itztli, Tlaloc;
    private Animator itztliAnim, tlalocAnim;
    private Vector3 previousSighting;

    void Awake()
    {
        Itztli = GameObject.Find("Itztli");
        Tlaloc = GameObject.Find("Tlaloc");
        // nav = GetComponent<NavMeshAgent>();
        col = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        // lastPlayerSighting= GameObject.Find
        itztliAnim = Itztli.GetComponent<Animator>();
        tlalocAnim = Tlaloc.GetComponent<Animator>();
        // playerHealth
        // hash

        personalLastSighting = lastPlayerSighting = new Vector3(0f,0f,0f);
        previousSighting = lastPlayerSighting = new Vector3(0f,0f,0f);
    }

    void Update()
    {
        if (lastPlayerSighting != previousSighting) personalLastSighting = lastPlayerSighting;

        previousSighting = lastPlayerSighting;

        // if (playerHealth)

        if (playerInSight) {
            if (GetComponentInParent<EnemyBehaviour>().getState() == "patrol") StopCoroutine("Patrol");
            if (GetComponentInParent<EnemyBehaviour>().getState() != "enraged") GetComponentInParent<EnemyBehaviour>().ChaseTarget(seenPlayer.transform);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == Itztli || other.gameObject == Tlaloc) {
            playerInSight = false;

            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fieldOfViewAngle) {
                RaycastHit hit;

                if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius)) {
                    playerInSight = true;
                    seenPlayer = other.gameObject;
                    lastPlayerSighting = other.transform.position;
                    Debug.LogError("angry");
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject == Itztli || other.gameObject == Tlaloc) {
            playerInSight = false;
            seenPlayer = null;
        }
    }
}
