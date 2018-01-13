using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour {

    public Vector3 ballStartingPos;
    public bool playInProgress;

    public PrefabReferenceGroup prefabReferences;

    float ballRadius = 0.4457389f;

    Ball ball;

    void Awake () {

        //Aparecer la camara si no hay
        if(Camera.main == null) {
            GameObject prefab = Resources.Load("Prefabs/camera", typeof(GameObject)) as GameObject;
            Instantiate(prefab);
        }

        //Aparecer el Canvas si no hay
        if(FindObjectOfType<Canvas>() == null) {
            GameObject prefab = Resources.Load("Prefabs/play_canvas", typeof(GameObject)) as GameObject;
            Instantiate(prefab);
        }

        //Instanciar la pelota
        ball = Instantiate(prefabReferences.ballPrefab, ballStartingPos + Vector3.up * ballRadius, Quaternion.identity).GetComponent<Ball>();

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

        //Dibujar el boton para reiniciar la jugada
        if(GUILayout.Button("Reiniciar ejemplo")) {
            RestartPlay();
        }

        //Dibujar los botones para para avanzar y retroceder de ejemplos
        if(SceneManager.GetActiveScene().buildIndex > 0) {
            if(GUILayout.Button("<< Ejemplo anterior")) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }

        if(SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1) {
            if(GUILayout.Button(">> Ejemplo siguiente")) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    void OnDrawGizmos() {

        //Dibujar el spawn de la pelota
        Gizmos.DrawSphere(Vector3Util.NoY(ballStartingPos) + Vector3.up * ballRadius, ballRadius);
    }

}

[System.Serializable]
public class PrefabReferenceGroup {

    public GameObject ballPrefab;
    public GameObject goalPrefab;

}
