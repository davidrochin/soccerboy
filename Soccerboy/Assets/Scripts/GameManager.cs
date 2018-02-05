using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public PrefabReferenceGroup prefabReferences;

    static GameManager instance;

	void Awake () {
		if(instance == null) { instance = this; }
        if (instance != null && instance != this) { Destroy(this); }
        DontDestroyOnLoad(gameObject);
	}
	
}
