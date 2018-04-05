using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {


	[HideInInspector]
	public List<int> inventory = new List<int>();
	public Item[] items;

	public Item[] getMyItems (){ // devuelve los items en formato Items;
		List<Item> MyItems = new List<Item> ();
		foreach (int nro in inventory) {
			MyItems.Add (searchItemByID (nro));
		}
		return MyItems.ToArray();
	}

	public int[] getMyIDS () { //devuelve un array de los ID de los item
		return inventory.ToArray();
	}
		
	public void addItemByID(int id){ //agrega un item al inventario por su id
		inventory.Add(id);
	}

	public void addItem(Item it){ //agrega un item al inventario pasando el item
		inventory.Add(it.ID);
	}

	public void removeItemByID(int id){ //borra un item del inventario;
		inventory.Remove (id);
	}

	public void removeItem(Item it){ //borra un item del inventario;
		inventory.Remove (it.ID);
	}

	Item searchItemByID(int id){ // busca en la lista de items, el que tenga el ID en cuestion
		foreach (Item item in items) {
			if (item.ID == id) return item;
		}
		return null;
	}
}
[System.Serializable]
public class Item{

	public enum tipo
	{
		Player,
		Obstacle,
		Ramp
	}

	public int ID;
	[HideInInspector]
	public int GamePlayer_ID;
	public tipo type;
	public string nombre;
	public string descripcion;

}
