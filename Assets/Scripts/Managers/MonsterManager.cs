using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager> {

	public CharacterStats monsterStats;
	public GameObject monster;

	public Dictionary<string,bool> monstersInGame;

	public string monsterName = ""; 


	protected override void Awake() {
		monstersInGame = new Dictionary<string,bool> ();
		IsPersistentBetweenScenes = true;
		base.Awake();
	}

	public void StartBattle(Transform player,string monsterId){
		PartyManager.Instance.StartBattle(player,monsterId);
	}

	public void EndBattle(bool victoryCondition,string monsterId){
		if(victoryCondition)
		{
			//GameOver
		}
		else
		{
			monstersInGame[monsterId] = false;
		}
	}

}
