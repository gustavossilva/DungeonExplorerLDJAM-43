using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : Singleton<PartyManager> {

	
	//0 - Barbarian 1 - Paladin 2 - Ranger 3 - Cleric 4 - Wizard
	public bool[] heroesAlive = new bool[5];
	public CharacterStats[] charStats = new CharacterStats[5];

	public Transform positionPreBattle;
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

	public void StartBattle(Transform oldPos){
		positionPreBattle = oldPos;
	}

	public void EndBattle(GameObject party){
		party.transform.position = positionPreBattle.position;
	}

	public void KillHero(int index)
	{
		heroesAlive[index] = false;
	}
}
