using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3Util {

    public static Vector3 QuadraticInterpolation(Vector3 a, Vector3 b, Vector3 c, float t) {
        Vector3 p0 = Vector3.Lerp(a, b, t);
        Vector3 p1 = Vector3.Lerp(b, c, t);
        return Vector3.Lerp(p0, p1, t);
    }

    public static Vector3 CubicInterpolation(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t) {
        Vector3 p0 = QuadraticInterpolation(a, b, c, t);
        Vector3 p1 = QuadraticInterpolation(b, c, d, t);
        return Vector3.Lerp(p0, p1, t);
    }

    public static Vector3 RotateAroundPoint(Vector3 point, Vector3 pivot, Quaternion rotation) {
        return rotation * (point - pivot) + pivot;
    }

    public static float InnerProduct(Vector3 a, Vector3 b, Vector3 point) {
        Vector3 delta = b - a;
        return (point.x - a.x) * delta.x + (point.y - a.y) * delta.y + (point.z - a.z) * delta.z;
    }

    /// <summary>
    /// Regresa un Vector3 con su componente Y en zero.
    /// </summary>
    public static Vector3 NoY(Vector3 vector) {
        return new Vector3(vector.x, 0f, vector.z);
    }

}
