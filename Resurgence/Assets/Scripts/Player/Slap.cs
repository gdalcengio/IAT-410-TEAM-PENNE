using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slap : MonoBehaviour
{
    public Animator animator;
    GameObject slapTarget;
    private float thrust = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        if (this.transform.parent.gameObject.name == "Itztli") slapTarget = GameObject.Find("Tlaloc");
        else if (this.transform.parent.gameObject.name == "Tlaloc") slapTarget = GameObject.Find("Itztli");
    }

    // Update is called once per frame
    void Update()
    {
        float itSlap = Input.GetAxis("iSlap");
        float tlSlap = Input.GetAxis("tSlap");
        // if (Input.GetButtonDown("iSlap") || Input.GetButtonDown("tSlap")|| Input.GetKeyDown(KeyCode.U)) {
        if (itSlap == 1 && slapTarget.name == "Tlaloc") {
            // slap animation
            animator.SetTrigger("Slap");
        } else if (tlSlap == 1 && slapTarget.name == "Itztli") {
            // slap animation
            animator.SetTrigger("Slap");
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col != null && col.gameObject.name == slapTarget.name)
        {
            float itSlap = Input.GetAxis("iSlap");
            float tlSlap = Input.GetAxis("tSlap");
            if ((itSlap == 1 && slapTarget.name == "Tlaloc")|| ((tlSlap == 1  || Input.GetKeyDown(KeyCode.I)) && slapTarget.name == "Itztli")) {
                FindObjectOfType<AudioManager>().PlayCoroutine();
                StartCoroutine(CameraManager.Instance.cameraShake(.15f, .1f));
                // slap to right
                if (this.transform.position.x < slapTarget.transform.position.x) slapTarget.GetComponent<Rigidbody2D>().AddForce(transform.right * thrust, ForceMode2D.Impulse);
                // slap to left
                else slapTarget.GetComponent<Rigidbody2D>().AddForce(-transform.right * thrust, ForceMode2D.Impulse);
            }
        }
    }
}
