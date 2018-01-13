using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDisabler : MonoBehaviour {

	void Awake () {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null) { Destroy(meshRenderer); }

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null) { Destroy(meshFilter); }
    }

}
