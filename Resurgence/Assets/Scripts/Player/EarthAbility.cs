using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthAbility : MonoBehaviour
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
    private GameObject fissureWall;                     //to test if the wall is fissureable

    private void Update() {
        // //for consistency and error handling
        if (!Input.GetButton("Transpose") && !Input.GetKey("u"))
        {
            pc.unfreeze();
            pc.abilityState = PlayerController.State.Ready;
        }
        // if (pc.abilityState == PlayerController.State.Busy) return;

        //better transpose
        if (Input.GetButtonDown("Transpose") || Input.GetKeyDown("u")) {
            if (canTranspose && transposeCoroutine != null) {
                canTranspose = false;
                //stops player movement
                pc.canMove = false; 
                pc.freeze();
                pc.abilityState = PlayerController.State.Busy;
                
                StartCoroutine(transposeCoroutine); //begins to charge if not on cooldown
            }
        }

        //fissure
        if (Input.GetButtonDown("Fissure") || Input.GetKeyDown("i")) {
            if (canFissure) {
                if (fissureWall != null) {
                    Destroy(fissureWall);
                    fissureWall = null;
                    canFissure = false;
                } else if (fissureCoroutine != null) {
                    StartCoroutine(fissureCoroutine);
                    Debug.Log("fissure coroutine started");
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        //aux stuff
        if (col.gameObject.tag == "Current") {
            col.GetComponent<LineBehaviour>().toggleConnected();
        }

        //transpose
        if (col.gameObject.tag == "Object") {
            canTranspose = true;
            charge = minCharge;        //reset the charge meter
            transposeCoroutine = chargeTranspose(col);
        }
        
        //fissure
        if (col.gameObject.tag == "Fissurable") {
            canFissure = true;
            Debug.Log("check");
            fissureCoroutine = fissure(col.transform.parent.GetChild(0).GetComponent<Collider2D>(), 
                                       col.transform.parent.GetChild(1).GetComponent<Collider2D>()
                                       );
        } else if (col.gameObject.tag == "Cracked") {
            canFissure = true;
            fissureWall = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col) 
    {
        if (col.gameObject.tag == "Current") {
            col.GetComponent<LineBehaviour>().resetCurrent();
            col.GetComponent<LineBehaviour>().toggleConnected();
        }

        //transpose
        if (col.gameObject.tag == "Object") {
            canTranspose = false;
        }

        //fissure
        if (col.gameObject.tag == "Fissurable" || col.gameObject.tag == "Cracked") {
            canFissure = false;
        }
    }

    void OnTriggerStay2D(Collider2D col) {
        if (Input.GetButtonDown("Switch")) {
            if (col != null && col.gameObject.tag == "BinarySwitch") {
                col.GetComponent<SwitchBehaviour>().toggleState();
            }
        }

        if (col != null && col.gameObject.tag == "Current") {
            col.GetComponent<LineBehaviour>().setPosition(1, new Vector2(pc.transform.position.x, 0f));
        }
    }













    private IEnumerator chargeTranspose(Collider2D col) {
        while (pc.abilityState == PlayerController.State.Busy) {
            if (charge < maxCharge) charge += 20;           //increases charge meter
            //if (Input.GetButtonUp("Transpose") || Input.GetKeyUp("u")) pc.abilityState = PlayerController.State.Ready;
            yield return null;
        }

        pushForce = (Input.GetAxis("I_Up") > 0) ? new Vector2(charge, charge) : new Vector2(charge * 1.2f, 0);

        if ((pc.facingRight && pushForce.x < 0) || (!pc.facingRight && pushForce.x > 0)) {
            pushForce.x *= -1;
        }

        //Debug.LogError(pushForce);
        if (col != null) col.attachedRigidbody.AddForce(pushForce, ForceMode2D.Impulse);

        //camera shake
        StartCoroutine(CameraManager.Instance.cameraShake(.15f, .1f));

        pc.unfreeze();
        pc.canMove = true;
        //Debug.LogError(pushForce);

        IEnumerator transposeCooldown = timer(0, 3);
        StartCoroutine(transposeCooldown);        //done transpose ability, now it's on cooldown if need be
    }

    /*fissure ability */
    private IEnumerator fissure(Collider2D col1, Collider2D col2) {
        //camera shake
        StartCoroutine(CameraManager.Instance.cameraShake(3f, .07f));

        float elapsed = 0.0f;

        while (elapsed < 3) { 
            float moveX = Mathf.Lerp(0, 3f, Time.deltaTime);

            col1.transform.position += Vector3.left*moveX;
            col2.transform.position += Vector3.right*moveX;

            elapsed += Time.deltaTime;

            yield return null;
            
        }
        yield return new WaitForEndOfFrame();
    }



    /* HELPER FUNCTIONS */


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
