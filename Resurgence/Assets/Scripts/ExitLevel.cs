using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevel : MonoBehaviour
{
    private bool iIn = false, tIn = false, cIn = false, raising = false;
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
            if (raising && cIn) {
                StartCoroutine(CatalystIn());
            } else {
                if (needCatalyst && cIn) {
                    UIManager.Instance.fadeNextScene();
                } else if (!needCatalyst) {
                    UIManager.Instance.fadeNextScene();
                }
            }
        }
    }

    // public void Start() {
    //     StartCoroutine(raise());
    // }

    public IEnumerator raise() {
        raising = true;
        float elapsed = 0f;
        
        StartCoroutine(CameraManager.Instance.cameraShake(3f, .09f));
        while (transform.position.y < 3.5 ) {
            float moveY = Mathf.Lerp(0, 2f, Time.deltaTime);
            transform.position += (Vector3.up * moveY)/10;

            elapsed += Time.deltaTime;

            yield return null;
        }

        
    }

    public IEnumerator CatalystIn() {
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("catalyst base active");
        yield return new WaitForSeconds(2);
        UIManager.Instance.fadeNextScene();
    }
}
