using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerManager : Singleton<RangerManager> {

	public bool isPlaying = false;
	public bool winner, loser;
	protected override void Awake() {
		IsPersistentBetweenScenes = false;
		base.Awake();
	}

	protected override void OnEnable(){
		base.OnEnable();
		isPlaying = true;
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
			//BattleManager.CheckStats();
			BattleManager.Instance.ChangeCharacter(BattleManager.Instance.ranger, BattleManager.Instance.ranger.animations.attackTime);
		}
		if(loser && isPlaying)
		{
			isPlaying = false;
			winner = false;
			loser = false;
			BattleManager.Instance.ranger.animations.PlayHitAniamtion();
			BattleManager.Instance.activeMonster.animations.PlayAttackAnimation();
			BattleManager.Instance.ranger.stats.TakeDamage(BattleManager.Instance.activeMonster.stats.damage.GetValue());
			//BattleManager.CheckStats();
			BattleManager.Instance.ChangeCharacter(BattleManager.Instance.ranger, BattleManager.Instance.ranger.animations.hitTime);
		}
	}
}
