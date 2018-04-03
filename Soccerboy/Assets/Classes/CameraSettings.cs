using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings {

    /// <summary>
    /// El centro sobre el cual la cámara rotará (si es que se implementa esa función).
    /// </summary>
    public Vector3 pivotCenter;

    /// <summary>
    /// La posición en la cual aparecerá la cámara.
    /// </summary>
    public Vector3 position;

    /// <summary>
    /// La rotación en la cual aparecerá la cámara.
    /// </summary>
    public Vector3 rotation;

    /// <summary>
    /// El atributo size que se le asignará a la cámara.
    /// </summary>
    public float size;
}
