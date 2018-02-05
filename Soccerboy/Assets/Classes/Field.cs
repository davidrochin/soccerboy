using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field {

    public int template;
    public FieldElement[] fieldElements; //(posiblemente una List nos sea más 
                                         //conveniente del lado del cliente).
    public Vector3 goalPosition;
    public Vector3 goalRotation;

    public Vector3 ballSpawn;

}

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

