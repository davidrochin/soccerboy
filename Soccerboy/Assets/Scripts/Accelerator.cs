using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerator : MonoBehaviour {

    public Vector3 areaSize;
    public float accelerationForce = 10f;

    Ball ball; Collider ballCollider;

	void Start () {
        ball = FindObjectOfType<Ball>();
        ballCollider = ball.GetComponent<Collider>();
	}
	
	void Update () {

        //Revisar si la pelota está en el area del acelerador
        Collider[] colliders = Physics.OverlapBox(transform.position, areaSize * 0.5f, transform.rotation);
        foreach (Collider col in colliders) {
            if(col == ballCollider) {

                //Acelerar la pelota hacia adelante
                ball.AddForce(transform.forward.NoY() * accelerationForce * Time.deltaTime);
            }
        }
	}

    private void OnDrawGizmosSelected() {
        //Dibujar el area del acelerador
        GizmosUtil.DrawWireCube(transform.position, transform.rotation, areaSize);
    }
}
