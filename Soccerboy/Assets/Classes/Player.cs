using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Clase cuyo propósito es retener datos de un jugador.
/// </summary>

public class Player {

    /// <summary>
    /// Instancia del jugador local.
    /// </summary>
    public static Player local;

    public string id;
    public string clubName;
    public int clubLogo;
    public string token;

    public int coins;
    public int altCoins;

    public Field field;
    public List<int> inventory;

    public Player(string id, string clubName, int clubLogo, string token, int coins, int secondaryCoins, Field field, int[] inventory) {
        this.id = id;
        this.clubName = clubName;
        this.clubLogo = clubLogo;
        this.token = token;
        this.coins = coins;
        this.altCoins = secondaryCoins;
        this.field = field;
        this.inventory = new List<int>(inventory);
    }

    public Player() : this("0", "Sin nombre", 0, "", 0, 0, Field.CreateInstance<Field>(), new int[0]) {
        
    }

    public override string ToString() {
        return "ID: " + id + ", " + clubName + ", " + coins + " monedas, " + altCoins + " monedas secundarias.";
    }

    public static Player FromJson(JSONObject json) {

        //Crear el Player que se va a regresar al final
        Player player = new Player();

        //Iterar por cada Key
        for (int i = 0; i < json.keys.Count; i++) {

            //Revisar en donde asignar el valor de la key actual
            switch (json.keys[i]) {
                case "id": player.id = json.list[i].str; break;
                case "clubName": player.clubName = json.list[i].str; break;
                case "clubLogo": player.clubLogo = (int)json.list[i].n; break;
                case "token": player.token = json.list[i].str; break;
                case "coins": player.coins = (int)json.list[i].n; break;
                case "altCoins": player.altCoins = (int)json.list[i].n; break;
                default: break;
            }
        }
        return player;
    }
}
