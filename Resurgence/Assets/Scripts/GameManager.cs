using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private int nextScene;
    // (Optional) Prevent non-singleton constructor use.
    protected GameManager() { 

    }

    private void Start() {
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;
    }

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
        nextScene++;
    }

    public void ResetScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
