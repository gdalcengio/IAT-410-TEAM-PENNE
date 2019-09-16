using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public bool canTranspose = false, canFissure = false;
    public Vector2 pushForce = new Vector2(0, 0);

    void OnTriggerEnter2D(Collider2D collider) {
        //Debug.Log("enter");
    }
    void OnTriggerStay2D(Collider2D col) {
        //if (canTranspose) transpose();
        //Debug.Log("staying");
    }

    public void transpose(Collider2D col) {
        if (Input.GetKeyDown("space")) {
            if (col.gameObject.tag == "Object") {
                col.attachedRigidbody.AddForce(pushForce, ForceMode2D.Impulse);
            }
        }
    }


}
