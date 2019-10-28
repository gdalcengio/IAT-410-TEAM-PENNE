using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCatalyst : MonoBehaviour
{
    private GameObject catalyst = null;
    private bool hasCatalyst = false;
    private EarthAbility iScript = null;
    private WaterAbilities tScript = null;
    private string parentName;

    private void Start() {
        parentName = transform.parent.name;

        if (parentName == "Itztli") {
            iScript = transform.parent.GetComponentInChildren<EarthAbility>(); 
        } else {
            tScript = transform.parent.GetComponentInChildren<WaterAbilities>();
        }
    }
    private void Update() {
        //catalyst pick up and throw
        if (catalyst != null && ((parentName == "Itztli" != null && Input.GetButtonDown("iCat")) || (parentName == "Tlaloc" && Input.GetButtonDown("tCat")))){
            Rigidbody2D crb = catalyst.GetComponent<Collider2D>().attachedRigidbody;
            if (!hasCatalyst && catalyst.transform.parent == null) {
                Debug.Log("cat c boi");
                crb.velocity = new Vector2(0f, 0f);
                //crb.mass = 0;
                // crb.useGravity = false;
                crb.isKinematic = true;
                // crb.detectCollisions = false;
                catalyst.transform.SetParent(this.transform);
                catalyst.transform.localPosition = new Vector2(0, 0);
                hasCatalyst = true;
                //crb.simulated = false;
                //disallow abilities
                if (parentName == "Itztli") {
                    iScript.lockAbilities();
                } else {
                    tScript.lockAbilities();
                }
            } else {
                Debug.LogError("chuck");
                //crb.AddForce(new Vector2(5, 5));
                // crb.useGravity = true;
                crb.isKinematic = false;
                // crb.detectCollisions = true;
                catalyst.transform.SetParent(null);
                hasCatalyst = false;
                catalyst = null;
                //crb.simulated = true;
                //re-allow abilities
                if (parentName == "Itztli") {
                    iScript.unlockAbilities();
                } else {
                    tScript.unlockAbilities();
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        //catalyst in range
        if (col.gameObject.tag == "Catalyst") {
            catalyst = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col) 
    {
        //catalyst out of range
        if (catalyst != null && col.gameObject.tag == "Catalyst") {
            catalyst = null;
        }
    }

    public bool GetHasCatatlyst()
    {
        return hasCatalyst;
    }
}
