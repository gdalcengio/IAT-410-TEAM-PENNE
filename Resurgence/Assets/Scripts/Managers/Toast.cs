using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toast : MonoBehaviour
{
    // public UIManager manager;
    public bool itzCan, tlaCan;

    void Start() {
        transform.parent = UIManager.Instance.transform;
    }

    private void OnTriggerEnter2D (Collider2D col){
        if (itzCan && col.gameObject.tag == "Itztli") {
            // IEnumerator toast = manager.Appear(GetComponent<Image>());
            
            IEnumerator toast = UIManager.Instance.Appear(GetComponent<Image>());
            StartCoroutine(toast);
        } else if (tlaCan && col.gameObject.tag == "Tlaloc") {
            // IEnumerator toast = manager.Appear(GetComponent<Image>());
            
            IEnumerator toast = UIManager.Instance.Appear(GetComponent<Image>());
            StartCoroutine(toast);
        }
    }

    private void OnTriggerExit2D (Collider2D col){
        if (itzCan && col.gameObject.tag == "Itztli") {
            // IEnumerator toast = manager.Appear(GetComponent<Image>());
            
            IEnumerator toast = UIManager.Instance.Disappear(GetComponent<Image>());
            StartCoroutine(toast);
        } else if (tlaCan && col.gameObject.tag == "Tlaloc") {
            // IEnumerator toast = manager.Appear(GetComponent<Image>());
            
            IEnumerator toast = UIManager.Instance.Disappear(GetComponent<Image>());
            StartCoroutine(toast);
        }
    }
}
