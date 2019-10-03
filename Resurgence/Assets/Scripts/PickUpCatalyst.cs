﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCatalyst : MonoBehaviour
{
    public EarthAbility pScript;
    private GameObject catalyst = null;
    private bool hasCatalyst = false;


    private void Update() {
        //catalyst pick up and throw
        if (catalyst != null && Input.GetButtonDown("Switch")){
            Rigidbody2D crb = catalyst.GetComponent<Collider2D>().attachedRigidbody;
            if (!hasCatalyst ) {
                Debug.Log("cat c boi");
                //crb.velocity = new Vector2(0f, 0f);
                //crb.mass = 0;
                // crb.useGravity = false;
                crb.isKinematic = true;
                catalyst.transform.SetParent(this.transform);
                hasCatalyst = true;
                //crb.simulated = false;
                //disallow abilities
                if (pScript!=null) {
                    pScript.canFissure = false;
                    pScript.canTranspose = false;
                }
            } else {
                Debug.LogError("chuck");
                //crb.AddForce(new Vector2(5, 5));
                // crb.useGravity = true;
                crb.isKinematic = false;
                catalyst.transform.SetParent(null);
                hasCatalyst = false;
                catalyst = null;
                //crb.simulated = true;
                //re-allow abilities
                if (pScript!=null) {
                    pScript.canFissure = true;
                    pScript.canTranspose = true;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        //catalyst in range
        if (col.gameObject.tag == "Catalyst") {
            catalyst = col.gameObject;
            Debug.Log("Catalyst");
        }
    }

    void OnTriggerExit2D(Collider2D col) 
    {
        //catalyst out of range
        if (catalyst != null && col.gameObject.tag =="Catalyst") {
            catalyst = null;
            Debug.Log("fuck");
        }
    }
}
