using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WizardManager : Singleton<WizardManager> {

	public TextMeshProUGUI timer;
	public int actualTime;
	public int maxTime;
	public bool isPlaying = false;
	public bool winner, loser;

	public AudioClip attackClip, hitClip;

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
			GetComponent<AudioSource>().PlayOneShot(attackClip, .4f);

			isPlaying = false;			
			winner = false;
			loser = false;
			BattleManager.Instance.wizard.animations.PlayAttackAnimation();
			BattleManager.Instance.activeMonster.animations.PlayHitAniamtion();
			BattleManager.Instance.activeMonster.stats.TakeDamage(BattleManager.Instance.wizard.stats.damage.GetValue());
			BattleManager.Instance.CheckStats();
			BattleManager.Instance.UpdateEnemyHealthBar();
			BattleManager.Instance.ChangeCharacter(BattleManager.Instance.wizard, BattleManager.Instance.wizard.animations.attackTime);
			StopAllCoroutines();
			timer.gameObject.SetActive(false);
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
		// GetComponent<AudioSource>().PlayOneShot(hitClip, .4f);

			isPlaying = false;
			winner = false;
			loser = false;
			BattleManager.Instance.wizard.animations.PlayHitAniamtion();
			BattleManager.Instance.activeMonster.animations.PlayAttackAnimation();
			BattleManager.Instance.wizard.stats.TakeDamage(BattleManager.Instance.activeMonster.stats.damage.GetValue());
			InventoryManager.Instance.ChangeHealth(Character.WIZARD, BattleManager.Instance.wizard.stats.currentHealth);
			BattleManager.Instance.CheckStats();
			BattleManager.Instance.UpdatePartyHealthBars();
			BattleManager.Instance.ChangeCharacter(BattleManager.Instance.wizard, BattleManager.Instance.wizard.animations.hitTime);
			StopAllCoroutines();
			timer.gameObject.SetActive(false);
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
