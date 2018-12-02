using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameItem : MonoBehaviour {

	public Item item;			// Scriptable Object

	void Update () {

		/// TODO: Change this behaviour to a collision detection

		if(Input.GetMouseButtonDown(1)){
			if(InventoryManager.Instance.ItemCollected(item))
				Destroy(gameObject);
			else
				Debug.Log("Inventory Full!");
		}
			
	}
}
