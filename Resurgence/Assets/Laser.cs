using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject laserBeam, laserCharge;
    // private BossBattle bossScript;

    void Start()
    {
        // bossScript = GetComponentInParent<BossBattle>       //this is where you're at gabe. setting the state for the bossbattle script
        // StartCoroutine(ready(1f));
    }

    private IEnumerator charge (float duration) {
        laserCharge.SetActive(true);
        Debug.Log("charging");

        //camera shake
        StartCoroutine(CameraManager.Instance.cameraShake(duration, .02f));

        float elapsed = 0.0f;
        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(fire(1f));
    }

    private IEnumerator fire(float duration)
    {
        laserCharge.SetActive(false);
        laserBeam.SetActive(true);

        Debug.Log("fire");

        //camera shake
        StartCoroutine(CameraManager.Instance.cameraShake(duration, .09f));

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(ready(4f));
    }


    public IEnumerator ready(float duration)
    {
        laserBeam.SetActive(false);

        Debug.Log("ready");

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(charge(3f));
    }

        
}
