using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager> {

	public CharacterStats monsterStats;
	public GameObject monster;

	protected override void Awake() {
		IsPersistentBetweenScenes = true;
		base.Awake();
	}

	public void StartBattle(GameObject monsterEncounter){
		monster = monsterEncounter;
		monsterStats = monster.GetComponent<CharacterStats>();
	}

	public void EndBattle(bool victoryCondition){
		if(victoryCondition)
		{
			//GameOver
		}
		else
		{
			monster.SetActive(false);
		}
	}

}
