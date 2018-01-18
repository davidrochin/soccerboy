using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour {

    [Range(0f, 1f)]
    public float forceMultiplier = 2f;
    public float maxForce = 10f;
    public GameObject launchArrow;
    public bool debugTouch = false;

    Vector3 startingTouchPos;
    Vector3 currentTouchPos;
    Vector3 touchFinalDelta;

    new Collider collider;
    Ball ball;

    Plane launcherPlane;


	void Awake () {
        collider = GetComponent<Collider>();
        ball = GetComponent<Ball>();

        //Inicializar el plano para los Raycast
        launcherPlane = new Plane(Vector3.up, Vector3.zero);

        //Crear la flecha de lanzamiento
        launchArrow = Instantiate(launchArrow);
        launchArrow.SetActive(false);
	}
	
	void Update () {

        Input.simulateMouseWithTouches = false;

        //Revisar si se está tocando la pantalla
        if (Input.touchCount > 0) {
            
            //Si el toque va empezando, guardar como posicion inicial
            if(Input.GetTouch(0).phase == TouchPhase.Began) {
                startingTouchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                Debug.Log(startingTouchPos);
            }
        }

        //Revisar si se hizo clic en la pantalla
        if (Input.GetMouseButtonDown(0)) {
            startingTouchPos = TouchUtil.TouchOnPlane(launcherPlane);
        }

        if (Input.GetMouseButton(0)) {
            currentTouchPos = TouchUtil.TouchOnPlane(launcherPlane);

            //Mostrar la flecha de lanzamiento
            launchArrow.SetActive(true);
            launchArrow.transform.position = transform.position + Vector3.up * 0.05f;
            launchArrow.transform.localScale = new Vector3(1f, 1f, Mathf.Clamp((currentTouchPos - startingTouchPos).magnitude, 0f, maxForce));
            launchArrow.transform.LookAt(transform.position + currentTouchPos - startingTouchPos);
            //launchArrow.transform.localRotation = Quaternion.Euler(new Vector3(0f, launchArrow.transform.localRotation.y, launchArrow.transform.localRotation.z));
        } else {
            //Ocultar la flecha de lanzamiento
            launchArrow.SetActive(false);
        }

        //Revisar si se levantó el clic de la pantalla
        if (Input.GetMouseButtonUp(0)) {
            touchFinalDelta = TouchUtil.TouchOnPlane(launcherPlane) - startingTouchPos;
            Launch(touchFinalDelta.normalized, Mathf.Clamp(touchFinalDelta.magnitude, 0f, maxForce));
            FindObjectOfType<PlayManager>().playInProgress = true;
            this.enabled = false;
        }
	}

    public void Launch(Vector3 direction, float force) {
        //Debug.Log(direction + ", " + force + ", " + forceMultiplier);
        ball.velocity = direction * force * forceMultiplier;
        
    }

    private void OnDrawGizmos() {
        if (debugTouch) {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(startingTouchPos, currentTouchPos);
        }
    }
}
