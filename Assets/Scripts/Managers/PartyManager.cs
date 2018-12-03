﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartyManager : Singleton<PartyManager> {

	public GameObject party;
	//0 - Barbarian 1 - Paladin 2 - Ranger 3 - Cleric 4 - Wizard
	public bool[] heroesAlive = new bool[5];
	public List<CharacterCombat> characters = new List<CharacterCombat>();

	public Transform positionPreBattle;

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
	}

	public void StartBattle(Transform oldPos, string monsterId){
		positionPreBattle = oldPos ;
		this.monsterId = monsterId;
		SceneManager.LoadScene("Combat");
	}

	public void EndBattle(GameObject party){
		party.transform.position = positionPreBattle.position;
		MonsterManager.Instance.EndBattle(true,monsterId);
	}

	public void AddCharacter(CharacterCombat character){
		characters.Add(character);
	}

	public void IncreasePartyDamage(float modifier){
		foreach(CharacterCombat character in characters){
			character.IncreaseDamage(modifier);
		}
	}

	public void IncreasePartyArmor(float modifier){
		foreach(CharacterCombat character in characters){
			character.IncreaseArmor(modifier);
		}
	}

	public void HealParty(float amount){
		foreach(CharacterCombat character in characters){
			character.Heal(amount);
		}
	}

	public void KillHero(int index)
	{
		heroesAlive[index] = false;
	}
}
