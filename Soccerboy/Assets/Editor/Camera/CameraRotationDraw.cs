using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RotateCamera))]
class ConnectLineHandleExampleScript : Editor
{
    void OnSceneGUI()
    {
        RotateCamera rotateCamera = target as RotateCamera;
        Handles.color = Color.blue;
        Handles.DrawWireArc(rotateCamera.transform.position, rotateCamera.transform.up, Camera.main.transform.localPosition, rotateCamera.AddAngle * rotateCamera.Turns, Vector3.Distance(rotateCamera.transform.position, Camera.main.transform.position));
    }
}