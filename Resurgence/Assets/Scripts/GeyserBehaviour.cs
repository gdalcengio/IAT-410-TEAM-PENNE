using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserBehaviour : MonoBehaviour
{
    // set height for geyser
    public float limit, currentHeight;
    private bool activeGeyser = false; // don't active geyser when it's already active
    private Vector2 reset;

    void Update() {
        currentHeight = transform.position.y;
    }

    public IEnumerator BuildGeyser(Vector3 direction,float speed)
    {
        float startime = Time.time;
        Vector2 startPos = transform.position; //Starting position.
        Vector2 endPos = transform.position + direction; //Ending position.
 
        while (startPos != endPos && ((Time.time - startime)*speed) < 1f && transform.position.y < limit) { 
            float move = Mathf.Lerp(0,1, (Time.time - startime)*speed);
 
            transform.position += direction*move;
 
            yield return null;
        }
        yield return new WaitForSeconds(5);

        reset = new Vector2(startPos.x, startPos.y); // reset geyser after time

        // reset geyser
        transform.position = reset;
        this.GetComponent<BuoyancyEffector2D>().density = 0f;
        activeGeyser = false;
    }

    // getters and setters
    public bool getActiveGeyser()
    {
        return activeGeyser;
    }

    public void setActiveGeyser(bool input)
    {
        activeGeyser = input;
    }

    public Vector2 getReset()
    {
        return reset;
    }

    public void setReset(Vector2 input)
    {
        reset = new Vector2(input.x, input.y);
    }
}
