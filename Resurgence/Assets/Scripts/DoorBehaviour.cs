using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    private bool open = false;

    GameObject container;
    GameObject binarySwitch;
    Collider2D col;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();

        container = transform.parent.gameObject;

        foreach (Transform child in container.transform) {
            if (child.tag == "BinarySwitch") {
                binarySwitch = child.gameObject;
                return;
            }
        }
    }

    void Update()
    {
        toggleDoor();
    }

    public bool getDoor() 
    {
        return open;
    }

    public void toggleDoor() 
    {
        if (binarySwitch.GetComponent<SwitchBehaviour>().getState()) {
            open = true;
            col.isTrigger = true;
        } else {
            open = false;
            col.isTrigger = false;
        }
    }
}
