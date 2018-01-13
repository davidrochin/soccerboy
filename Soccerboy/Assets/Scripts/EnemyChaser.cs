using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaser : MonoBehaviour {

    [Range(1f, 5f)]
    public float moveSpeed = 2f;

    public LayerMask floorLayerMask;

    [HideInInspector]
    public float playTime = 0f;

    Ball ball; Collider ballCollider;

    Vector3 startingPos;
    Quaternion startingRot;

    PlayManager playManager;

    void Start() {
        ball = FindObjectOfType<Ball>();
        ballCollider = ball.GetComponent<Collider>();

        //Guardar la posicion y rotación inicial
        startingPos = transform.position;
        startingRot = transform.rotation;

        playManager = FindObjectOfType<PlayManager>();
    }

    void Update() {

        //Incrementar el contador de tiempo de juego
        playTime += Time.deltaTime;

        //Si la pelota está en el radio, dirigirse hacia ella
        if (playManager.playInProgress) {

            //Revisar si hay suelo
            Vector3 nextPos = transform.position + (Vector3Util.NoY(ball.transform.position) - transform.position).normalized * moveSpeed * Time.deltaTime;

            if (Physics.Raycast(nextPos + Vector3.up, Vector3.down, 4f, floorLayerMask)) {
                transform.position = Vector3.MoveTowards(transform.position, Vector3Util.NoY(ball.transform.position), moveSpeed * Time.deltaTime);
                Quaternion lookRotation = Quaternion.LookRotation(ball.transform.position - transform.position);
                Quaternion fixedLookRotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, fixedLookRotation, 400f * Time.deltaTime);
            }
        }

    }

}
