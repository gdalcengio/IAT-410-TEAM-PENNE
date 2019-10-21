using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveBehaviour : MonoBehaviour
{
    public List<GameObject> nodes;
    private int diveEntrance; 
    private int diveTarget = 0;
    private bool canChange = true;

    // get all possible locations
    public void defineNodes()
    {
        foreach (Transform child in transform) {
            if (child.name != "index") nodes.Add(child.gameObject);
        }
    }

    public int parseNode(string node)
    {
        node = node.Substring(node.Length - 1, 1);
        int.TryParse (node,out diveEntrance);

        return diveEntrance;
    }

    // cycle through dive locations
    public void changePath()
    {
        if (!canChange) {
            if (diveTarget < nodes.Count-1) {
                diveTarget++;
            } else {
                diveTarget = 0;
            }
        }
    }

    // declare destination
    public string getNode(int input) 
    {
        string destination = "node" + input;

        return destination;
    }

    // getters and setters
    public int getEntrance()
    {
        return diveEntrance;
    }

    public void setEntrance(int input)
    {
        diveEntrance = input;
    }

    public int getDestination()
    {
        return diveTarget;
    }

    public void setDestination(int input)
    {
        diveTarget = input;
    }

    public bool getCanChange() 
    {
        return canChange;
    }

    public void setCanChange(bool input) {
        canChange = input;
    }
}
