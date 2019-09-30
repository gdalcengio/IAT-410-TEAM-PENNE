using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCatalyst : MonoBehaviour
{
    public PlayerAbility pScript;
    private GameObject catalyst = null;
    private bool hasCatalyst = false;


    private void Update() {
        //catalyst pick up and throw
        if (catalyst != null && Input.GetKeyDown("c")){
            Rigidbody2D crb = catalyst.GetComponent<Collider2D>().attachedRigidbody;
            if (!hasCatalyst ) {
                Debug.Log("cat c boi");
                catalyst.transform.SetParent(this.transform);
                hasCatalyst = true;
                crb.isKinematic = true;
                //disallow abilities
                pScript.canFissure = false;
                pScript.canTranspose = false;
            } else {
                Debug.LogError("chuck");
                crb.AddForce(new Vector2(5, 5));
                catalyst.transform.SetParent(null);
                catalyst = null;
                hasCatalyst = false;
                crb.isKinematic = false;
                //re-allow abilities
                pScript.canFissure = true;
                pScript.canTranspose = true;
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
