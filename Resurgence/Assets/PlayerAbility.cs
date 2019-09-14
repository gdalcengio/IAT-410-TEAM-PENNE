using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public Vector2 pushForce = new Vector2(0, 0);
    void OnTriggerStay2D(Collider2D col) {
        if (Input.GetKeyDown("space")) {
            if (col.gameObject.tag == "Object") {
                col.attachedRigidbody.AddForce(pushForce, ForceMode2D.Impulse);
            }
        }
    }


}
