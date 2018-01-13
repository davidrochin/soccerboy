using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kicker : MonoBehaviour {

    [Range(0.1f, 1f)]
    public float kickRadius = 0.5f;
    public float kickForce = 5f;

    public LayerMask ballLayerMask;

    Ball ball; Collider ballCollider;

    void Start() {
        ball = FindObjectOfType<Ball>();
        ballCollider = ball.GetComponent<Collider>();
    }

    void Update() {

        Collider[] hits;

        //Revisar si la pelota está en el radio de pateo
        hits = Physics.OverlapSphere(transform.position, kickRadius, ballLayerMask);

        //Si la pelota está en el radio, patearla
        if (hits.Length > 0) {
            FindObjectOfType<Ball>().velocity = Vector3Util.NoY(transform.forward) * kickForce;
        }
    }

    void OnDrawGizmos() {

        //Dibujar el radio de pateo
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, kickRadius);
    }
}
