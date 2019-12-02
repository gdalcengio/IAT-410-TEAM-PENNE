using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    public enum State { rockets, laser, enraged, dead }
    public RocketSpawner rocketScript;
    public Laser laserScript;
    public State state;
    private int health;
    Animator animator;

    void Start()
    {
        health = 3;
        state = State.rockets;

        transform.position += Vector3.right * 15;
        animator = GetComponent<Animator>();
    }

    public IEnumerator bossEnter() {
        float elapsed = 0f;

        while (elapsed > -7) {
            float moveX = Mathf.Lerp(0, 2f, Time.deltaTime);
            transform.position -= Vector3.right * moveX;

            elapsed -= Time.deltaTime;

            yield return null;
        }
        StartCoroutine(bossRocket());
        StartCoroutine(bossMoveBackward());
    }

    private IEnumerator bossMoveForward() {
        float elapsed = 0f;

        while (elapsed < 2)
        {
            float moveX = Mathf.Lerp(0, 1f, Time.deltaTime);
            transform.position += Vector3.left * moveX;

            elapsed += Time.deltaTime;

            yield return null;
        }

        StartCoroutine(bossMoveBackward());
    }

    private IEnumerator bossMoveBackward()
    {
        float elapsed = 0f;

        while (elapsed < 2)
        {
            float moveX = Mathf.Lerp(0, 1f, Time.deltaTime);
            transform.position += Vector3.right * moveX;

            elapsed += Time.deltaTime;

            yield return null;
        }

        StartCoroutine(bossMoveForward());
    }

    private IEnumerator bossRocket() {
        if (state != State.rockets) yield return null;

        while (state == State.rockets || state == State.laser) {
            StartCoroutine(rocketScript.spawner());

            yield return new WaitForSeconds(7);
        }
    }

    private IEnumerator deathSequence() {
        die();
        yield return null;
    }

    private void die() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Itztli" || col.gameObject.tag == "Tlaloc" || col.gameObject.tag == "Laser") {
            GameManager.Instance.ResetScene();
            return;
        }

         

        if (col.gameObject.tag == "Object" || col.gameObject.tag == "Rocket") {
            animator.SetTrigger("Hurt");
            col.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            if (col.gameObject.tag == "Object") col.GetComponent<Animator>().SetBool("Boom", true);
            //starts at three
            // Destroy(col.gameObject);
            if (col.gameObject.tag == "Rocket") col.GetComponent<Animator>().SetBool("Explosion", true);
            health--;
            
            switch(health) {
                case 2:
                    animator.SetTrigger("Hurt");
                    StartCoroutine(laserScript.ready(1f));
                    Debug.Log("two health");
                    break;
                case 1:
                    //enraged
                    animator.SetTrigger("Hurt");
                    Debug.Log("one health");
                    break;
                case 0:
                    //dying
                    animator.SetInteger("Health", 0);
                    Debug.Log("dead");
                    StartCoroutine(deathSequence());

                    break;
                default:
                    break;
            }
        }
    }

    public void startEntering() {
        StartCoroutine(bossEnter());
    }
}
