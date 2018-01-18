using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public Vector3 velocity;
    public LayerMask floorLayerMask;
    public LayerMask wallLayerMask;

    SphereCollider sphereCollider;

    Vector3 previousPosition;

	void Awake () {
        sphereCollider = GetComponent<SphereCollider>();

        previousPosition = transform.position;

        hitInfos = new List<HitInfo>();
	}
	
	void Update () {

        //Recopilar datos del suelo
        RaycastHit hit;
        //bool thereIsFloor = Physics.SphereCast(transform.position, sphereCollider.radius * 0.999f, Vector3.down, out hit, 10f, floorLayerMask);
        bool thereIsFloor = Physics.Raycast(transform.position + Vector3.up * sphereCollider.radius, Vector3.down, out hit, 10f, floorLayerMask);

        //Si no hay suelo, caer
        if (thereIsFloor == false) {

            velocity = velocity + Vector3.down * Time.deltaTime * 16f;

            //Reiniciar la partida despues de 1 segundo
            StartCoroutine(FindObjectOfType<PlayManager>().RestartPlayAfter(1f));
        } 
        
        //Si hay suelo
        else {

            //Disminuir la velocidad
            Deacelerate(2f);

            //Pegarse a el
            transform.position = new Vector3(transform.position.x, hit.point.y + sphereCollider.radius, transform.position.z);

            //Darle velocidad de acuerdo a la pendiente
            Vector3 velAdd = Vector3Util.NoY(hit.normal);
            velocity = velocity + velAdd * Time.deltaTime * 16f;
        }

        //Moverse de acuerdo a la velocidad
        transform.position = transform.position + velocity * Time.deltaTime;

        //Manejar colisión
        ManageCollision();

        //Guardar esta posicion como la ultima para el siguiente frame
        previousPosition = transform.position;
    }

    void ManageCollision() {

        //Hacer el SphereCast desde la posición previa hasta la actual
        Vector3 delta = transform.position - previousPosition;
        RaycastHit[] hits = Physics.SphereCastAll(previousPosition, sphereCollider.radius, (delta).normalized, delta.magnitude, wallLayerMask);
        //Debug.Log(hits.Length);
        //if(hits.Length > 1) { Debug.Break(); }

        foreach (RaycastHit hit in hits) {

            //Checar que no sea un hit desde adentro de un collider
            if (hit.distance == 0f) { break; }

            //Reflejar la velocidad
            velocity = Vector3Util.NoY(Vector3.Reflect(velocity, hit.normal));

            //Guardar la info para el Gizmos y la normal
            hitInfos.Add(new HitInfo(hit.point, hit.normal));

            //Mover la pelota fuera del collider
            transform.position = hit.point + hit.normal * sphereCollider.radius;
        }

    }

    public void AddForce(Vector3 force) {
        velocity = velocity + force;
    }

    public void Deacelerate(float factor) {
        float newMagnitude = velocity.magnitude - factor * Time.deltaTime;
        newMagnitude = Mathf.Clamp(newMagnitude, 0f, float.MaxValue);
        velocity = velocity.normalized * newMagnitude;
    }

    private void OnDrawGizmos() {
        //Dibujar los HitInfo
        Gizmos.color = Color.red;
        foreach (HitInfo i in hitInfos) {
            Gizmos.DrawRay(i.point, i.normal);
        }
    }

    List<HitInfo> hitInfos;

    class HitInfo {
        public Vector3 point;
        public Vector3 normal;

        public HitInfo(Vector3 p, Vector3 n) {
            point = p;
            normal = n;
        }
    }
}
