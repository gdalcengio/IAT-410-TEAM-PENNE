using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatalystCamBehavior : MonoBehaviour
{
    [HideInInspector]

    Camera cam;
    private Vector2 screenBounds;
    private float playerWidth, playerHeight;
    Vector3 viewPos;
    private PickUpCatalyst iScript = null;
    private WaterPickUpCatalyst tScript = null;

    //on start, gets the rigidbody of the player
    void Start() {
        cam = Camera.main;
        screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
        playerWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x;
        iScript = GetComponent<PickUpCatalyst>();
        tScript = GetComponent<WaterPickUpCatalyst>();
    }

    void FixedUpdate() 
    {
        if (!iScript.GetHasCatatlyst() || !tScript.GetHasCatatlyst()) {
            if (transform.position.x <= -screenBounds.x+playerWidth) {
                transform.position = new Vector2(-screenBounds.x+playerWidth, transform.position.y);
            } else if (transform.position.x >= screenBounds.x-playerWidth) {
                transform.position = new Vector2(screenBounds.x-playerWidth, transform.position.y);
            }
        }
    }

}
