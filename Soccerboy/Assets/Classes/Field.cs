using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "field", menuName = "Fields/Field")]
public class Field : ScriptableObject {

    public FieldTemplate template;
    public FieldElement[] fieldElements; //(posiblemente una List nos sea más 
                                         //conveniente del lado del cliente).
}

[System.Serializable]
[CreateAssetMenu(fileName = "element", menuName = "Fields/FieldElement")]
public class FieldElement : ScriptableObject {

    /// <summary>
    /// El tipo de elemento.
    /// </summary>
    public Type type;

    /// <summary>
    /// El prefab del elemento.
    /// </summary>
    public GameObject prefab;

    /// <summary>
    /// La posición del elemento (relativa al campo).
    /// </summary>
    public Vector3 position;
    /// <summary>
    /// La rotación del elemento (relativa al campo).
    /// </summary>
    public Vector3 rotation;

    /// <summary>
    /// El skin del elemento.
    /// </summary>
    public Skin skin;
    /// <summary>
    /// Reservado. No usar.
    /// </summary>
    public int config;

    /// <summary>
    /// Describe el tipo de elemento.
    /// </summary>
    public enum Type {
        FieldBarrier,
        SmallBarrier,
        Bumper,
        Cone,
        Blower,
        CurvedRamp,
        Accelerator,
        Pyramid,
        
        Goal,
        BallSpawner
    }

}

/// <summary>
/// Define de qué manera debería ser cargado el campo.
/// </summary>
public enum FieldLoadingMode {
	/// <summary>
	/// El jugador está probando si su propio campo es superable.
	/// </summary>
	UserTesting,
	/// <summary>
	/// El jugador está jugando en el campo de otra persona.
	/// </summary>
	Attack,
    /// <summary>
	/// El jugador está abriendo su campo para editarlo.
	/// </summary>
    Edit,
	/// <summary>
	/// Reservado. No usar.
	/// </summary>
	// TODO: Eliminar antes del stage Beta
	InternalDebug
}