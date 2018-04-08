using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Brinda procedimientos estáticos 
/// que permiten traer información del servidor.
/// </summary>

public class DataRequester : MonoBehaviour {

    public delegate void AfterSuccessGetPlayer(Player player);
    public delegate void AfterFail();

    /// <summary>
    /// Solicita la información de un jugador al servidor.
    /// </summary>
    /// <param name="id">El ID del jugador del cual se quiere la información.</param>
    /// <param name="action">El código a ejecutar cuando llegue la información del jugador.</param>
    public static void GetPlayer(int id, AfterSuccessGetPlayer action) {

        //Crear el GameObject con el componente GraphQL
        GraphQL graphQL = new GameObject("GraphQL container").AddComponent<GraphQL>();

        //Hacer la solicitud a GraphQL con el componente creado
        graphQL.Query("{ gamePlayer(id: " + id + ") { id, clubName, coins, altCoins } }", delegate(string result) {

            //Borrar el objeto GraphQL porque ya no se necesita
            Destroy(graphQL.gameObject);

            //Convertir el string a un objeto JSON
            JSONObject json = new JSONObject(result);

            //Abrir el subobjeto "data" y luego el siguiente subobjeto (así entrega la info GraphQL). 
            json = json.list[0].list[0];

            //Armar el objeto
            Player player = Player.FromJson(json);

            //Ejecutar la acción proporcionada con el nuevo objeto
            action(player);
            
        });
    }

}
