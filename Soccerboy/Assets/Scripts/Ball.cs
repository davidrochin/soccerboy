using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Ball : MonoBehaviour {

    public Vector3 velocity;
    public LayerMask floorLayerMask;
    public LayerMask wallLayerMask;

    [HideInInspector]
    public Vector3 bottom {
        get { return transform.position + Vector3.down * sphereCollider.radius; }
        set { transform.position = value + Vector3.up * sphereCollider.radius; }
    }
    [HideInInspector] public bool launched;

    SphereCollider sphereCollider;

    Vector3 previousPosition;

    //Eventos
    public event Action OnOutOfField;

    void Awake() {
        sphereCollider = GetComponent<SphereCollider>();

        previousPosition = transform.position;

        hitInfos = new List<HitInfo>();
    }

    void Update() {

        //Limitar la velocidad
        velocity = Vector3.ClampMagnitude(velocity, 20f);

        //Revisar si la pelota se cayó del campo
        if (transform.position.y < -1f) {
            if (OnOutOfField != null) { OnOutOfField(); }
        }

        //Disminuir la velocidad
        Deacelerate(2f);

        //Caer
        Fall(16f);

        //Moverse de acuerdo a la velocidad
        transform.position = transform.position + velocity * Time.deltaTime;

        //Recopilar datos del suelo
        RaycastHit sphereHit; Vector3 castStart = transform.position + Vector3.up * sphereCollider.radius * 2f;
        bool thereIsFloor = Physics.SphereCast(castStart, sphereCollider.radius, Vector3.down, out sphereHit, sphereCollider.radius * 2f, floorLayerMask);

        //Si hay suelo
        if (thereIsFloor) {

            //Quitar la velocidad en Y
            velocity = new Vector3(velocity.x, 0f, velocity.z);

            //Darle velocidad de acuerdo a la pendiente
            Vector3 velAdd = Vector3.Cross(sphereHit.normal, Quaternion.Euler(0f, -90f, 0f) * sphereHit.normal.NoY());
            velocity = velocity + velAdd * Time.deltaTime * 16f;

            transform.position = castStart + Vector3.down * sphereHit.distance;

        }

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

    public void Fall(float factor) {
        velocity = velocity + Vector3.down * Time.deltaTime * factor;
    }

    Vector3 pendNorm;
    Vector3 pendPos;

    private void OnDrawGizmos() {
        //Dibujar los HitInfo
        Gizmos.color = Color.red;
        foreach (HitInfo i in hitInfos) {
            Gizmos.DrawRay(i.point, i.normal);
        }

        Gizmos.DrawRay(pendPos, pendNorm);
    }

    public static float ballRadius = 0.4457389f;

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
