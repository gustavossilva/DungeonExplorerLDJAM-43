using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
	
	public GameObject prefab;
	public Slot slotCharA;
	public Slot slotCharB;
	public Slot slotCharC;
	public Slot slotCharD;
	public Slot slotMainChar;
	public Slot[] slots;

	private const int maxItemsQuantity = 8;
	private int itemsQuantity = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(1)){
			ItemCollected();
		}
	}

	void ItemCollected(){
		if(itemsQuantity <= maxItemsQuantity){
			// Find the next available slot
			for (int i = 0; i < slots.Length; i++){
				if(slots[i].transform.childCount == 0){
					GameObject go = Instantiate(prefab, slots[i].transform);
					slots[i].item = go.GetComponent<Item>();
					return;
				}
			}

			// TODO: something, if I could not find any slot with no children
		}
	}
}
