using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlider : MonoBehaviour {

    [Range(1f, 6f)]
    public float slideExtent = 3f;

    [Range(1f, 5f)]
    public float moveSpeed = 2f;

    public LayerMask ballLayerMask;
    public GameObject extentIndicator;

    Ball ball; Collider ballCollider;

    Vector3 startingPos;
    Quaternion startingRot;

	void Start () {
        ball = FindObjectOfType<Ball>();
        ballCollider = ball.GetComponent<Collider>();

        //Guardar la posicion y rotación inicial
        startingPos = transform.position;
        startingRot = transform.rotation;

        //Instanciar el indicador de extensión
        /*extentIndicator = Instantiate(extentIndicator);
        extentIndicator.transform.position = startingPos + Vector3.up * 0.05f;
        extentIndicator.transform.localScale = Vector3.one * slideExtent * 2f;*/
	}
	
	void Update () {
        //Debug.Log(Mathf.Sin(Time.time));
        float fixedSin = MathfUtil.Map(-1f, 1f, 0f, 1f, Mathf.Sin(Time.time * moveSpeed));
        transform.position = Vector3.Lerp(startingPos + transform.right * slideExtent, startingPos + transform.right * -1f * slideExtent, fixedSin);
    }

    void OnDrawGizmos() {

        //Dibujar la extensión
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * slideExtent);
        Gizmos.DrawLine(transform.position, transform.position + transform.right * -1f * slideExtent);

    }
}
