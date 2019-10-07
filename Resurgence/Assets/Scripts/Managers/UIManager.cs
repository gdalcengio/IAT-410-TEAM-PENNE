using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    protected UIManager() {

    }
    //for tutorial messages or general information
    public IEnumerator Toast(int seconds, Text toast) {
        Color alpha = toast.color;

        while (alpha.a < 1) {
            alpha.a += 0.05f;
            toast.color = alpha;
            Debug.Log("adding alpha" + alpha);
            yield return null;
        }

        yield return new WaitForSeconds(seconds);

        while (alpha.a > 0) {
            alpha.a -= 0.05f;
            toast.color = alpha;
            Debug.Log("subtracting alpha" + alpha);
            yield return null;
        }
    }
}
