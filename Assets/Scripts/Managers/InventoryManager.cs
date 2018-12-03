﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	public Text barbarianHealthText;
	public Text paladinHealthText;
	public Text rangerHealthText;
	public Text clericHealthText;
	public Text wizardHealthText;

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

	

	public void ChangeHealth(Character character, float currentHealth){
		string health = "Health: " + currentHealth;
		// Change the health text
		switch(character){
			case Character.BARBARIAN:
				barbarianHealthText.text = health;
				break;
			case Character.CLERIC:
				clericHealthText.text = health;
				break;
			case Character.PALADIN:
				paladinHealthText.text = health;
				break;
			case Character.RANGER:
				rangerHealthText.text = health;
				break;
			case Character.WIZARD:
				wizardHealthText.text = health;
				break;

		}
	}
}
