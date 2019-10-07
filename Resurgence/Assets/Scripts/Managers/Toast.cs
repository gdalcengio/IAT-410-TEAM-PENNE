using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toast : MonoBehaviour
{
    public int seconds = 3;
    public UIManager manager;
    private void OnTriggerEnter2D (Collider2D col){
        if (col.gameObject.tag == "Itztli" || col.gameObject.tag == "Tlaloc") {
            // Debug.LogError("starting coroutine");
            // IEnumerator toast = UIManager.Instance.Toast(seconds, GetComponent<Text>());
            IEnumerator toast = manager.Toast(seconds, GetComponent<Text>());
            StartCoroutine(toast);
            // Debug.LogError("started coroutine");
        }
    }
}
