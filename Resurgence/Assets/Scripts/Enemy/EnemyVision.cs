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
    public CircleCollider2D col;
    private Animator anim;
    private Vector3 lastPlayerSighting;
    private GameObject Itztli, Tlaloc;
    private Animator itztliAnim, tlalocAnim;
    private Vector3 previousSighting;
    private float currX, prevX;
    private Vector3 facing;

    void Awake()
    {
        Itztli = GameObject.Find("Itztli");
        Tlaloc = GameObject.Find("Tlaloc");
        // nav = GetComponent<NavMeshAgent>();
        col = this.GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        // lastPlayerSighting= GameObject.Find
        itztliAnim = Itztli.GetComponent<Animator>();
        tlalocAnim = Tlaloc.GetComponent<Animator>();
        // playerHealth
        // hash

        personalLastSighting = lastPlayerSighting = new Vector3(0f,0f,0f);
        previousSighting = lastPlayerSighting = new Vector3(0f,0f,0f);

        currX = this.transform.position.x;
        facing = this.transform.right;
    }

    void Update()
    {
        if (lastPlayerSighting != previousSighting) personalLastSighting = lastPlayerSighting;

        previousSighting = lastPlayerSighting;

        // if (playerHealth)

        prevX = currX;
        currX = this.transform.position.x;

        if (facing == this.transform.right) {
            if (currX >= prevX) facing = this.transform.right;
            else facing = this.transform.right*-1;
        } else if (facing == this.transform.right*-1) {
            if (currX <= prevX) facing = this.transform.right*-1;
            else facing = this.transform.right;
        }

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
            // float angle = Vector2.Angle(this.transform.position, other.transform.position);
// Debug.LogError(Vector3.Angle(direction, other.transform.position - this.transform.position));
            // if (angle < fieldOfViewAngle) {
            if (Vector3.Angle(facing, other.transform.position - this.transform.position) < 22.5) {
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, other.transform.position - transform.position, Vector2.Distance(transform.position, other.transform.position));
                Debug.DrawRay(transform.position, other.transform.position - transform.position, Color.blue);
                if (hits[0].transform.gameObject == other.gameObject) {
                    Debug.LogError("got here");
                    playerInSight = true;
                    seenPlayer = other.gameObject;
                    lastPlayerSighting = other.transform.position;
                }
                // if (Physics.Raycast(facing, direction.normalized, out hit, col.radius)) {
                //     playerInSight = true;
                //     seenPlayer = other.gameObject;
                //     lastPlayerSighting = other.transform.position;
                    
                // }
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
