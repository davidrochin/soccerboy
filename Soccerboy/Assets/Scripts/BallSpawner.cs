using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour {

    Ball ball;

    public PrefabReferenceGroup prefabReferences;

	void Awake () {
        ball = Instantiate(prefabReferences.ballPrefab, transform.position + Vector3.up * Ball.ballRadius, Quaternion.identity).GetComponent<Ball>();
	}
	
	void Update () {
		
	}

    private void OnDrawGizmos() {

        //Dibujar el lugar donde va a aparecer la pelota
        Gizmos.color = new Color(1f, 1f, 1f, 0.5f);
        Gizmos.DrawSphere(transform.position + Vector3.up * Ball.ballRadius, Ball.ballRadius);

    }
}
