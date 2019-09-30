using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    /*Auxillary stuff */
    public PlayerController pc;                           //reference to the player
    public bool canTranspose = false, canFissure = false; //ability flags

    /*transpose skill variables */
    public Vector2 pushForce = new Vector2(0, 0);         //the reference vector2 for the transpose force
    public float charge = 0;                              //the temporary variable to charge transpose
    public float minCharge;                               //minimum force the player starts charging
    public float maxCharge;                               //maximum force the player can charge

    private IEnumerator transposeCoroutine;               //coroutine reference to get the colider

    /*fissure variables */
    private IEnumerator fissureCoroutine;               //coroutine reference to get the two objects




    private void Update() {

        //better transpose

        //fissure
        if (canFissure && fissureCoroutine != null && Input.GetKeyDown("g")) {
            StartCoroutine(fissureCoroutine);
            Debug.Log("coroutine started");
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        //aux stuff
        if (col.gameObject.tag == "Object") {
            charge = minCharge;        //reset the charge meter
        }
        if (col.gameObject.tag == "Current") {
            col.GetComponent<LineBehaviour>().toggleConnected();
        }

        //fissure
        if (col.gameObject.tag == "Ground") {
            canFissure = true;
            fissureCoroutine = fissure(col.transform.parent.GetChild(0).GetComponent<Collider2D>(), col.transform.parent.GetChild(1).GetComponent<Collider2D>());
        }
    }

    void OnTriggerExit2D(Collider2D col) 
    {
        if (col.gameObject.tag == "Current") {
            col.GetComponent<LineBehaviour>().resetCurrent();
            col.GetComponent<LineBehaviour>().toggleConnected();
        }

        //fissure
        if (col.gameObject.tag == "Ground") {
            canFissure = false;
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
        if (col != null) col.attachedRigidbody.AddForce(pushForce, ForceMode2D.Impulse);                  //          --theres a null pointer here i'll deal with it when I convert to the other method
        pc.canMove = true;
        //Debug.LogError(pushForce);

        IEnumerator transposeCooldown = timer(0, 3);
        StartCoroutine(transposeCooldown);        
    }

    /*fissure ability */
    private IEnumerator fissure(Collider2D col1, Collider2D col2) {

        //col1.transform = col1.transform + new Vector2(1, 0);
        while (col1.transform.position.x > -2) { 
            float moveX = Mathf.Lerp(0, 0.5f, Time.deltaTime);

            col1.transform.position += Vector3.left*moveX;
            col2.transform.position += Vector3.right*moveX;
            // col1.transform.localScale += Vector3.left*moveX;
            Debug.Log("hit");

            yield return null;
        }
        yield return new WaitForEndOfFrame();
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
