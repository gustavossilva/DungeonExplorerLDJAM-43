using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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

	public Vector3 positionPreBattle;

	private bool startGameLoading = true;

	public string monsterId;

	private bool firstBattle = true;
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

	public void Sacrifice()
	{
		int count = 0;
		foreach (bool item in heroesAlive)
		{
				if(item)
					count++;
		}
		if(count == 1)
		{
			GameOver();
			return;
		}
		if(firstBattle){
			GhostManager.Instance.ActiveFreeGhost();
			Time.timeScale = 0;
		}else{
			GhostManager.Instance.ActivePayGhost();
			Time.timeScale = 0;
			int mainIndex = -1;
			if(SelectedCharacter.Instance.selectedCharName == "Barbarian")
				mainIndex = 0;
			if(SelectedCharacter.Instance.selectedCharName == "Paladin")
				mainIndex = 1;
			if(SelectedCharacter.Instance.selectedCharName == "Ranger")
				mainIndex = 2;
			if(SelectedCharacter.Instance.selectedCharName == "Cleric")
				mainIndex = 3;
			if(SelectedCharacter.Instance.selectedCharName == "Wizard")
				mainIndex = 4;
			int sacrificeIndex = Random.Range(0,5);
			while(sacrificeIndex == mainIndex || !heroesAlive[sacrificeIndex])
			{
				sacrificeIndex = Random.Range(0,5);
			}
			heroesAlive[sacrificeIndex] = false;
		}
	}

	public void StartBattle(Transform oldPos, string monsterId){
		// positionPreBattle = oldPos ;
		this.monsterId = monsterId;
		barbaroCharacter.UseItemOnStart();
		rangerCharacter.UseItemOnStart();
		paladinoCharacter.UseItemOnStart();
		wizardCharacter.UseItemOnStart();
		clericCharacter.UseItemOnStart();
		Sacrifice();
		if(firstBattle)
			firstBattle = false;
	}

	protected override void OnEnable() {
		{
			base.OnEnable();
			SceneManager.sceneLoaded += OnLevelFinishedLoading;
		}
	}
	protected override void OnDisable() {
		{
			base.OnDisable();
			SceneManager.sceneLoaded -= OnLevelFinishedLoading;
		}
	}

	public void EndBattle(){
		SceneManager.LoadScene("GameScene");
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		if(!startGameLoading){
			if(scene.name == "GameScene")
			{
				party = GameObject.Find("Player");
				party.transform.position = positionPreBattle;
				MonsterManager.Instance.EndBattle(true,monsterId);
			}
		}
		startGameLoading = false;
	}

	public void GameOver()
	{
		SceneManager.LoadScene("GameOver");
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

		InventoryManager.Instance.ChangeHealth(Character.BARBARIAN, barbaroCharacter.charStats.currentHealth);
		InventoryManager.Instance.ChangeHealth(Character.PALADIN, paladinoCharacter.charStats.currentHealth);
		InventoryManager.Instance.ChangeHealth(Character.RANGER, rangerCharacter.charStats.currentHealth);
		InventoryManager.Instance.ChangeHealth(Character.CLERIC, clericCharacter.charStats.currentHealth);
		InventoryManager.Instance.ChangeHealth(Character.WIZARD, wizardCharacter.charStats.currentHealth);
	}

	public void KillHero(int index)
	{
		heroesAlive[index] = false;
	}
}
