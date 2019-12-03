using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBehaviour : MonoBehaviour
{
    private LineRenderer line;
    private Vector2 firstEndPoint;

    private bool connected = true;

    Color c1 = new Color(255, 255, 255, 0);
    Color c2 = new Color(255, 255, 255, 0);

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        firstEndPoint = line.GetPosition(1);
    }

    void Update()
    {
        line.SetColors(c1, c2);
    }

    public void setPosition(int index, Vector2 position) {
        line.SetPosition(index, position);
    }

    public bool getConnected()
    {
        return connected;
    }

    public void toggleConnected()
    {
        connected = !connected;
    }

    public void resetCurrent()
    {
        line.SetPosition(1, firstEndPoint);
    }

    public Vector2 returnOrigin()
    {
        return line.GetPosition(0);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col != null && (col.gameObject.tag == "Itztli" || col.gameObject.tag == "Tlaloc" || col.gameObject.tag == "Godot")) {
            FindObjectOfType<AudioManager>().Play("Zapper");
            if (col.gameObject.tag == "Itztli" || col.gameObject.tag == "Tlaloc") {
                GameManager.Instance.ResetScene();
            }

            if (col.gameObject.tag == "Godot") {
                col.gameObject.GetComponent<EnemyBehaviour>().health = 0;
            }
        }
    }
}
