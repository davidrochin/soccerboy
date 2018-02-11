using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods {

    public static Vector3 NoY(this Vector3 vector) {
        return new Vector3(vector.x, 0f, vector.z);
    }

}
