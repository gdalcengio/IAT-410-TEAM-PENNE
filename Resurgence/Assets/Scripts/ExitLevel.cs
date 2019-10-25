using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevel : MonoBehaviour
{
    private bool iIn = false, tIn = false, cIn = false;
    public bool needCatalyst = false;
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Itztli") {
            iIn = true;
        } else if (col.gameObject.tag == "Tlaloc") {
            tIn = true;
        } else if (col.gameObject.tag == "Catalyst") {
            cIn = true;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Itztli") {
            iIn = false;
        } else if (col.gameObject.tag == "Tlaloc") {
            tIn = false;
        } else if (col.gameObject.tag == "Catalyst") {
            cIn = true;
        }
    }
    void Update() {
        if (iIn && tIn) {
            if (needCatalyst && cIn) {
                GameManager.Instance.LoadNextScene();
            } else if (!needCatalyst) {
                GameManager.Instance.LoadNextScene();
            }
        }
    }
}
