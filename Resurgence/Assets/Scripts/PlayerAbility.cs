using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    private GameObject inHand = null;   //to hold the catalyst
    public Rigidbody2D playerRB;
    public PlayerController pc;
    public bool canTranspose = false, canFissure = false;
    public Vector2 pushForce = new Vector2(0, 0);
    public float charge = 0;
    public float minCharge;
    public float maxCharge;

    private IEnumerator transposeCoroutine;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Object") {
            charge = minCharge;        //reset the charge meter
        }
    }

    void OnTriggerExit2D() {
        //StopCoroutine(transposeCoroutine);
    }
    void OnTriggerStay2D(Collider2D col) {
    //         transposeCoroutine = transpose(col);
    //         StartCoroutine(transposeCoroutine);
        //Debug.Log("staying");
        //if (canTranspose) {
            //charge = minCharge;        //reset the charge meter
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
        } else if (Input.GetKeyDown("down")) { //pick up catalyst
            if (col != null && col.gameObject.tag == "Catalyst") {
                //if pickedUp is false this wont work
                //inHand = col.GameObject;
                col.transform.SetParent(transform.parent, true);
                if (col.transform.parent != null) col.attachedRigidbody.simulated = false;
            }
        }
        
    }

    // public void transpose(Collider2D col) {
    //     if (Input.GetKey("space")) {
    //         if (col.gameObject.tag == "Object") {
    //             if ((pc.facingRight && pushForce.x < 0) || (!pc.facingRight && pushForce.x > 0)) {
    //                 pushForce.x *= -1;
    //             }
    //             col.attachedRigidbody.AddForce(pushForce, ForceMode2D.Impulse);
    //         }
    //     }
    // }
    private IEnumerator chargeTranspose(Collider2D col) {
        // if (!canTranspose) yield return null;  //aborts if cooldown

        // while (col.gameObject.tag != "Object") {
        //     yield return null; //just loops until player is in front of a transposable object
        // }

        while (Input.GetKey("space")) {
            // pc.canMove = false;    //stops player movement
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
        // if (canTranspose) {
        // } else {
        //     Debug.LogError("cooldown on transpose!");
        Debug.LogError(pushForce);
        // }

        IEnumerator transposeCooldown = timer(0, 3);
        StartCoroutine(transposeCooldown);        
    }

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
