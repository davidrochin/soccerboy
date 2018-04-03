using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "field", menuName = "Fields/Field")]
public class Field : ScriptableObject {

    // TODO: Este entero necesita cambiarse. Que ahora sea un objeto de tipo FieldTemplate
    public int template;
    public FieldElement[] fieldElements; //(posiblemente una List nos sea más 
                                         //conveniente del lado del cliente).
    public Vector3 goalPosition;
    public Vector3 goalRotation;

    public Vector3 ballSpawn;

    public Vector3 cameraPosition;
}

[System.Serializable]
public class FieldElement {

    public Type type;

    public Vector3 position;
    public Vector3 rotation;

    public int skin;
    public int config;

    public enum Type {
        FieldBarrier,
        SmallBarrier,
        Bumper,
        Cone,
        Blower,
        CurvedRamp,
        Accelerator,
        Pyramid
    }

}

/// <summary>
/// De qué manera debería ser cargado el campo.
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
	/// Reservado para propósitos de debugging. NO USAR.
	/// </summary>
	// TODO: Eliminar antes del stage Beta
	InternalDebug
}