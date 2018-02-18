using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldTest : MonoBehaviour {

	// Campo de prueba
	public Field testField = new Field() {
		template = 1,

		goalPosition = new Vector3(4.11f, 0, 9.344f),
		goalRotation = Vector3.zero,

		ballSpawn = new Vector3(-4.01f, 0, -2.49f),

		fieldElements = new FieldElement[] {
			new FieldElement() {
				type = FieldElement.Type.Accelerator,
				position = new Vector3(4, 0)
			},
			new FieldElement() {
				type = FieldElement.Type.FieldBarrier,
				position = new Vector3(-4, 0),
				rotation = new Vector3(0, 90)
			},
			new FieldElement() {
				type = FieldElement.Type.SmallBarrier,
				position = new Vector3(4, 0, -4),
				rotation = new Vector3(0, -45)
			}
		}
	};

	void OnGUI() {
		if (GUILayout.Button ("Cargar campo de prueba")) {
			GameManager.StartField (testField, FieldLoadingMode.UserTesting);
		} else if (GUILayout.Button ("Prueba de manejo de errores (FieldLoadingMode desconocido)")) {
			GameManager.StartField (testField, FieldLoadingMode.InternalDebug);
		}
	}

}