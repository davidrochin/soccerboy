using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public PrefabReferenceGroup prefabReferences;
	public string playFieldSceneName;

	[NonSerialized]
	public Field currentField;
	[NonSerialized]
	public FieldLoadingMode currentFieldLoadingMode;

    public static GameManager instance;

	void Awake () {
		if (instance == null) { instance = this; }
		if (instance != null && instance != this) { Destroy (this); }
		else { DontDestroyOnLoad (gameObject); }
    
        Time.maximumDeltaTime = 0.0333333333f;
	}

	/// <summary>
	/// Carga un campo.
	/// </summary>
	/// <param name="f">El campo a cargar.</param>
	/// <param name="mode">De qué manera debería ser cargado el campo.</param>
	public static void StartField(Field f, FieldLoadingMode mode) {
		instance.currentField = f;
		instance.currentFieldLoadingMode = mode;

		// Cargar escena, la carga del nivel se hace automáticamente al inicio de esta
		SceneManager.LoadScene (instance.playFieldSceneName);
	}

}
