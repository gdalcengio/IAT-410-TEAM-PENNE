using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawner : MonoBehaviour
{

    public GameObject rocket;

    void Start() {
        StartCoroutine(spawner());
    }

    private IEnumerator launch() {
        float interval = 0f;

        while (interval < 1f) {
            interval += Time.deltaTime;
            yield return null;
        }

        Instantiate(rocket, transform);
    }

    private IEnumerator spawner() {
        int rockets = 0;

        while (rockets < 3) {
            StartCoroutine(launch());

            rockets++;
            yield return new WaitForSeconds(1);
        }
    }

}
