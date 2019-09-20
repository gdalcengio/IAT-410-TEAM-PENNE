using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAbilities : MonoBehaviour
{
    GameObject buoyancyObject, buoyancyParent;
    GameObject diveObject, diveParent;
    int diveIndex;
    BuoyancyEffector2D buoy;
    BuoyancyEffector2D targetGeyser = null;
    Vector2 startPosition;
    Vector2 targetPosition;
    Vector2 reset;
    float startTime;

    private IEnumerator geyserCoroutine;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Spout") {
            buoyancyParent = col.gameObject;
        }

        if (col.gameObject.tag == "EntryPoint") {
            diveObject = col.gameObject;
        }
    }

    void OnTriggerExit2D (Collider2D col) {
        // reset colliders
        col = null;

        if (col.gameObject == buoyancyParent) {
            //deactivate colliders
            buoyancyParent = buoyancyObject = null;
            col = null;
            targetGeyser = null;
        }
    }

    void OnTriggerStay2D(Collider2D col) 
    {
        if (Input.GetKey(KeyCode.G)) { // initiate geyser
            if (col.gameObject == buoyancyParent) {
                // get geyser object
                buoyancyObject = buoyancyParent.transform.GetChild(0).gameObject; 
                buoy = buoyancyObject.GetComponent<BuoyancyEffector2D>();
                targetGeyser = buoy;

                reset = startPosition = new Vector2(buoy.transform.position.x, buoy.transform.position.y); // reset geyser after time
                targetPosition = new Vector2(0.0f, buoy.transform.position.y + buoy.GetComponent<GeyserBehaviour>().limit); // need limit
                activateGeyser(buoy);
            }
        }

        if (Input.GetKey(KeyCode.R)) {
            if (col.gameObject == diveObject) {
                // get index of parent
                diveParent = diveObject.transform.parent.gameObject;
                string entrance = diveParent.name;
                entrance = entrance.Substring(entrance.Length - 1, 1);
                int.TryParse (entrance,out diveIndex);

                GameObject diveContainer = diveParent.transform.parent.gameObject;
                int divePoints = diveContainer.transform.childCount;
                // Debug.LogError(col.name);
                diveMove(col, divePoints, diveIndex);

                // deactivate collidrs
                diveContainer = diveParent = diveObject = null;
                col = null;
                entrance = null;
            }
        }
    }

    private void diveMove(Collider2D col, int divePoints, int diveIndex)
    {
        if (divePoints > 1) {
            Vector2 currentLocation = new Vector2(col.transform.position.x, col.transform.position.y);
            Transform playerTransform = GameObject.Find("Base").transform;
            GameObject exitPoint;
            exitPoint = getNode(diveIndex, divePoints).transform.GetChild(0).gameObject; 
            Debug.LogError(exitPoint.name + ", " + exitPoint.transform.position);
            // playerTransform.position = teleportGoal.position;
            playerTransform.position = exitPoint.transform.position;
        }
    }

    private GameObject getNode(int diveIndex, int divePoints) {
        GameObject exitPoint;
        if (divePoints == 2) {
            string node;
            if (diveIndex == 0) node = "node1";
            else node = "node0";
            exitPoint = GameObject.Find(node);
        } else {
            return null;
        }
        return exitPoint;
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
