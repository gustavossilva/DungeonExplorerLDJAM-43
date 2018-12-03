using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinManager : Singleton<PaladinManager> {


	public GameObject axe1,axe2,axe3,axe4,axe5,axe6;
	public float axeInitialX = 0.2123f;
	public Transform topLimit, bottomLimit;
	public bool isPlaying;
	public bool missAxe = false;
	public int remainDefenses = 6;

	protected override void Awake() {
		IsPersistentBetweenScenes = false;
		base.Awake();
	}

	protected override void OnEnable() {
		base.OnEnable();
		isPlaying = true;
		StartCoroutine(PlayGame());
	}	

	private void Update() {
		if(!isPlaying)
			return;
		if(missAxe)
		{
			StopAllCoroutines();
			axe1.SetActive(false);
			axe2.SetActive(false);
			axe3.SetActive(false);
			axe4.SetActive(false);
			axe5.SetActive(false);
			axe6.SetActive(false);
			isPlaying = false;
			missAxe = false;
			remainDefenses = 6;
			BattleManager.Instance.paladin.animations.PlayHitAniamtion();
			BattleManager.Instance.activeMonster.animations.PlayAttackAnimation();
			BattleManager.Instance.paladin.stats.TakeDamage(BattleManager.Instance.activeMonster.stats.damage.GetValue());
			InventoryManager.Instance.ChangeHealth(Character.PALADIN, BattleManager.Instance.paladin.stats.currentHealth);
			BattleManager.Instance.CheckStats();
			BattleManager.Instance.UpdatePartyHealthBars();
			BattleManager.Instance.ChangeCharacter(BattleManager.Instance.paladin, BattleManager.Instance.paladin.animations.hitTime);
			//Enemy Attack Animation
			//Receive Damage
		}
		if(remainDefenses <= 0 && isPlaying){
			axe1.SetActive(false);
			axe2.SetActive(false);
			axe3.SetActive(false);
			axe4.SetActive(false);
			axe5.SetActive(false);
			axe6.SetActive(false);
			isPlaying = false;
			missAxe = false;
			remainDefenses = 6;
			BattleManager.Instance.paladin.animations.PlayAttackAnimation();
			BattleManager.Instance.activeMonster.animations.PlayHitAniamtion();
			BattleManager.Instance.activeMonster.stats.TakeDamage(BattleManager.Instance.paladin.stats.damage.GetValue());
			BattleManager.Instance.CheckStats();
			BattleManager.Instance.UpdateEnemyHealthBar();
			BattleManager.Instance.ChangeCharacter(BattleManager.Instance.paladin, BattleManager.Instance.paladin.animations.attackTime);
			//Enemy Hit Animation
			//Do Damage
		}
	}

	IEnumerator PlayGame() {
		WaitForSeconds wait = new WaitForSeconds(0.7f);
		axe1.transform.localPosition = new Vector2(axeInitialX,Random.Range(bottomLimit.position.y,topLimit.position.y));
		axe2.transform.localPosition = new Vector2(axeInitialX,Random.Range(bottomLimit.position.y,topLimit.position.y));
		axe3.transform.localPosition = new Vector2(axeInitialX,Random.Range(bottomLimit.position.y,topLimit.position.y));
		axe4.transform.localPosition = new Vector2(axeInitialX,Random.Range(bottomLimit.position.y,topLimit.position.y));
		axe5.transform.localPosition = new Vector2(axeInitialX,Random.Range(bottomLimit.position.y,topLimit.position.y));
		axe6.transform.localPosition = new Vector2(axeInitialX,Random.Range(bottomLimit.position.y,topLimit.position.y));
		yield return new WaitForSeconds(1.4f);
		axe1.SetActive(true);
		yield return wait;
		axe2.SetActive(true);
		yield return wait;
		axe3.SetActive(true);
		yield return wait;
		axe4.SetActive(true);
		yield return wait;
		axe5.SetActive(true);
		yield return wait;
		axe6.SetActive(true);
		yield return null;
		StopCoroutine(PlayGame());
	}
}
