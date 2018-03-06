using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Aquí se guardarán los prefabs necesarios para armar un campo.
/// </summary>
public class FieldBuildingBlocks : MonoBehaviour {

	/// <summary>
	/// Transform padre de las plantillas para el suelo del campo.
	/// </summary>
	public Transform templateBank;
	/// <summary>
	/// Transform padre de las plantillas para los arcos del campo.
	/// </summary>
	public Transform goalBank;
	/// <summary>
	/// Transform padre de las plantillas para los elementos del campo.
	/// </summary>
	public Transform elementBank;
	/// <summary>
	/// Transform padre de las plantillas para la pelota del jugador.
	/// </summary>
	public Transform ballSpawnerPrefab;

}