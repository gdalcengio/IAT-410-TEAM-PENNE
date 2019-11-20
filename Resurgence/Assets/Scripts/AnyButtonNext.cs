using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyButtonNext : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown) {
            UIManager.Instance.fadeNextScene();
        }
    }
}
