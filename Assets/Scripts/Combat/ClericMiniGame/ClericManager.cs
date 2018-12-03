using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClericManager : Singleton<ClericManager> {

	public Slider healthBar;
	public bool isPlaying = false;


	protected override void Awake(){
		IsPersistentBetweenScenes = false;
		base.Awake();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		isPlaying = true;
		healthBar.gameObject.SetActive(true);
	}

	void Update () {
		if(!isPlaying)
		{
			return;
		}
		if(healthBar.value >= 99.5 && isPlaying)
		{
			isPlaying = false;
			BattleManager.Instance.cleric.animations.PlayAttackAnimation();
			PartyManager.Instance.HealParty(BattleManager.Instance.cleric.stats.damage.GetValue());
			BattleManager.Instance.ChangeCharacter(BattleManager.Instance.cleric, BattleManager.Instance.cleric.animations.attackTime);
			healthBar.value = 50;
			healthBar.gameObject.SetActive(false);
		}
		if(healthBar.value <= 0.2 && isPlaying)
		{
			isPlaying = false;
			BattleManager.Instance.cleric.animations.PlayHitAniamtion();
			BattleManager.Instance.cleric.stats.TakeDamage(BattleManager.Instance.activeMonster.stats.damage.GetValue());
			InventoryManager.Instance.ChangeHealth(Character.CLERIC, BattleManager.Instance.cleric.stats.currentHealth);
			BattleManager.Instance.activeMonster.animations.PlayAttackAnimation();
			BattleManager.Instance.ChangeCharacter(BattleManager.Instance.cleric, BattleManager.Instance.cleric.animations.hitTime);
			BattleManager.Instance.CheckStats();
			healthBar.value = 50;
			healthBar.gameObject.SetActive(false);
		}
	}

	public void AddHealthBar(float qtdToAdd)
	{
		healthBar.value += qtdToAdd;
	}

	public void RemoveHealthBar(float qtdToRemove)
	{
		healthBar.value -=qtdToRemove;
	}

}
