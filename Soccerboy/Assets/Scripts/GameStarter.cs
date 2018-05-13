using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Este Script se debe de ejecutar justo despues de iniciar el juego.
/// Su trabajo es autenticarse con el servidor y cargar el juego
/// para posteriormente pasar al usuario al menú principal.
/// </summary>

public class GameStarter : MonoBehaviour {

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

    void RequestPlayerData() {

    }
	
}
