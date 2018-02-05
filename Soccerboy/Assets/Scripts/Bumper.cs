using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour {

    public LayerMask ballLayerMask;
    Ball ball; Collider ballCollider;
    
    //Eventos
    public event Action OnBump; 

    void Start() {
        ball = FindObjectOfType<Ball>();
        ballCollider = ball.GetComponent<Collider>();
    }

    void Update() {

        //Revisar si la pelota colisionó con este bumber
        Collider[] colliders = Physics.OverlapSphere(transform.position + Vector3.up * 0.43f, 0.9635437f, ballLayerMask);
        foreach (Collider col in colliders) {
            if (col == ballCollider) {
                float newMagnitude = ball.velocity.magnitude * 1.5f;
                ball.velocity = Vector3.Reflect(ball.velocity.normalized, Vector3Util.NoY(ball.transform.position - transform.position)) * newMagnitude;
                StartCoroutine("BumperAnimation");
                if (OnBump != null) { OnBump(); }
            }
        }

    }

    IEnumerator BumperAnimation() {

        float animationSpeed = 3f;
        float animationScale = 1.2f;

        while (transform.localScale.x < animationScale) {
            transform.localScale = transform.localScale + Vector3.one * animationSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        while (transform.localScale.x > 1) {
            transform.localScale = transform.localScale - Vector3.one * animationSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.localScale = Vector3.one;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 0.43f, 0.9635437f);
    }
}
