using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    /*Auxillary stuff */
    public PlayerController pc;                           //reference to the player
    public bool canTranspose = false, canFissure = false; //ability flags
    private GameObject catalyst = null;
    private bool hasCatalyst = false;

    /*transpose skill variables */
    public Vector2 pushForce = new Vector2(0, 0);         //the reference vector2 for the transpose force
    public float charge = 0;                              //the temporary variable to charge transpose
    public float minCharge;                               //minimum force the player starts charging
    public float maxCharge;                               //maximum force the player can charge

    private IEnumerator transposeCoroutine;               //coroutine reference to get the colider

    /*fissure variables */




    private void Update() {
        //catalyst pick up and throw
        if (catalyst != null){
            Rigidbody2D crb = catalyst.GetComponent<Collider2D>().attachedRigidbody;
            if (!hasCatalyst && Input.GetKeyDown("c")) {
                Debug.Log("cat c boi");
                catalyst.transform.SetParent(this.transform);
                hasCatalyst = true;
                crb.isKinematic = true;
            } else if (Input.GetKeyDown("c")) {
                Debug.LogError("chuck");
                crb.AddForce(new Vector2(5, 5));
                catalyst.transform.SetParent(null);
                catalyst = null;
                hasCatalyst = false;
                crb.isKinematic = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        //catalyst pick up
        if (col.gameObject.tag == "Catalyst") {
            catalyst = col.gameObject;
            Debug.Log("Catalyst");
        }

        if (col.gameObject.tag == "Object") {
            charge = minCharge;        //reset the charge meter
        }
        if (col.gameObject.tag == "Current") {
            col.GetComponent<LineBehaviour>().toggleConnected();
        }
    }

    void OnTriggerExit2D(Collider2D col) 
    {
        //catalyst pick up
        if (catalyst != null && col.gameObject.tag =="Catalyst") {
            catalyst = null;
            Debug.Log("fuck");
        }

        if (col.gameObject.tag == "Current") {
            col.GetComponent<LineBehaviour>().resetCurrent();
            col.GetComponent<LineBehaviour>().toggleConnected();
        }
    }


    void OnTriggerStay2D(Collider2D col) {
        /*transpose ability */
        if (Input.GetKey("space")) {
            transposeCoroutine = chargeTranspose(col);
            if (canTranspose) {
                charge = minCharge;
                StartCoroutine(transposeCoroutine); //begins to charge if cooldown
                pc.canMove = false; //stops player movement
                // playerRB.constraints = RigidbodyConstraints2D.FreezePosition;
                pc.freeze();
                canTranspose = false;
            }
        } 
        /*script to pick up catalyst */
        // if (col.gameObject.tag == "Catalyst")
        //     catalyst = col.GameObject; //          --will deal with this later
        // else if (Input.GetKeyDown("down"))
        // {
        //     {
        //         //if pickedUp is false this wont work
        //         col.transform.SetParent(transform.parent, true);
        //         if (col.transform.parent != null) col.attachedRigidbody.simulated = false;
        //     }
        // }

        //else 
        if (Input.GetKeyDown(KeyCode.T)) {
            if (col != null && col.gameObject.tag == "BinarySwitch") {
                col.GetComponent<SwitchBehaviour>().toggleState();
            }
        }

        if (col != null && col.gameObject.tag == "Current") {
            col.GetComponent<LineBehaviour>().setPosition(1, new Vector2(pc.transform.position.x, 0f));
        }
    }


    private IEnumerator chargeTranspose(Collider2D col) {

        while (Input.GetKey("space")) {
            // pc.canMove = false;    //stops player movement   --might deal with it better later
            if (charge < maxCharge) charge += 20;           //increases charge meter
            // Debug.LogError(charge);
            yield return new WaitForSeconds(0.2f);
        }
        pc.unfreeze();

        pushForce = new Vector2(charge, charge);

        if ((pc.facingRight && pushForce.x < 0) || (!pc.facingRight && pushForce.x > 0)) {
            pushForce.x *= -1;
        }

        Debug.LogError(pushForce);
        if (col != null) col.attachedRigidbody.AddForce(pushForce, ForceMode2D.Impulse);
        pc.canMove = true;
        //Debug.LogError(pushForce);

        IEnumerator transposeCooldown = timer(0, 3);
        StartCoroutine(transposeCooldown);        
    }

    /*expects a start time and end time to target to (for adaptability) */
    private IEnumerator timer(int time, int endTime) {
        while (time < endTime) {
            time++;
            yield return new WaitForSeconds(1);
        }

        canTranspose = true;
    }

    private void freezeObject(Collider2D col) {
        col.attachedRigidbody.bodyType = RigidbodyType2D.Static;
    }

    private void unfreezeObject(Collider2D col) {
        col.attachedRigidbody.bodyType = RigidbodyType2D.Dynamic;
    }

}
