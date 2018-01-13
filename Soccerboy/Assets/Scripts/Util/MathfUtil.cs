using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathfUtil {

	public static float Map(float oldMin, float oldMax, float newMin, float newMax, float value) {
        return Mathf.Lerp(newMin, newMax, Mathf.InverseLerp(oldMin, oldMax, value));
    }
}
