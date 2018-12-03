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
			BattleManager.Instance.ChangeCharacter(BattleManager.Instance.cleric, BattleManager.Instance.cleric.animations.attackTime);
			healthBar.value = 50;
			healthBar.gameObject.SetActive(false);
			Debug.Log("Heal Everyone");
		}
		if(healthBar.value <= 0.2 && isPlaying)
		{
			isPlaying = false;
			BattleManager.Instance.cleric.animations.PlayHitAniamtion();
			BattleManager.Instance.activeMonster.animations.PlayAttackAnimation();
			BattleManager.Instance.ChangeCharacter(BattleManager.Instance.cleric, BattleManager.Instance.cleric.animations.hitTime);
			healthBar.value = 50;
			healthBar.gameObject.SetActive(false);
			Debug.Log("Hit player");
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
