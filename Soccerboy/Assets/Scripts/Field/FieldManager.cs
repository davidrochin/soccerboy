using System;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public static void Load(Field field, FieldLoadingMode mode) {
        if (field == null)
            throw new ArgumentNullException("field");

        // Cargar plantilla
        var template = field.template;
        if (template == null)
            Debug.LogError("\"field.template\" was null.");

        // Cargar elementos
        // NOTA: Acá lo más apropiado sería usar una lista de tuplas ( List<Tuple<FieldElement, GameObject>> ),
        // pero como un Dictionary hace mas o menos lo mismo, lo utilizo para no sobre-complicar las cosas.
        Dictionary<FieldElement, GameObject> elements = new Dictionary<FieldElement, GameObject>();

        foreach (var element in field.fieldElements)
        {
            var elemPrefab = element.prefab;
            if (elemPrefab == null)
                Debug.LogErrorFormat("\"field.fieldElements[{0}].prefab\" was null.", Array.IndexOf(field.fieldElements, element));
            else
                elements.Add(element, Instantiate(elemPrefab));
        }
        // TODO: Implementar cambio de skins

        //////////////////////
        // Armar este menjunge
        //////////////////////

        // Guardar arco
        var fieldParent = new GameObject("field");

        // Instanciar suelo
        var floor = new GameObject(fieldParent.name + "_floor");
        floor.transform.parent = fieldParent.transform;
        floor.transform.localPosition = Vector3.zero;

        for (var i = 0; i < template.blocks.Length; i++) {
            var floorBlock = template.blocks[i];
            var floorBlockInstance = Instantiate(floorBlock.prefab);

            floorBlockInstance.transform.parent = floor.transform;
            floorBlockInstance.transform.position = floorBlock.position;
            floorBlockInstance.transform.eulerAngles = floorBlock.rotation;
            floorBlockInstance.name = String.Format("{0}_{1}", floor.name, i);
        }

        // Instanciar elementos
        var elemParent = new GameObject(fieldParent.name + "_elements");
        elemParent.transform.parent = fieldParent.transform;

        foreach (var element in elements) {
            element.Value.transform.parent = fieldParent.transform;
            element.Value.transform.localPosition = element.Key.position;
            element.Value.transform.localEulerAngles = element.Key.rotation;

            element.Value.transform.parent = elemParent.transform;
            element.Value.name = String.Format("{0}_{1}", elemParent.name, element.Key.type);
            // TODO: Agregar cambio de configs (¿?)
        }

        // Acomodar cámara
        var camera = Camera.main;
        var cameraConfig = template.cameraSettings;

        camera.transform.position = cameraConfig.position;
        camera.transform.eulerAngles = cameraConfig.rotation;
        camera.orthographicSize = cameraConfig.size;
        // TODO: Ver que ocurre con pivotCenter

        // TODO: Agregar diferentes acciones dependiendo de "fieldMode"
        switch (mode) {
            case FieldLoadingMode.Attack:
                break;
            case FieldLoadingMode.UserTesting:
                break;
            case FieldLoadingMode.Edit:
                break;
            default:
                Debug.LogErrorFormat("\"fieldMode\" is in a unknown state: \"{0}\"", mode);
                break;
        }

        // TODO: Agregar "algo que hacer" en caso de error
        // Ideas: Una pantalla de error,
        //        Volver a pedir el campo al servidor,
        //        Pedir otro campo al servidor
    }

    public Field Save() {
        //Este procedimineto tiene que analizar la escena, armar un Field a partir de los objetos en ella, y devolverlo.
        throw new NotImplementedException();
    }
}