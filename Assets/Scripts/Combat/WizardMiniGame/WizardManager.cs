using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardManager : Singleton<WizardManager> {

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
			BattleManager.Instance.wizard.animations.PlayAttackAnimation();
			BattleManager.Instance.activeMonster.animations.PlayHitAniamtion();
			BattleManager.Instance.activeMonster.stats.TakeDamage(BattleManager.Instance.wizard.stats.damage.GetValue());
			BattleManager.Instance.CheckStats();
			BattleManager.Instance.ChangeCharacter(BattleManager.Instance.wizard, BattleManager.Instance.wizard.animations.attackTime);
		}
		if(loser && isPlaying)
		{
			isPlaying = false;
			winner = false;
			loser = false;
			BattleManager.Instance.wizard.animations.PlayHitAniamtion();
			BattleManager.Instance.activeMonster.animations.PlayAttackAnimation();
			BattleManager.Instance.wizard.stats.TakeDamage(BattleManager.Instance.activeMonster.stats.damage.GetValue());
			BattleManager.Instance.CheckStats();
			BattleManager.Instance.ChangeCharacter(BattleManager.Instance.wizard, BattleManager.Instance.wizard.animations.hitTime);
		}
	}
}
