using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchUtil {

    public static bool CheckTouched(Collider col) {

        Input.simulateMouseWithTouches = false;

        //Revisar si se tocó el collider (Android)
        /*if (Application.platform == RuntimePlatform.Android) {
            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began) {

                    //Transformar coordenadas de camara a posición global
                    Vector3 worldPoint = Camera.main.ScreenToWorldPoint(touch.position);

                    //Revisar si hizo click en el objeto
                    if (Physics2D.OverlapPoint(worldPoint) == col) {
                        return true;
                    }
                }
            }
        }*/

        //Revisar si se hizo click en el collider (Windows)
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer) {
            if (Input.GetMouseButtonDown(0)) {

                //Transformar coordenadas de camara a posición global
                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //Revisar si hizo click en el objeto
                RaycastHit hit; Physics.Raycast(worldPoint, Camera.main.transform.forward, out hit);
                if (hit.collider == col) {
                    return true;
                }
            }
        }

        return false;
    }

    public static Vector3 TouchOnPlane(Plane plane) {

        if(Application.platform == RuntimePlatform.Android) {
            return Vector3.zero;
        } else {
            Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward);
            float rayDistance = 0f;
            plane.Raycast(ray, out rayDistance);
            return Camera.main.ScreenToWorldPoint(Input.mousePosition) + Camera.main.transform.forward * rayDistance;
        }
    }

}
