using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAbilities : MonoBehaviour
{
    GameObject buoyancyObject, buoyancyParent;
    GameObject diveObject, diveParent, diveContainer;
    int diveIndex;
    BuoyancyEffector2D buoy;
    BuoyancyEffector2D targetGeyser = null;
    Vector2 startPosition;
    Vector2 targetPosition;
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

                startPosition = buoy.GetComponent<GeyserBehaviour>().getReset(); // reset geyser after time
                targetPosition = new Vector2(0.0f, buoy.transform.position.y + buoy.GetComponent<GeyserBehaviour>().limit); // need limit
                if (!buoy.GetComponent<GeyserBehaviour>().getActiveGeyser()) {
                    activateGeyser(buoy);
                    buoy.GetComponent<GeyserBehaviour>().setActiveGeyser(true);
                }
            }
        }

        if (Input.GetKey(KeyCode.R)) {
            if (col.gameObject == diveObject) {
                // get index of parent
                diveParent = diveObject.transform.parent.gameObject;
                diveContainer = diveParent.transform.parent.gameObject;

                diveContainer.GetComponent<DiveBehaviour>().nodes.Clear(); // always start with a fresh set of nodes
                diveIndex = diveContainer.GetComponent<DiveBehaviour>().parseNode(diveParent.name);

                diveContainer.GetComponent<DiveBehaviour>().defineNodes(); // get all valid locations
                diveContainer.GetComponent<DiveBehaviour>().setEntrance(diveIndex);
                diveMove(col, diveContainer.GetComponent<DiveBehaviour>().nodes.Count, diveContainer.GetComponent<DiveBehaviour>().getEntrance());
                // deactivate colliders
                diveContainer = diveParent = diveObject = null;
                col = null;
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
            playerTransform.position = exitPoint.transform.position;
        }
    }

    private GameObject getNode(int diveIndex, int divePoints) {
        GameObject exitPoint = null;
        if (divePoints == 2) {
            if (diveIndex == 0) diveContainer.GetComponent<DiveBehaviour>().setDestination(1);
            else  diveContainer.GetComponent<DiveBehaviour>().setDestination(0);
            string node = diveContainer.GetComponent<DiveBehaviour>().getNode(diveContainer.GetComponent<DiveBehaviour>().getDestination());
            exitPoint = diveContainer.transform.GetChild(diveContainer.GetComponent<DiveBehaviour>().getDestination()).gameObject;
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

        geyserCoroutine = buoy.GetComponent<GeyserBehaviour>().BuildGeyser(targetPosition, 0.005f);
        StartCoroutine(geyserCoroutine);
    }
}
