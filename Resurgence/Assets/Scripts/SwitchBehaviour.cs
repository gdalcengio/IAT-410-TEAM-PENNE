using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehaviour : MonoBehaviour
{
    private bool state = false;

    public bool rockSpawn = false;
    public GameObject rockPrefab;
    private bool rockPresent;

    void Update()
    {
        if (GameObject.FindWithTag("Rock") != null) rockPresent = true;
        else rockPresent = false;
    }

    public bool getState()
    {
        return state;
    }

    public void toggleState()
    {
        FindObjectOfType<AudioManager>().Play("Switch");
        // flip switch
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        if (rockSpawn && !rockPresent) {
            Instantiate(rockPrefab, new Vector2(-7.7f, 28.5f), Quaternion.identity);
        }

        if (state) {
            state = false;
        } else {
            state = true;
        }
    }
}
