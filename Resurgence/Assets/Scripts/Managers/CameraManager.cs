using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{

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
