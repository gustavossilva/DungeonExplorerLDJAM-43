using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager> {
	
	public GameObject prefab;
	public Slot slotCharA;
	public Slot slotCharB;
	public Slot slotCharC;
	public Slot slotCharD;
	public Slot slotMainChar;
	public Slot[] slots;

	private const int maxItemsQuantity = 8;
	public int itemsQuantity = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(1)){
			ItemCollected();
		}
	}

	public void ItemCollected(){
		if(itemsQuantity <= maxItemsQuantity){
			// Find the next available slot
			for (int i = 0; i < slots.Length; i++){
				if(slots[i].transform.childCount == 0){
					GameObject itemGO = Instantiate(prefab, slots[i].transform);
					slots[i].item = itemGO.GetComponent<Item>();
					// slots[i].item.image = ...
					itemsQuantity++;
					return;
				}
			}
		}
	}

	public void ChangeHealth(){
		// Change the health text
	}
}
