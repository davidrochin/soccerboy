using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "template", menuName = "Fields/Template")]
public class FieldTemplate : ScriptableObject {

    /// <summary>
    /// La lista de bloques por los cuales está conformado esta plantilla de campo.
    /// </summary>
    public FieldBlock[] blocks;

    /// <summary>
    /// Los ajustes que se le deberán hacer a la cámara al cargar un campo con esta plantilla.
    /// </summary>
    public CameraSettings cameraSettings;

}
