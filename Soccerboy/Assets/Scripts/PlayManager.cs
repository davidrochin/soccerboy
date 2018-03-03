using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour {

    public PlayMode mode = PlayMode.Play;
    public bool playInProgress;

    public PrefabReferenceGroup prefabReferences;
    public Vector3 sceneCenter;

    [Header("Debug")]
    public bool showDebugButtons = false;

    Ball ball;

    void Awake () {

        //Aparecer la camara si no hay
        if(Camera.main == null) {
            Camera cam = Instantiate(prefabReferences.cameraPrefab).GetComponent<Camera>();

            //Ajustar la camara al centro de los objetos de la escena
            Bounds playBounds = new Bounds(); MeshRenderer[] meshRenderers = FindObjectsOfType<MeshRenderer>();
            foreach (MeshRenderer mr in meshRenderers) { playBounds.Encapsulate(mr.bounds); }
            sceneCenter = playBounds.center;
            Debug.Log("Se encontraron " + meshRenderers.Length + " mesh renderers y el centro es " + sceneCenter);

            cam.transform.rotation = Quaternion.Euler(new Vector3(38.91f, -45f, 0f));
            cam.transform.position = playBounds.center + cam.transform.forward * -1f * 10f;
        }

        //Aparecer el Canvas y su menú de juego si no hay
        if(FindObjectOfType<Canvas>() == null) {
            GameObject canvas = Instantiate(prefabReferences.canvasPrefab);
            Instantiate(prefabReferences.playMenu, canvas.transform, false);
        }

	}

    void Start() {

        ball = FindObjectOfType<Ball>();
        ball.OnOutOfField += RestartPlay;

        Time.maximumDeltaTime = 0.0333333333f;

    }

    void Update () {
		
	}

    public void RestartPlay() {
        if(ball != null) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public IEnumerator RestartPlayAfter(float seconds) {
        yield return new WaitForSeconds(seconds);
        RestartPlay();
    }

    void OnGUI() {

        if (showDebugButtons) {
            //Dibujar el boton para reiniciar la jugada
            if (GUILayout.Button("Reiniciar ejemplo")) {
                RestartPlay();
            }

            //Dibujar los botones para para avanzar y retroceder de ejemplos
            if (SceneManager.GetActiveScene().buildIndex > 0) {
                if (GUILayout.Button("<< Ejemplo anterior")) {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                }
            }

            if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1) {
                if (GUILayout.Button(">> Ejemplo siguiente")) {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }

    }

    public enum PlayMode { Play, Edit, Test }

}

[System.Serializable]
public class PrefabReferenceGroup {

    [Header("UI")]
    public GameObject canvasPrefab;
    public GameObject mainMenu;
    public GameObject playMenu;
    public GameObject editMenu;

    [Header("Play")]
    public GameObject cameraPrefab;
    public GameObject ballPrefab;
    public GameObject goalPrefab;
}
