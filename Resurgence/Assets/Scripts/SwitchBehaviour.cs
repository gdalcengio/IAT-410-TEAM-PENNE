using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehaviour : MonoBehaviour
{
    private bool state = false;

    public bool getState()
    {
        return state;
    }

    public void toggleState()
    {
        // flip switch
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        if (state) {
            state = false;
        } else {
            state = true;
        }
    }
}
