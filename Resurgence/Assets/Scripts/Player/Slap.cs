using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slap : MonoBehaviour
{
    GameObject slapTarget;
    private float thrust = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (this.transform.parent.gameObject.name == "Itztli") slapTarget = GameObject.Find("Tlaloc");
        else if (this.transform.parent.gameObject.name == "Tlaloc") slapTarget = GameObject.Find("Itztli");
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.LogError(slapTarget.name);
        if (Input.GetKeyDown(KeyCode.U)) {
            // slap animation
        }

    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col != null && col.gameObject.name == slapTarget.name) {
            Debug.LogError(col.gameObject.name);
            float gay = Input.GetAxis("iSlap");
            float fuck = Input.GetAxis("iSlap");
            if (Input.GetKeyDown(KeyCode.U) || gay == 1 || fuck == 1) {
                StartCoroutine(CameraManager.Instance.cameraShake(.15f, .1f));
                // slap to right
                if (this.transform.position.x < slapTarget.transform.position.x) slapTarget.GetComponent<Rigidbody2D>().AddForce(transform.right * thrust, ForceMode2D.Impulse);
                // slap to left
                else slapTarget.GetComponent<Rigidbody2D>().AddForce(-transform.right * thrust, ForceMode2D.Impulse);
            }
        }
    }
}
