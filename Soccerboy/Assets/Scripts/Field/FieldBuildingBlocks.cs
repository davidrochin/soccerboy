using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Aquí se generarán los elementos necesarios para armar un campo.
/// </summary>
public static class FieldFactory {

    /// <summary>
    /// Carga un elemento, le asigna un skin y lo devuelve.
    /// </summary>
    /// <param name="type">El tipo de elemento a cargar.</param>
    /// <param name="skin">El skin del elemento.</param>
    public static FieldElement LoadElement(FieldElement.Type type, Skin skin = 0) {
        var element = Resources.Load<FieldElement>(
            String.Format("Field/Elements/{0}.asset", type)
            );
        element.skin = skin;

        return element;
    }

    /// <summary>
    /// Carga el spawner pelota, le asigna un skin y la devuelve.
    /// </summary>
    /// <param name="skin">El skin de la pelota.</param>
    public static FieldElement LoadBallSpawner(Skin skin = 0) {
        var element = Resources.Load<FieldElement>(
            "Field/Ball"
            );
        element.skin = skin;

        return element;
    }

    /// <summary>
    /// Carga el spawner pelota, le asigna un skin y la devuelve.
    /// </summary>
    /// <param name="skin">El skin del arco.</param>
    public static FieldElement LoadGoal(Skin skin = 0) {
        var element = Resources.Load<FieldElement>(
            "Field/Goal"
            );
        element.skin = skin;

        return element;
    }

    /// <summary>
    /// Carga una plantilla de campo y la devuelve.
    /// </summary>
    /// <param name="templateName">El nombre de la plantilla.</param>
    public static FieldTemplate LoadTemplate(string templateName) {
        var element = Resources.Load<FieldTemplate>(
            String.Format("Field/Fields/{0}", templateName)
            );
        
        return element;
    }

}

/*
 * Estructura necesaria, suponiendo que
 * la carpeta raíz es Assets:
 * 
 * Resources/
 *     Field/
 *         Fields/
 *             Square2by2.asset
 *             Rounded3by3.asset
 *             TShape3by3.asset
 *             etc...
 *         Elements/
 *             FieldBarrier.asset
 *             SmallBarrier.asset
 *             Bumper.asset
 *             Cone.asset
 *             Blower.asset
 *             CurvedRamp.asset
 *             Accelerator.asset
 *             Pyramid.asset
 *         Ball.asset
 *         Goal.asset
 * Prefabs/
 *     Field/
 *         Blocks/
 *             Quarter.prefab
 *             Half.prefab
 *             Full.prefab
 *             Rounded.prefab
 *         Elements/
 *             FieldBarrier.prefab
 *             SmallBarrier.prefab
 *             Bumper.prefab
 *             Cone.prefab
 *             Blower.prefab
 *             CurvedRamp.prefab
 *             Accelerator.prefab
 *             Pyramid.prefab
 *         Goal/
 *         Ball/
 * Materials/
 *     Field/
 *         Block/
 *             01.mat
 *             etc...
 *         Elements/
 *             FieldBarrier/
 *                 01.mat
 *                 etc...
 *             SmallBarrier/
 *                 01.mat
 *                 etc...
 *             Bumper/
 *                 01.mat
 *                 etc...
 *             Cone/
 *                 01.mat
 *                 etc...
 *             Blower/
 *                 01.mat
 *                 etc...
 *             CurvedRamp/
 *                 01.mat
 *                 etc...
 *             Accelerator/
 *                 01.mat
 *                 etc...
 *             Pyramid/
 *                 01.mat
 *                 etc...
 *         Goal/
 *             01.mat
 *             etc...
 *         Ball/
 *             01.mat
 *             etc...
 */
