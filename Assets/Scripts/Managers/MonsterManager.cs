using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager> {

	public CharacterStats monsterStats;
	public GameObject monster;

	public string monsterName = "";


	protected override void Awake() {
		IsPersistentBetweenScenes = true;
		base.Awake();
	}

	public void StartBattle(Transform player){
		PartyManager.Instance.StartBattle(player);
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
