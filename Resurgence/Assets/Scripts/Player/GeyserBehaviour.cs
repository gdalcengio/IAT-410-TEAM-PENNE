using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserBehaviour : MonoBehaviour
{
    // set height for geyser
    public float limit, currentHeight;
    private bool activeGeyser = false; // don't active geyser when it's already active
    private Vector2 reset;
    public string state = "NotActive";
    float height;
    Vector3 centerPos;
    Vector3 finalPos;
    Vector3 startPos;
    Vector3 scale;
    public Animator animator;
    public Transform child;
    public Vector3 saveParentPos;
    public float colliderBounds;

    void Start()
    {
        startPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        child = this.transform.GetChild(0);
        saveParentPos = this.transform.parent.position;
        colliderBounds = this.GetComponent<BoxCollider2D>().size.y;
    }

    void Update() {
        currentHeight = transform.position.y;

        height = child.gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    public void Stretch(GameObject _sprite, Vector3 startPos, Vector3 finalPos)
    {
        finalPos = new Vector3(_sprite.transform.position.x, currentHeight, _sprite.transform.position.z);
        centerPos = (startPos + finalPos) / 2f;
        _sprite.transform.position = centerPos;
        scale = new Vector3(1,1,1);
        scale.y = Vector3.Distance(startPos, finalPos) / limit;
        // scale.y = Vector3.Distance(startPos, finalPos);
        _sprite.transform.localScale = scale;
    }

    public IEnumerator BuildGeyser(Vector3 direction,float speed)
    {
        float startime = Time.time;
        Vector2 startPos = transform.position; //Starting position.
        Vector2 endPos = transform.position + direction; //Ending position.
 
        //camera shake
        // StartCoroutine(CameraManager.Instance.cameraShake(3f, (Time.time - startime)*speed));

        while (startPos != endPos && ((Time.time - startime)*speed) < 1f && transform.position.y < limit) { 
            state = "BuildUp";
            animator.SetFloat("Duration", -0.1f);

            float move = Mathf.Lerp(0,1, (Time.time - startime)*speed);
 
            transform.position += direction*move;
            Stretch(child.gameObject, this.transform.parent.position, new Vector3(startPos.x, this.transform.position.y, 1f));
            animator.SetFloat("Point", transform.position.y);
 
            yield return null;
        }
        state = "Peaked";
        animator.SetFloat("Duration", -0.1f);
        yield return new WaitForSeconds(5);
        animator.SetFloat("Duration", Time.time - startime);

        state = "Decline";
        Vector3 savePos = new Vector3(child.position.x, child.position.y, child.position.z);
        reset = new Vector2(startPos.x, startPos.y); // reset geyser after time

        // reset geyser
        transform.position = reset;
        child.position = savePos;
        this.GetComponent<BuoyancyEffector2D>().density = 0f;
        activeGeyser = false;
        animator.SetFloat("Point", -0.1f);
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
