﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartyManager : Singleton<PartyManager> {

	public GameObject party;
	//0 - Barbarian 1 - Paladin 2 - Ranger 3 - Cleric 4 - Wizard
	public bool[] heroesAlive = new bool[5];

	public CharacterCombat barbaroCharacter;
	public CharacterCombat paladinoCharacter;
	public CharacterCombat rangerCharacter;
	public CharacterCombat clericCharacter;
	public CharacterCombat wizardCharacter;

	public CharacterStats barbaroStats;
	public CharacterStats paladinoStats;
	public CharacterStats rangerStats;
	public CharacterStats clericStats;
	public CharacterStats wizardStats;

	public Transform positionPreBattle = null;

	public string monsterId;
	protected override void Awake() {
		IsPersistentBetweenScenes = true;
		base.Awake();
	}
	
	// Use this for initialization
	void Start () {
		for(int i=0;i<5;i++)
		{
			heroesAlive[i] = true;
		}

		barbaroCharacter.slot = InventoryManager.Instance.slotBarbaro;
		rangerCharacter.slot = InventoryManager.Instance.slotRanger;
		paladinoCharacter.slot = InventoryManager.Instance.slotPaladino;
		wizardCharacter.slot = InventoryManager.Instance.slotWizard;
		clericCharacter.slot = InventoryManager.Instance.slotClerigo;

		barbaroCharacter.charStats = barbaroStats;
		rangerCharacter.charStats = rangerStats;
		paladinoCharacter.charStats = paladinoStats;
		clericCharacter.charStats = clericStats;
		wizardCharacter.charStats = wizardStats;
	}

	public void StartBattle(Transform oldPos, string monsterId){
		// positionPreBattle = oldPos ;
		this.monsterId = monsterId;
		SceneManager.LoadScene("Combat");

		barbaroCharacter.UseItemOnStart();
		rangerCharacter.UseItemOnStart();
		paladinoCharacter.UseItemOnStart();
		wizardCharacter.UseItemOnStart();
		clericCharacter.UseItemOnStart();
	}

	public void EndBattle(){
		SceneManager.LoadScene("GameScene");
		party.transform.position = positionPreBattle.position;
		MonsterManager.Instance.EndBattle(true,monsterId);
	}

	// public void AddCharacter(CharacterCombat character){
	// 	characters.Add(character);
	// }

	public void IncreasePartyDamage(float modifier){
		barbaroCharacter.IncreaseDamage(modifier);
		paladinoCharacter.IncreaseDamage(modifier);
		rangerCharacter.IncreaseDamage(modifier);
		clericCharacter.IncreaseDamage(modifier);
		wizardCharacter.IncreaseDamage(modifier);
	}

	public void IncreasePartyArmor(float modifier){
		barbaroCharacter.IncreaseArmor(modifier);
		paladinoCharacter.IncreaseArmor(modifier);
		rangerCharacter.IncreaseArmor(modifier);
		clericCharacter.IncreaseArmor(modifier);
		wizardCharacter.IncreaseArmor(modifier);
	}

	public void HealParty(float amount){
		barbaroCharacter.Heal(amount);
		paladinoCharacter.Heal(amount);
		rangerCharacter.Heal(amount);
		clericCharacter.Heal(amount);
		wizardCharacter.Heal(amount);
	}

	public void KillHero(int index)
	{
		heroesAlive[index] = false;
	}
}
