using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Clase cuyo propósito es retener datos de un jugador.
/// </summary>

public class Player {

    /// <summary>
    /// Instancia estática creada para poder
    /// accesar a los datos del jugador local
    /// desde cualquier parte.
    /// </summary>
    public static Player local;

    public string id;
    public string clubName;
    public int insignia;
    public string token;

    public int coins;
    public int altCoins;

    public Field field;

    public Player(string id, string clubName, int insignia, string token, int coins, int secondaryCoins, Field field) {
        this.id = id;
        this.clubName = clubName;
        this.insignia = insignia;
        this.token = token;
        this.coins = coins;
        this.altCoins = secondaryCoins;
        this.field = field;
    }

    public Player() : this("0", "Sin nombre", 0, "", 0, 0, Field.CreateInstance<Field>()) {
        
    }

    public override string ToString() {
        return "ID: " + id + ", " + clubName + ", " + coins + " monedas, " + altCoins + " monedas secundarias.";
    }
}
