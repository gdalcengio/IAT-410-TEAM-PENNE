using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserBehaviour : MonoBehaviour
{
    // set height for geyser
    public float limit, currentHeight;

    void Update() {
        currentHeight = transform.position.y;
    }
}
