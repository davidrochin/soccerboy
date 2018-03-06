using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Ball : MonoBehaviour {

    public bool frozen = true;

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
    public event Action OnOutOfField, OnSlope;

    #region Monobehaviours

    void Awake() {
        sphereCollider = GetComponent<SphereCollider>();

        previousPosition = transform.position;

        hitInfos = new List<HitInfo>();
    }

    void FixedUpdate() {
        if (!frozen) {
            ManageFloorCollision();
            ManageWallCollision();
        }
    }

    Vector3 pendNorm;
    void OnDrawGizmos() {
        //Dibujar los HitInfo
        Gizmos.color = Color.red;
        foreach (HitInfo i in hitInfos) {
            Gizmos.DrawRay(i.point, i.normal);
        }

        Gizmos.DrawRay(bottom, pendNorm);

        //Dibujar la siguiente posicion de la bola
        //Gizmos.DrawSphere(transform.position + velocity * Time.deltaTime * 4f, sphereCollider.radius);

    }

    #endregion

    #region Procedimientos Privados

    void ManageFloorCollision() {

        //Limitar la velocidad
        velocity = Vector3.ClampMagnitude(velocity, 20f);

        //Revisar si la pelota se cayó del campo
        if (transform.position.y < -1f) {
            if (OnOutOfField != null) { OnOutOfField(); }
        }

        //Disminuir la velocidad
        float newMagnitude = velocity.magnitude - 0.04f;
        newMagnitude = Mathf.Clamp(newMagnitude, 0f, float.MaxValue);
        velocity = velocity.normalized * newMagnitude;

        //Moverse de acuerdo a la velocidad
        transform.position = transform.position + velocity * 0.02f;

        //Recopilar datos del suelo
        RaycastHit sphereHit; Vector3 castStart = transform.position + Vector3.up * sphereCollider.radius * 2f;
        bool thereIsFloor = Physics.SphereCast(castStart, sphereCollider.radius, Vector3.down, out sphereHit, sphereCollider.radius * 2f, floorLayerMask);

        //Si hay suelo
        if (thereIsFloor) {

            //Darle velocidad de acuerdo a la pendiente (si hay pendiente)
            if (!Mathf.Approximately(Vector3.Angle(Vector3.up, sphereHit.normal), 0f)) {

                //hitInfos.Add(new HitInfo(sphereHit.point, sphereHit.normal));
                Vector3 velAdd = Vector3.Cross(sphereHit.normal, Quaternion.Euler(0f, -90f, 0f) * sphereHit.normal.NoY()).normalized;

                //Darle mas velocidad si la pendiente es mas inclinada
                velAdd = velAdd * Mathf.InverseLerp(90f, 180f, Vector3.Angle(Vector3.up, velAdd));

                Debug.Log(Vector3.Angle(Vector3.up, velAdd) + ", " + Mathf.InverseLerp(90f, 180f, Vector3.Angle(Vector3.up, velAdd)));

                pendNorm = velAdd;
                velocity = velocity + velAdd * 1f;
            } else {
                velocity = new Vector3(velocity.x, 0f, velocity.z);
            }

            //Poner la pelota en el suelo
            Vector3 tmp = transform.position;
            transform.position = castStart + Vector3.down * sphereHit.distance;

            //Darle la velocidad que se movió debido a posicionamiento en el suelo (para rampear)
            velocity += (transform.position - tmp) * 16f;

        } else {

            //Caer
            velocity = velocity + Vector3.down * 0.5f;
        }

    }

    void ManageWallCollision() {

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

        //Guardar esta posicion como la ultima para el siguiente frame
        previousPosition = transform.position;
    }

    #endregion

    #region Procedimientos Públicos

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

    #endregion

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
