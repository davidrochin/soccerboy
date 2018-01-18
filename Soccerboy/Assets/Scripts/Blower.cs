using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blower : MonoBehaviour {

    public float range = 6f;
    public float blowForce = 10f;
    public LayerMask ballLayerMask;

    Ball ball; Collider ballCollider;
    ParticleSystem particleSystem;

	void Start () {
        ball = FindObjectOfType<Ball>();
        ballCollider = ball.GetComponent<Collider>();
        particleSystem = GetComponent<ParticleSystem>();
	}
	
	void Update () {

        //Revisar si la pelota está en el rango
        bool ballInRange = Physics.CheckBox(transform.position + transform.forward * range * 0.5f, new Vector3(1f, 1f, range) * 0.5f, transform.rotation, ballLayerMask);

        //Si la pelota está en el rango, aplicarle una fuerza para simular el aire
        if (ballInRange) {
            Debug.Log("Aplicando fuerza");
            ball.AddForce(transform.forward * blowForce * Time.deltaTime);
        }

        float secondsForUnit = 1f / particleSystem.startSpeed;
        particleSystem.startLifetime = secondsForUnit * range;

	}

    private void OnDrawGizmos() {

    }
}
