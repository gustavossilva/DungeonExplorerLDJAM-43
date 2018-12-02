using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour {
		


	public GameObject barbGame, pallyGame, rangerGame, clericGame, wizzardGame;
	public Hero barbarian, paladin, ranger, cleric, wizzard;
	public Slider clericSlider;	
	public float slideSpeed = 5;
	public GameObject Monster;
	public Transform battlePos, initialPosition;
	//Handle Alive players
	//Carrega stats
	//Gera o efeito dos itens
	//Habilita os personagens
	//Inicia animação de entrada do primeiro personagem na fila
	//Inicia o minigame referente ao personagem em primeiro na fila
	private void Start() {
		if(PartyManager.Instance.heroesAlive[0])
			barbarian.isAlive = true;
			barbarian.hero.SetActive(true);
		if(PartyManager.Instance.heroesAlive[1])
			paladin.isAlive = true;
			paladin.hero.SetActive(true);
		if(PartyManager.Instance.heroesAlive[2])
			ranger.isAlive = true;
			ranger.hero.SetActive(true);
		if(PartyManager.Instance.heroesAlive[3])
			cleric.isAlive = true;
			cleric.hero.SetActive(true);
		if(PartyManager.Instance.heroesAlive[4])
			wizzard.isAlive = true;
			wizzard.hero.SetActive(true);
		//get playersNumber from manager
	}

	public void Battle()
	{

	}

	private void Update() {
		if(barbarian.hero.activeSelf)
		{
			if(!barbarian.isBattling)
				barbarian.transform.position = Vector2.MoveTowards(barbarian.transform.position,battlePos.position,slideSpeed * Time.deltaTime);
			if(barbarian.transform.position.x == battlePos.position.x)
				barbarian.isBattling = true;
		}else if(paladin.hero.activeSelf)
		{
			if(!paladin.isBattling)
				paladin.transform.position = Vector2.MoveTowards(paladin.transform.position,battlePos.position,slideSpeed * Time.deltaTime);
			if(paladin.transform.position.x == battlePos.position.x)		
				paladin.isBattling = true;
		}else if(ranger.hero.activeSelf)
		{
			if(!ranger.isBattling)			
				ranger.transform.position = Vector2.MoveTowards(ranger.transform.position,battlePos.position,slideSpeed * Time.deltaTime);
			if(ranger.transform.position.x == battlePos.position.x)
				ranger.isBattling = true;
		}else if(cleric.hero.activeSelf)
		{
			if(!cleric.isBattling)			
				cleric.transform.position = Vector2.MoveTowards(cleric.transform.position,battlePos.position,slideSpeed * Time.deltaTime);
			if(cleric.transform.position.x == battlePos.position.x)
				cleric.isBattling = true;
		}else if(wizzard.hero.activeSelf)
		{
			if(!wizzard.isBattling)			
				wizzard.transform.position = Vector2.MoveTowards(wizzard.transform.position,battlePos.position,slideSpeed * Time.deltaTime);
			if(wizzard.transform.position.x == battlePos.position.x)
				wizzard.isBattling = true;
		}else{
			//RestartBattle();
		}
		if(Input.GetKeyDown(KeyCode.Q))
		{
			ChangeCharacter(barbarian);
		}
		if(Input.GetKeyDown(KeyCode.W))
		{
		ChangeCharacter(paladin);
		}
		if(Input.GetKeyDown(KeyCode.E))
		{
		ChangeCharacter(ranger);
		}
		if(Input.GetKeyDown(KeyCode.R))
		{
			ChangeCharacter(cleric);
		}
	}

	public void ChangeCharacter(Hero character)
	{
		StartCoroutine(MoveBack(character));
	}

	IEnumerator MoveBack(Hero character)
	{
		character.isBattling = false;
		while(character.transform.position.x > initialPosition.position.x)
		{
			character.transform.position = Vector2.MoveTowards(character.transform.position,initialPosition.position,slideSpeed * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
		character.hero.SetActive(false);
	}

[System.Serializable]
public struct Hero{

	public GameObject hero;
	public bool isAlive;
	public bool isBattling;
	public Transform transform;
}


}
