using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTest : MonoBehaviour {
	Text textInfo,texti;
	Inventory inv;
	Dropdown drp;
	Slider sb;
	// Use this for initialization
	void Start () {
		textInfo = GameObject.Find ("TextInfo").GetComponent<Text> ();
		inv = GetComponent<Inventory> ();
		drp = GameObject.FindObjectOfType<Dropdown> ();
		drp.AddOptions (listainttolistaString ());
		sb = GameObject.FindObjectOfType<Slider> ();
		texti = GameObject.Find ("Texti").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GetMYIDS(){
		textInfo.text = "";
		foreach (int nro in inv.inventory) {
			textInfo.text = textInfo.text + nro.ToString () + "\n";
		}
	}

	public void GetMYItems(){
		textInfo.text = "";
		foreach (Item item in inv.getMyItems()) {
			textInfo.text = textInfo.text +  "ID: " + item.ID + "\n" + "Nombre: " + item.nombre + "\n" + "Descripcion: " + item.descripcion + "\n" + "\n";
		}
	}

	public void removeItem(){
		inv.removeItemByID (int.Parse(drp.captionText.text));
		drp.ClearOptions ();
		drp.AddOptions(listainttolistaString ());
	}

	public void agregarItem(){
		inv.addItemByID ((int) sb.value);
		drp.ClearOptions ();
		drp.AddOptions(listainttolistaString ());
	}

	List<string> listainttolistaString(){
		List<string> ls = new List<string> ();
		foreach (int a in inv.inventory) {
			ls.Add (a.ToString ());
		}
		return ls;
	}

	public void valore(){
		texti.text = sb.value.ToString ();
	}
		
}
