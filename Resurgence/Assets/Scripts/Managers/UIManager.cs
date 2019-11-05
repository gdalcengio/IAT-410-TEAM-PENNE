using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public Animator animator;
    public static UIManager Instance { get { return _instance; } }


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
    //for tutorial messages or general information
    public IEnumerator Appear(Image toast) {
        Color alpha = toast.color;

        while (alpha.a < 1) {
            alpha.a += 0.05f;
            toast.color = alpha;
            Debug.Log("adding alpha" + alpha);
            yield return null;
        }
    }

    public IEnumerator Disappear(Image toast) {
        Color alpha = toast.color;

        while (alpha.a > 0) {
            alpha.a -= 0.05f;
            toast.color = alpha;
            Debug.Log("subtracting alpha" + alpha);
            yield return null;
        }
    }

    public void fadeNextScene() {
        animator.SetTrigger("NextSceneFadeOut");
    }

    public void onFadeComplete() {
        GameManager.Instance.LoadNextScene();
    }
}
