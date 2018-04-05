using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuestraDeCodigo : MonoBehaviour {
	Inventory inv;
	// Use this for initialization
	void Start () {
		inv = GetComponent<Inventory> ();
		Invoke ("agregoItemsAlInventario", 0.1f);
	}


	//Los ID los cree en el inspector en el gameobject "Script ", componente "inventory"
	void agregoItemsAlInventario(){
		inv.addItemByID (0);//agrego item por su ID;
		inv.addItemByID (1);
		inv.addItemByID (2);
		inv.addItemByID (3);
		inv.removeItemByID (2); // borre un item por su ID;
		inv.addItem (inv.items [1]); //agregue un item pasandole el item a agregar directamente
		GetComponent<InventoryTest>().actualizoValores(); // esto es solamente para q funcione bien el dropdown de la escena de test.

	}
}
