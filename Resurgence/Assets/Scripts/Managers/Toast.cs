using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toast : MonoBehaviour
{
    private void OnTriggerEnter2D (Collider2D col){
        if (col.gameObject.tag == "Player") {
            Debug.LogError("starting coroutine");
            IEnumerator toast = UIManager.Instance.Toast(3, GetComponent<Text>());
            StartCoroutine(toast);
            Debug.LogError("started coroutine");
        }
    }
}
