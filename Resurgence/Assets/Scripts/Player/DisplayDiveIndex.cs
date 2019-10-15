using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDiveIndex : MonoBehaviour
{
    private Text textIndex;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        textIndex = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textIndex.text = index.ToString();
    }
}
