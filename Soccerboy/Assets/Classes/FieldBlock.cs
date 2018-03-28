using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FieldBlock {

    /// <summary>
    /// El objeto que incluye el modelo, material, textura, etc. del bloque.
    /// </summary>
    public GameObject prefab;

    /// <summary>
    /// La posición del bloque.
    /// </summary>
    public Vector3 position;

    /// <summary>
    /// La rotación del bloque.
    /// </summary>
    public Vector3 rotation;

    /// <summary>
    /// El skin de el FieldBlock
    /// </summary>
    public Skin skin;
}