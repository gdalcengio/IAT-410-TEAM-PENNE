using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAbilities : MonoBehaviour
{
    GameObject buoyancyObject, parent;
    BuoyancyEffector2D buoy;
    BuoyancyEffector2D targetGeyser = null;
    Vector2 startPosition;
    Vector2 targetPosition;
    Vector2 reset;
    float startTime;

    private IEnumerator geyserCoroutine;


    void OnTriggerStay2D(Collider2D col) 
    {
        if (Input.GetKey(KeyCode.G)) {
            // get geyser object
            parent = GameObject.FindWithTag("Spout");
            buoyancyObject = parent.transform.GetChild(0).gameObject; 
            buoy = buoyancyObject.GetComponent<BuoyancyEffector2D>();
            targetGeyser = buoy;

            reset = startPosition = new Vector2(buoy.transform.position.x, buoy.transform.position.y); // reset geyser after time
            targetPosition = new Vector2(0.0f, buoy.transform.position.y +  buoy.GetComponent<GeyserBehaviour>().limit); // need limit
            activateGeyser(buoy);
        }
    }

    private void activateGeyser(BuoyancyEffector2D buoy)
    {
        // height limit
        float limit = buoy.GetComponent<GeyserBehaviour>().limit;

        // increase density/buoyancy
        buoy.density = 3f;

        geyserCoroutine = BuildGeyser(buoy, targetPosition, 0.005f);
        StartCoroutine (geyserCoroutine);
    }

    IEnumerator BuildGeyser(BuoyancyEffector2D buoyancy, Vector3 direction,float speed)
    {
        float startime = Time.time;
        Vector2 startPos = buoy.transform.position; //Starting position.
        Vector2 endPos = buoy.transform.position + direction; //Ending position.
 
        while (startPos != endPos && ((Time.time - startime)*speed) < 1f && buoy.transform.position.y < buoy.GetComponent<GeyserBehaviour>().limit) { 
            float move = Mathf.Lerp(0,1, (Time.time - startime)*speed);
 
            buoy.transform.position += direction*move;
 
            yield return null;
        }
        yield return new WaitForSeconds(5);

        // reset geyser
        buoy.transform.position = reset;
        buoy.density = 0f;
    }
}
