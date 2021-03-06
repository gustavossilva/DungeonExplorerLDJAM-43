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
	
	public GameObject prefab, barbarianX, paladinX, rangerX, clericX, wizardX;
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
				if(currentHealth <= 0f){
					barbarianX.SetActive(true);
					if(!slotBarbaro.IsEmpty()){
						slotBarbaro.itemUI = null;
						Destroy(slotBarbaro.transform.GetChild(0).gameObject);
					}
				}
				barbarianHealthText.text = health;
				break;
			case Character.CLERIC:
				if(currentHealth <= 0f){
					clericX.SetActive(true);
					if(!slotClerigo.IsEmpty()){
						slotClerigo.itemUI = null;
						Destroy(slotClerigo.transform.GetChild(0).gameObject);
					}
				}
				clericHealthText.text = health;
				break;
			case Character.PALADIN:
				if(currentHealth <= 0f){
					paladinX.SetActive(true);
					if(!slotPaladino.IsEmpty()){
						slotPaladino.itemUI = null;
						Destroy(slotPaladino.transform.GetChild(0).gameObject);
					}
				}
				paladinHealthText.text = health;
				break;
			case Character.RANGER:
				if(currentHealth <= 0f){
					rangerX.SetActive(true);
					if(!slotRanger.IsEmpty()){
						slotRanger.itemUI = null;
						Destroy(slotRanger.transform.GetChild(0).gameObject);
					}
				}
				rangerHealthText.text = health;
				break;
			case Character.WIZARD:
				if(currentHealth <= 0f){
					wizardX.SetActive(true);
					if(!slotWizard.IsEmpty()){
						slotWizard.itemUI = null;
						Destroy(slotWizard.transform.GetChild(0).gameObject);
					}
				}
				wizardHealthText.text = health;
				break;
		}
	}
}
