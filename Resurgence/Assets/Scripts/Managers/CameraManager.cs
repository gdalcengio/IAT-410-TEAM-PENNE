using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager _instance;

    public static CameraManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public IEnumerator cameraShake(float duration, float magnitude) {
        Vector2 startPos = transform.localPosition;

        float elapsed = 0.0f;
        //start sound loop
        
        while (elapsed < duration) {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            
            transform.localPosition = startPos + new Vector2(x, y);

            elapsed+= Time.deltaTime;

            yield return null;
        }

        //end sound loop
        transform.localPosition = startPos;
    }

}
