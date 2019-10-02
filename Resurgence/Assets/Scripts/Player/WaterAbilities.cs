using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAbilities : MonoBehaviour
{
    GameObject buoyancyObject, buoyancyParent;
    GameObject diveObject, diveParent, diveContainer;
    GameObject diveSwitch;
    int diveIndex;
    BuoyancyEffector2D buoy;
    BuoyancyEffector2D targetGeyser = null;
    Vector2 startPosition;
    Vector2 targetPosition;
    float startTime;

    private IEnumerator geyserCoroutine;

    void OnTriggerEnter2D(Collider2D col) {
        if (col != null) {
            if (col.gameObject.tag == "Spout") {
                buoyancyParent = col.gameObject;
            }

            if (col.gameObject.tag == "EntryPoint") {
                // access needed GameObjects
                diveObject = col.gameObject;
                diveParent = diveObject.transform.parent.gameObject;
                diveContainer = diveParent.transform.parent.gameObject;

                foreach (Transform child in diveContainer.transform) {
                    if (child.name.Substring(0, child.name.Length - 1) == "index") {
                        diveSwitch = child.gameObject;
                        return;
                    }
                }
            }

            if (col.gameObject.tag == "switch") {
                diveSwitch = col.gameObject;

                diveContainer = diveSwitch.transform.parent.gameObject;
                foreach (Transform child in diveContainer.transform) {
                    if (child.name.Substring(child.name.Length - 1, 4) == "node") {
                        diveParent = child.gameObject;
                        return;
                    }
                }
                diveObject = diveParent.transform.GetChild(0).gameObject;
            }
        }
    }

    void OnTriggerExit2D (Collider2D col) {
        // reset colliders
        // col = null;

        if (col.gameObject == buoyancyParent) {
            //deactivate colliders
            buoyancyParent = buoyancyObject = null;
            targetGeyser = null;
        }

        if (col.gameObject == diveSwitch) {
            diveSwitch = null;
        }
    }

    void OnTriggerStay2D(Collider2D col) 
    {
        if (Input.GetButtonDown("Geyser")) { // initiate geyser
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

        if (Input.GetButtonDown("Dive")) {
            if (col.gameObject == diveObject) {
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

        if (Input.GetButtonDown("DiveSwitch")) {
            if (col.gameObject == diveSwitch) {
                diveContainer.GetComponent<DiveBehaviour>().nodes.Clear(); // always start with a fresh set of nodes
                diveContainer.GetComponent<DiveBehaviour>().defineNodes(); // get all valid locations
                if (diveContainer.GetComponent<DiveBehaviour>().getCanChange()) {
                    diveContainer.GetComponent<DiveBehaviour>().setCanChange(false);
                    diveContainer.GetComponent<DiveBehaviour>().changePath();
                } 
                Debug.LogError("Target = " + diveContainer.GetComponent<DiveBehaviour>().getDestination());
            }
        } else if (Input.GetButtonUp("DiveSwitch")) {
            if (!diveContainer.GetComponent<DiveBehaviour>().getCanChange()) diveContainer.GetComponent<DiveBehaviour>().setCanChange(true);
        }
    }

    private void diveMove(Collider2D col, int divePoints, int diveIndex)
    {
        if (divePoints > 1) {
            // get current and target locations
            Vector2 currentLocation = new Vector2(col.transform.position.x, col.transform.position.y);
            Transform playerTransform = GameObject.Find("Base").transform;
            GameObject exitPoint;
            exitPoint = getNode(diveIndex, divePoints).transform.GetChild(0).gameObject; // define new location
            playerTransform.position = exitPoint.transform.position; // teleport
        }
    }

    // define target location
    private GameObject getNode(int diveIndex, int divePoints) {
        GameObject exitPoint = null;
        if (divePoints == 2) {
            if (diveIndex == 0) diveContainer.GetComponent<DiveBehaviour>().setDestination(1);
            else  diveContainer.GetComponent<DiveBehaviour>().setDestination(0);
            string node = diveContainer.GetComponent<DiveBehaviour>().getNode(diveContainer.GetComponent<DiveBehaviour>().getDestination());
            // don't teleport to the point that you entered
            if (diveIndex != diveContainer.GetComponent<DiveBehaviour>().getDestination()) exitPoint = diveContainer.transform.GetChild(diveContainer.GetComponent<DiveBehaviour>().getDestination()).gameObject;
        } else {
            diveContainer.GetComponent<DiveBehaviour>().setDestination(diveContainer.GetComponent<DiveBehaviour>().getDestination());
            string node = diveContainer.GetComponent<DiveBehaviour>().getNode(diveContainer.GetComponent<DiveBehaviour>().getDestination());
            // don't teleport to the point that you entered
            if (diveIndex != diveContainer.GetComponent<DiveBehaviour>().getDestination()) exitPoint = diveContainer.transform.GetChild(diveContainer.GetComponent<DiveBehaviour>().getDestination()).gameObject;
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
