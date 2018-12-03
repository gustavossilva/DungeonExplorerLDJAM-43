using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BattleManager : Singleton<BattleManager> {
		


	public GameObject barbGame, pallyGame, rangerGame, clericGame, wizardGame;
	public Hero barbarian, paladin, ranger, cleric, wizard;
	public Slider clericSlider;
	public Slider enemyHealthBar, barbarianHealthBar, paladinHealthBar, rangerHealthBar, clericHealthBar, wizardHealthBar;
	public float slideSpeed = 5;
	public Monster skeleton, trund, goburin, slime, boss;
	public Monster activeMonster;
	public Transform battlePos, initialPosition;
	public TextMeshProUGUI winText;
	public bool gameOver = false;
	//Handle Alive players
	//Carrega stats
	//Gera o efeito dos itens
	//Habilita os personagens
	//Inicia animação de entrada do primeiro personagem na fila
	//Inicia o minigame referente ao personagem em primeiro na fila
	protected override void Awake() {
		IsPersistentBetweenScenes = false;
		base.Awake();
	}

	private void Start() {
		 
		if(PartyManager.Instance.heroesAlive[0]){
			barbarian.isAlive = true;
			barbarian.hero.SetActive(true);
			barbarian.stats = PartyManager.Instance.barbaroCharacter.charStats;
		}else{
			barbarianHealthBar.transform.GetChild(3).gameObject.SetActive(true);
			barbarian.stats = PartyManager.Instance.barbaroCharacter.charStats;
		}
		if(PartyManager.Instance.heroesAlive[1]){
			paladin.isAlive = true;
			paladin.hero.SetActive(true);
			paladin.stats = PartyManager.Instance.paladinoCharacter.charStats;
		}else{
			paladinHealthBar.transform.GetChild(3).gameObject.SetActive(true);
			paladin.stats = PartyManager.Instance.paladinoCharacter.charStats;

		}
		if(PartyManager.Instance.heroesAlive[2]){
			ranger.isAlive = true;
			ranger.hero.SetActive(true);
			ranger.stats = PartyManager.Instance.rangerCharacter.charStats;
		}else{
			rangerHealthBar.transform.GetChild(3).gameObject.SetActive(true);	
			ranger.stats = PartyManager.Instance.rangerCharacter.charStats;

		}
		if(PartyManager.Instance.heroesAlive[3]){
			cleric.isAlive = true;
			cleric.hero.SetActive(true);
			cleric.stats = PartyManager.Instance.clericCharacter.charStats;
		}else{
			clericHealthBar.transform.GetChild(3).gameObject.SetActive(true);
			cleric.stats = PartyManager.Instance.clericCharacter.charStats;

		}
		if(PartyManager.Instance.heroesAlive[4]){
			wizard.isAlive = true;
			wizard.hero.SetActive(true);
			wizard.stats = PartyManager.Instance.wizardCharacter.charStats;
		}else{
			wizardHealthBar.transform.GetChild(3).gameObject.SetActive(true);
			wizard.stats = PartyManager.Instance.wizardCharacter.charStats;

		}
			
		UpdatePartyHealthBars();	

		


		skeleton.stats = skeleton.monster.GetComponent<CharacterStats>();
		trund.stats = trund.monster.GetComponent<CharacterStats>();
		goburin.stats = goburin.monster.GetComponent<CharacterStats>();
		slime.stats = slime.monster.GetComponent<CharacterStats>();
		// boss.stats = boss.monster.GetComponent<CharacterStats>();
		if(MonsterManager.Instance.monsterName == "Skeleton")
		{
				activeMonster = skeleton;
		}
		else if(MonsterManager.Instance.monsterName == "Trund")
		{
			activeMonster = trund;
		}
		else if(MonsterManager.Instance.monsterName == "Goburin")
		{
			activeMonster = goburin;
		}
		else if(MonsterManager.Instance.monsterName == "Slime")
		{
			activeMonster = slime;
		}
	  else
		{
			activeMonster = boss;
		}
		// activeMonster = skeleton;
		activeMonster.monster.SetActive(true);

		barbarian.isAlive = true;
		paladin.isAlive = true;
		ranger.isAlive = true;
		cleric.isAlive = true;
		wizard.isAlive = true;
	}

	public void TurnOffGames()
	{
		barbGame.SetActive(false);
		pallyGame.SetActive(false);
		rangerGame.SetActive(false);
		clericGame.SetActive(false);
		wizardGame.SetActive(false);
	}

	private void Update() {
		if(gameOver)
			return;
		if(barbarian.hero.activeSelf)
		{
			if(!barbarian.isBattling)
				barbarian.transform.position = Vector2.MoveTowards(barbarian.transform.position,battlePos.position,slideSpeed * Time.deltaTime);
			if(barbarian.transform.position.x == battlePos.position.x)
			{
				barbarian.isBattling = true;
				barbGame.SetActive(true);
			}
		}
		else if(paladin.hero.activeSelf)
		{
			//if monster is dead, endbattle
			if(!paladin.isBattling)
				paladin.transform.position = Vector2.MoveTowards(paladin.transform.position,battlePos.position,slideSpeed * Time.deltaTime);
			if(paladin.transform.position.x == battlePos.position.x)
			{
				paladin.isBattling = true;
				pallyGame.SetActive(true);
			}
		}else if(ranger.hero.activeSelf)
		{
			//if monster is dead, endbattle
			if(!ranger.isBattling)			
				ranger.transform.position = Vector2.MoveTowards(ranger.transform.position,battlePos.position,slideSpeed * Time.deltaTime);
			if(ranger.transform.position.x == battlePos.position.x)
			{
				ranger.isBattling = true;
				rangerGame.SetActive(true);
			}
		}else if(cleric.hero.activeSelf)
		{
			//if monster is dead, endbattle
			if(!cleric.isBattling)			
				cleric.transform.position = Vector2.MoveTowards(cleric.transform.position,battlePos.position,slideSpeed * Time.deltaTime);
			if(cleric.transform.position.x == battlePos.position.x){
				cleric.isBattling = true;
				clericGame.SetActive(true);
			}
		}else if(wizard.hero.activeSelf)
		{
			//if monster is dead, endbattle
			if(!wizard.isBattling)			
				wizard.transform.position = Vector2.MoveTowards(wizard.transform.position,battlePos.position,slideSpeed * Time.deltaTime);
			if(wizard.transform.position.x == battlePos.position.x){
				wizard.isBattling = true;
				wizardGame.SetActive(true);
			}
		}else{
			//if monster is dead, endbattle
			//else
			BattleAgain();
		}
	}

	public void BattleAgain()
	{
		if(barbarian.isAlive){
			barbarian.hero.SetActive(true);
			barbarian.isBattling = false;
		}
		if(paladin.isAlive){
			paladin.hero.SetActive(true);
			paladin.isBattling = false;
		}
		if(ranger.isAlive){
			ranger.hero.SetActive(true);
			ranger.isBattling = false;
		}
		if(cleric.isAlive){
			cleric.hero.SetActive(true);
			cleric.isBattling = false;
		}
		if(wizard.isAlive){
			wizard.hero.SetActive(true);
			wizard.isBattling = false;
		}
	}

	public void UpdatePartyHealthBars(){
		barbarianHealthBar.value = (barbarian.stats.currentHealth / barbarian.stats.maxHealth.baseValue) * 100;
		paladinHealthBar.value = (paladin.stats.currentHealth / paladin.stats.maxHealth.baseValue) * 100;	
		rangerHealthBar.value = (ranger.stats.currentHealth / ranger.stats.maxHealth.baseValue) * 100;	
		clericHealthBar.value = (cleric.stats.currentHealth / cleric.stats.maxHealth.baseValue) * 100;	
		wizardHealthBar.value = (wizard.stats.currentHealth / wizard.stats.maxHealth.baseValue) * 100;
	}

	public void UpdateEnemyHealthBar(){
		enemyHealthBar.value = (activeMonster.stats.currentHealth / activeMonster.stats.maxHealth.baseValue) * 100;
	}

	public void CheckStats()
	{
		if(activeMonster.stats.currentHealth <= 0)
		{
			activeMonster.animations.PlayDeathAnimation();
			StopAllCoroutines();
			gameOver = true;
			StartCoroutine(WinBattle());
		}
		if(barbarian.stats.currentHealth <= 0)
		{
			if(SelectedCharacter.Instance.selectedCharName == "Barbarian"){
				PartyManager.Instance.GameOver();
			}
			barbarian.isAlive = false;
			PartyManager.Instance.heroesAlive[0] = false;
			barbarianHealthBar.transform.GetChild(3).gameObject.SetActive(true);	
		}
		if(paladin.stats.currentHealth <= 0)
		{
			if(SelectedCharacter.Instance.selectedCharName == "Paladin"){
				PartyManager.Instance.GameOver();
			}
			paladin.isAlive = false;
			PartyManager.Instance.heroesAlive[1] = false;
			paladinHealthBar.transform.GetChild(3).gameObject.SetActive(true);	
		}
		if(ranger.stats.currentHealth <= 0)
		{
			if(SelectedCharacter.Instance.selectedCharName == "Ranger"){
				PartyManager.Instance.GameOver();				
			}
			ranger.isAlive = false;
			PartyManager.Instance.heroesAlive[2] = false;
			rangerHealthBar.transform.GetChild(3).gameObject.SetActive(true);	
		}
		if(cleric.stats.currentHealth <= 0)
		{
			if(SelectedCharacter.Instance.selectedCharName == "Cleric"){
				PartyManager.Instance.GameOver();				
			}
			cleric.isAlive = false;
			PartyManager.Instance.heroesAlive[3] = false;
			clericHealthBar.transform.GetChild(3).gameObject.SetActive(true);	
		}
		if(wizard.stats.currentHealth <= 0)
		{
			if(SelectedCharacter.Instance.selectedCharName == "Wizard"){
				PartyManager.Instance.GameOver();				
			}
			wizard.isAlive = false;
			PartyManager.Instance.heroesAlive[4] = false;
			wizardHealthBar.transform.GetChild(3).gameObject.SetActive(true);	
		}
		if(!barbarian.isAlive && !paladin.isAlive && !ranger.isAlive && !cleric.isAlive && !wizard.isAlive)
		{
				PartyManager.Instance.GameOver();
		}
	}

	public void ChangeCharacter(Hero character, float delay=0)
	{
		StartCoroutine(MoveBack(character, delay));
	}

	IEnumerator WinBattle()
	{
		winText.gameObject.SetActive(true);
		yield return new WaitForSeconds(activeMonster.animations.deathTime+0.5f);
		PartyManager.Instance.EndBattle();
	}

	IEnumerator MoveBack(Hero character, float delay)
	{
		character.isBattling = false;
		yield return new WaitForSeconds(delay);
		while(character.transform.position.x > initialPosition.position.x)
		{
			character.transform.position = Vector2.MoveTowards(character.transform.position,initialPosition.position,slideSpeed * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
		TurnOffGames();
		character.hero.SetActive(false);
	}

[System.Serializable]
public struct Hero{

	public GameObject hero;
	public bool isAlive;
	public bool isBattling;
	public Transform transform;
	public BattlePlayersAnimation animations;
	public CharacterStats stats;
}

[System.Serializable]
public struct Monster {
	public GameObject monster;
	public float health;
	public BattleMonsterAnimation animations;
	public CharacterStats stats;
}


}
