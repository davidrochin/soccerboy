using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    public Vector3 goalDetectionSize;
    public Vector3 goalDetectionOffset;

    //Acciones
    public event Action OnGoal;

    public LayerMask ballLayerMask;

    Ball ball;

    void Awake() {
        
    }

    void Start () {
        ball = FindObjectOfType<Ball>();
	}
	
	void Update () {

        //Revisar si la pelota entró en la portería
        Collider[] colliders = Physics.OverlapBox(transform.position + goalDetectionOffset, goalDetectionSize * 0.5f, Quaternion.identity, ballLayerMask);
        if (colliders.Length > 0 && colliders[0].gameObject == ball.gameObject) {
            //FindObjectOfType<GoalText>().Show();
            if (OnGoal != null) { OnGoal(); }
            ball.Deacelerate(12f);
        }

	}

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(transform.position + goalDetectionOffset, goalDetectionSize);
    }
}
