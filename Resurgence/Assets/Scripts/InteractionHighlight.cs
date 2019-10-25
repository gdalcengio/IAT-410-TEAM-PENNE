using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class InteractionHighlight : MonoBehaviour
{
    public bool itzCan, tlaCan;
    Light2D light2D = null;

    public void Start() {
        light2D = GetComponent<Light2D>();
    }

    public void OnTriggerEnter2D(Collider2D col) {
        if ((itzCan && col.gameObject.tag == "ItzPowerCol") || (tlaCan && col.gameObject.tag == "TlaPowerCol")) {

            StartCoroutine("glow");
        }
    }

    public void OnTriggerExit2D(Collider2D col) {
        if ((itzCan && col.gameObject.tag == "ItzPowerCol") || (tlaCan && col.gameObject.tag == "TlaPowerCol")) {

            StartCoroutine("unglow");
        }
    }

    IEnumerator glow() {
        while(light2D.intensity < 7) {
            light2D.intensity += 1;
            yield return null;
        }
    }

    IEnumerator unglow() {
        while(light2D.intensity > 2) {
            light2D.intensity -= 1;
            yield return null;
        }
    }
}
