using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour {
    private int angle;
    public int changeAngle;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            angle = angle == 0 ? changeAngle : 0;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, angle, 0), 10 * Time.deltaTime);
    }
}