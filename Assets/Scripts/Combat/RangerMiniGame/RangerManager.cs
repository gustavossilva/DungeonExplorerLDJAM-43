using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RangerManager : Singleton<RangerManager> {

	public TextMeshProUGUI timer;
	public int actualTime;
	public int maxTime;
	public bool isPlaying = false;
	public bool winner, loser;
	protected override void Awake() {
		IsPersistentBetweenScenes = false;
		base.Awake();
	}

	protected override void OnEnable(){
		base.OnEnable();
		isPlaying = true;
		timer.text = maxTime.ToString();
		actualTime = maxTime;
		timer.gameObject.SetActive(true);
		StartCoroutine(Timer());
	}

	// Update is called once per frame
	void Update () {
		if(!isPlaying)
			return;
		if(winner && isPlaying)
		{
			isPlaying = false;
			winner = false;
			loser = false;	
			BattleManager.Instance.ranger.animations.PlayAttackAnimation();
			BattleManager.Instance.activeMonster.animations.PlayHitAniamtion();
			BattleManager.Instance.activeMonster.stats.TakeDamage(BattleManager.Instance.ranger.stats.damage.GetValue());
			BattleManager.Instance.CheckStats();
			BattleManager.Instance.UpdateEnemyHealthBar();
			BattleManager.Instance.ChangeCharacter(BattleManager.Instance.ranger, BattleManager.Instance.ranger.animations.attackTime);
			timer.gameObject.SetActive(false);
			StopAllCoroutines();
		}
		if(loser && isPlaying)
		{
			Loser();
			
		}
		if(actualTime == 0)
		{
			loser = true;
		}
	}

	public void Loser()
	{
			isPlaying = false;
			winner = false;
			loser = false;
			BattleManager.Instance.ranger.animations.PlayHitAniamtion();
			BattleManager.Instance.activeMonster.animations.PlayAttackAnimation();
			BattleManager.Instance.ranger.stats.TakeDamage(BattleManager.Instance.activeMonster.stats.damage.GetValue());
			InventoryManager.Instance.ChangeHealth(Character.RANGER, BattleManager.Instance.ranger.stats.currentHealth);
			BattleManager.Instance.CheckStats();
			BattleManager.Instance.UpdatePartyHealthBars();
			BattleManager.Instance.ChangeCharacter(BattleManager.Instance.ranger, BattleManager.Instance.ranger.animations.hitTime);
			timer.gameObject.SetActive(false);
			StopAllCoroutines();
	}

	IEnumerator Timer (){
		WaitForSeconds wait = new WaitForSeconds(1f);
		while(actualTime != 0)
		{
			yield return wait;
			actualTime--;
			timer.text = actualTime.ToString();
		}
	}

}
