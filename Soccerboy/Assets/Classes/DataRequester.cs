using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// El propósito de esta clase es brindar procedimientos estáticos 
/// que permitan traen información del servidor fácil y rápida.
/// </summary>

public class DataRequester : MonoBehaviour {

    public delegate void AfterSuccessGetPlayer(Player player);

    public static void GetPlayer(int id, AfterSuccessGetPlayer action) {

        //Crear el GameObject con el componente GraphQL
        GraphQL graphQL = new GameObject().AddComponent<GraphQL>();

        //Hacer la solicitud a GraphQL en un hilo
        graphQL.Query("{ gamePlayer(id: " + id + ") { id, clubName, coins, altCoins } }", delegate(string result) {

            //Convertir el JSON a un objeto Player
            JSONObject json = new JSONObject(result);

            //Abrir el subobjeto "data" y luego el siguiente subobjeto (así entrega la info GraphQL). 
            json = json.list[0].list[0];

            //Armar el objeto
            Player player = new Player() {
                id = json.list[0].str,
                clubName = json.list[1].str,
                coins = (int)json.list[2].n,
                altCoins = (int)json.list[3].n
            };

            //Ejecutar la acción proporcionada con el nuevo objeto
            action(player);
        });
    }

}
