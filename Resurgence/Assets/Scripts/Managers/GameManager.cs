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

    private void Start() {
        nextScene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("next scene is: " + nextScene);
    }

    private void Update() {
        if (Input.GetKeyDown("r")) ResetScene();
        if (Input.GetKeyDown("t")) UIManager.Instance.fadeNextScene();
    }

    private void OnTriggerEnter2D(Collider2D col) {
        //in order to reset scene on death, add more tags to do it
        if (col.gameObject.tag == "Player") {
            ResetScene();
        }
    }

    public void LoadNextScene() {
        ClearUI();
        Debug.Log(nextScene);
        nextScene++;
        Debug.Log(nextScene);
        if (nextScene == 9) {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
        DontDestroyOnLoad(this.gameObject);
        
    }

    public void ResetScene() {
        ClearUI();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        DontDestroyOnLoad(this.gameObject);
    }

    public void ClearUI() {
        foreach (Transform child in UIManager.Instance.transform)
        {
            if (child.tag != "Fade") {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

   
}
