using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Este Script se debe de ejecutar justo despues de iniciar el juego.
/// Su trabajo es autenticarse con el servidor y cargar el juego
/// para posteriormente pasar al usuario al menú principal.
/// </summary>

public class GameStarter : MonoBehaviour {

    public Slider loadingBar;

	void Awake () {

        //Si el jugador es nuevo, solicitar la creacion de un nuevo perfil y guardar su id y su pass
        if(PlayerPrefs.GetString("local_player_id", "null").Equals("null")) {

        }

        //Si no es nuevo, solicitar los datos del jugador al servidor
        else {
            RequestPlayerData();
        }

        //Obtener el jugador con ID 2, desde el servidor, como prueba.
        /*DataRequester.GetPlayer(2, delegate(Player p) {
            Debug.Log(p);    
        });*/
	}

    void Update() {
        loadingBar.value = loadingBar.value + 0.5f * Time.deltaTime;
        if(loadingBar.value >= 1f) { SceneManager.LoadScene("menu"); }
    }


    void RequestPlayerData() {

    }
	
}
