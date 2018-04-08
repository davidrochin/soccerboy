using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Item {

	public int id;
	public string name;
    public string description;
    public string thumbnail;
	public Type type;

    public Item(string name, string description, string thumbnail) {
        id = nextId; nextId++;
        this.name = name;
        this.description = description;
        this.thumbnail = thumbnail;
    }

    public override string ToString() {
        return name + ", " + description;
    }

    public static Item[] list = {
        new Item("Barrera lateral", "La descripción de la barrera lateral", "lateral_barrier"),
        new Item("Barrera chica", "La descripción de la barrera chica", "small_barrier"),
    };

    public static int nextId = 0;

    public enum Type { Obstacle, FieldPlayer }

}
