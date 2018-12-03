using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Character{
	BARBARIAN,
	CLERIC,
	PALADIN,
	RANGER,
	WIZARD
}

public class InventoryManager : Singleton<InventoryManager> {
	
	public GameObject prefab;
	public Slot slotPaladino;
	public Slot slotRanger;
	public Slot slotClerigo;
	public Slot slotWizard;
	public Slot slotBarbaro;
	public Slot[] slots;

	private const int maxItemsQuantity = 6;
	public int itemsQuantity = 0;

	// Use this for initialization
	protected override void Awake () {
		base.IsPersistentBetweenScenes = true;
		base.Awake();
	}

	public bool ItemCollected(Item item){
		if(itemsQuantity >= maxItemsQuantity)
			return false;

		// Find the next available slot
		for (int i = 0; i < slots.Length; i++){
			if(slots[i].transform.childCount == 0){
				GameObject itemGO = Instantiate(prefab, slots[i].transform);
				slots[i].itemUI = itemGO.GetComponent<ItemDisplay>();
				slots[i].itemUI.effect = item._effect;
				slots[i].itemUI.image.sprite = item._spriteUI;
				slots[i].itemUI.description = item._description;
				itemsQuantity++;
				return true;
			}
		}
		
		return false;
	}

	

	public void ChangeHealth(Character character){
		// Change the health text
	}
}
