using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreMessage : MonoBehaviour
{
    private BossBattle script;

    void Start() {
        gameObject.SetActive(true);
        script = GameObject.Find("Boss").GetComponent<BossBattle>();
    }

    void Update()
    {
        if (Input.GetButtonDown("I_Jump") || Input.GetButtonDown("T_Jump")) {
            gameObject.SetActive(false);
            script.startEntering();
        }

    }
}
