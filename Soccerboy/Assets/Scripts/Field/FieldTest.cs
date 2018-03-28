using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldTest : MonoBehaviour {

	// Campo de prueba
	public Field testField;

    public void Awake()
    {
        // Campo de prueba
        if (testField == null)
            Debug.LogError("Capo, tenés que colocar un Field en el campo \"Test Field\" del editor.");
    }

    void OnGUI() {
		if (GUILayout.Button ("Cargar el campo de prueba")) {
			FieldManager.Load (testField, FieldLoadingMode.UserTesting);
		} else if (GUILayout.Button ("Crear un campo desde el código")) {
            FieldManager.Load (TestField(), FieldLoadingMode.UserTesting);
		}
	}

    Field TestField() {
        var field = new Field() {
            template = FieldFactory.LoadTemplate("Rounded3by3"),
            fieldElements = new FieldElement[] {
                FieldFactory.LoadElement(FieldElement.Type.SmallBarrier),
                FieldFactory.LoadGoal(),
                FieldFactory.LoadBallSpawner()
            }
        };

        return field;
    }

}