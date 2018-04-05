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

	void Start () {

        //Obtener el jugador con ID 2, desde el servidor, como prueba.
        DataRequester.GetPlayer(2, delegate(Player p) {
            Debug.Log(p);    
        });
	}
	
}
