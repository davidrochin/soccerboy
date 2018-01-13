using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySentry : MonoBehaviour {

    [Range(1f, 6f)]
    public float followRadius = 3f;

    [Range(1f, 5f)]
    public float moveSpeed = 2f;

    public LayerMask ballLayerMask;
    public GameObject radiusIndicator;

    Ball ball; Collider ballCollider;

    Vector3 startingPos;
    Quaternion startingRot;

	void Start () {
        ball = FindObjectOfType<Ball>();
        ballCollider = ball.GetComponent<Collider>();

        //Guardar la posicion y rotación inicial
        startingPos = transform.position;
        startingRot = transform.rotation;

        //Instanciar el indicador de radio
        radiusIndicator = Instantiate(radiusIndicator);
        radiusIndicator.transform.position = startingPos + Vector3.up * 0.05f;
        radiusIndicator.transform.localScale = Vector3.one * followRadius * 2f;
	}
	
	void Update () {

        //Revisar si la pelota está en el radio de seguimiento
        Collider[] hits = Physics.OverlapSphere(startingPos, followRadius, ballLayerMask);

        //Si la pelota está en el radio, dirigirse hacia ella
        if(hits.Length > 0) {
            transform.position = Vector3.MoveTowards(transform.position, Vector3Util.NoY(hits[0].transform.position), moveSpeed * Time.deltaTime);
            Quaternion lookRotation = Quaternion.LookRotation(hits[0].transform.position - transform.position);
            Quaternion fixedLookRotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, fixedLookRotation, 400f * Time.deltaTime);
        } 
        
        //Si no está en el radio, regresar al centro de este mismo
        else {
            transform.position = Vector3.MoveTowards(transform.position, startingPos, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, startingRot, 200f * Time.deltaTime);
        }
    }

    void OnDrawGizmos() {

        //Dibujar el radio de seguimiento
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, followRadius);

    }
}
