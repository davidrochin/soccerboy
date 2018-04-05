using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Componente capáz de hacer llamadas HTTP al servidor GraphQL y traer datos en formato JSON.
/// </summary>

public class GraphQL : MonoBehaviour {

    string serverUrl = "http://deliver.games:8000/graphql";

    public delegate void AfterQuerySuccess(string result);

    /// <summary>
    /// Ejecuta una query en el servidor GraphQL. Como lo hace en una corrutina, no devuelve el
    /// resultado inmediatamente.
    /// </summary>
    /// <param name="query">La query que se quiere hacer.</param>
    /// <param name="action">El procedimiento a ejecutar cuando la información esté lista.</param>
    public void Query(string query, AfterQuerySuccess action) {
        StartCoroutine(QueryRoutine(query, action));
    }

    public IEnumerator QueryRoutine(string query, AfterQuerySuccess action) {
        byte[] requestBody = Encoding.UTF8.GetBytes("{ \"query\":\"" + query + "\" }");
        UnityWebRequest request = UnityWebRequest.Put(serverUrl, requestBody);
        request.method = "POST"; request.SetRequestHeader("content-type", "application/json");

        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError) {
            Debug.Log("Ha habido un error con el request");
            Debug.Log(request.downloadHandler.text);
        } else {
            Debug.Log(request.downloadHandler.text);
            action(request.downloadHandler.text);
        }
    }

}
