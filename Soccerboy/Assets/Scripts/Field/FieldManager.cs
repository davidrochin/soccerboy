using System;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
	// Se llamará al iniciar la escena
	void Awake ()
	{
		var field = GameManager.instance.currentField;
		var fieldMode = GameManager.instance.currentFieldLoadingMode;
		if (field == null)
			throw new Exception ("\"GameManager.instance.field\" was null, this usually means that the level was loaded manually. Try loading it using \"GameManager.StartField()\".");

		// Cargar prefab bank
		var prefBank = GameObject.Find ("prefabBank").GetComponent<FieldBuildingBlocks> ();
		if (prefBank == null)
			Debug.LogError ("\"prefBank\" not found in the scene.");

		// Cargar plantilla
		var template = prefBank.templateBank.Find (field.template.ToString ());
		if (template == null)
			Debug.LogErrorFormat ("\"field.template\" does not match any child of {0}", prefBank.templateBank.name);

		// Cargar arco
		var goalSkin = prefBank.goalBank.Find ("0"); // TODO: Agregar distintos skins o tipos de arcos
		if (goalSkin == null)
			Debug.LogErrorFormat ("\"field.goalSkin\" does not match any child of {0}", prefBank.goalBank.name);

		// Cargar pelota
		var ballSkin = prefBank.ballSpawnerPrefab;
		// TODO: Implementar cambio de skin para la pelota

		// Cargar elementos
		Dictionary<FieldElement, GameObject> elements = new Dictionary<FieldElement, GameObject> ();

		foreach (var element in field.fieldElements) {
			var elemPrefab = prefBank.elementBank.Find (element.type.ToString ());
			if (elemPrefab == null)
				Debug.LogErrorFormat ("\"field.fieldElements[{0}].type\" does not match any child of {1}", Array.IndexOf (field.fieldElements, element), prefBank.elementBank.name);

			elements.Add (element, Instantiate (elemPrefab).gameObject);
		}
		// TODO: Implementar cambio de skins

		//////////////////////
		// Armar este menjunge
		//////////////////////

		// Guardar arco
		var instField = this.gameObject;

		// Instanciar suelo
		var instTemplate = Instantiate (template);
		instTemplate.parent = instField.transform;
		instTemplate.localPosition = Vector3.zero;
		instTemplate.gameObject.name = "field_floor";

		// Instanciar arco
		var instGoal = Instantiate (goalSkin);
		instGoal.parent = instField.transform;
		instGoal.localPosition = field.goalPosition;
		instGoal.eulerAngles = field.goalRotation;
		instGoal.gameObject.name = "field_goal";

		// Instanciar pelota
		var instBall = Instantiate (ballSkin);
		instBall.parent = instField.transform;
		instBall.localPosition = field.ballSpawn;
		instBall.gameObject.SetActive (true);
		instBall.gameObject.name = "field_ballSpawn";

		// Instanciar elementos
		var elemParent = new GameObject(instField.name + "_elements");
		elemParent.transform.parent = instField.transform;

		foreach (var element in elements) {
			element.Value.transform.parent = instField.transform;
			element.Value.transform.localPosition = element.Key.position;
			element.Value.transform.eulerAngles = element.Key.rotation;

			element.Value.transform.parent = elemParent.transform;
			element.Value.name = String.Format("{0}_{1}", elemParent.name, element.Key.type);
			// TODO: Agregar cambio de configs (¿?)
		}

		// TODO: Agregar diferentes acciones dependiendo de "fieldMode"
		switch (fieldMode) {
		case FieldLoadingMode.Attack:
			break;
		case FieldLoadingMode.UserTesting:
			break;
		default:
			Debug.LogErrorFormat ("\"fieldMode\" is in a unknown state: \"{0}\"", fieldMode.ToString ());
			break;
		}

		prefBank.gameObject.SetActive (false); // Desactivar lo que no se va a utilizar

		// TODO: Agregar "algo que hacer" en caso de error
		// Ideas: Una pantalla de error,
		//        Volver a pedir el campo al servidor
	}

    public void Load(Field field, FieldLoadingMode mode) {
        //Este procedimiento tiene que cargar el Field en la escena actual.
    }

    public Field Save() {
        //Este procedimineto tiene que analizar la escena, armar un Field a partir de los objetos en ella, y devolverlo.
        return new Field();
    }
}