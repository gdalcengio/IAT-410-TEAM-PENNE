using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }


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

    private int nextScene = 1;

    // private void Start() {
    //     nextScene = SceneManager.GetActiveScene().buildIndex + 1;
    //     Debug.Log("next scene is: " + nextScene);
    // }

    private void Update() {
        if (Input.GetKeyDown("r")) ResetScene();
    }

    private void OnTriggerEnter2D(Collider2D col) {
        //in order to reset scene on death, add more tags to do it
        if (col.gameObject.tag == "Player") {
            ResetScene();
        }
    }

    public void LoadNextScene() {
            SceneManager.LoadScene(nextScene);
            DontDestroyOnLoad(this.gameObject);
            nextScene++;
    }

    public void ResetScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        DontDestroyOnLoad(this.gameObject);
    }

   
}
