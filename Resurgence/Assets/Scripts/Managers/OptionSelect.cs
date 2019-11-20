using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionSelect : MonoBehaviour
{
    public int[] options; 
    public int max, min;
    private int cursor = 0;
    public Transform icon;

    public GameObject preface;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") == -1 || Input.GetAxis("optionsV") == -1) {
            if (cursor < max) {
                icon.localPosition = new Vector3(icon.localPosition.x, options[cursor + 1], 0);
                cursor++;
            }
        }
        if (Input.GetAxis("Vertical") == 1 || Input.GetAxis("optionsV") == 1) {
            if (cursor > min) {
                icon.localPosition = new Vector3(icon.localPosition.x, options[cursor - 1], 0);
                cursor--;
            }
        }

        if (Input.GetButtonDown("I_Jump") || Input.GetButtonDown("T_Jump") || Input.GetButtonDown("pause")) {
            if (cursor == 0) {
                UIManager.Instance.fadeNextScene();
            } else if (cursor == 1) {
                if (icon.gameObject.activeSelf) {
                    preface.SetActive(true);
                    icon.gameObject.SetActive(false);
                } else {
                    UIManager.Instance.fadeNextScene();
                }
            }
        }
    }
}
