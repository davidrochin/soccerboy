using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour {
    private int angle;
    public int AddAngle;
    public int Turns;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeAngle();
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, angle, 0), 10 * Time.deltaTime);
    }

    public int ChangeAngle()
    {
        return angle = (AddAngle * Turns) == angle ? 0 : (AddAngle * (Turns - 1)) == angle && (AddAngle * Turns) == 360 ? 0 : angle + AddAngle;
    }
}