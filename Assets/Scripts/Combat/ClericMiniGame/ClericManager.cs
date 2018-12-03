using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClericManager : Singleton<ClericManager> {

	public TextMeshProUGUI timer;
	public int actualTime;
	public int maxTime;
	public Slider healthBar;
	public bool isPlaying = false;

	public AudioClip attackClip, hitClip;


	protected override void Awake(){
		IsPersistentBetweenScenes = false;
		base.Awake();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		isPlaying = true;
		healthBar.gameObject.SetActive(true);
		timer.text = maxTime.ToString();
		actualTime = maxTime;
		timer.gameObject.SetActive(true);
		StartCoroutine(Timer());
	}

	void Update () {
		if(!isPlaying)
		{
			return;
		}
		if(healthBar.value >= 99.5 && isPlaying)
		{
			GetComponent<AudioSource>().PlayOneShot(attackClip, .4f);
			isPlaying = false;
			BattleManager.Instance.cleric.animations.PlayAttackAnimation();
			PartyManager.Instance.HealParty(BattleManager.Instance.cleric.stats.damage.GetValue());
			BattleManager.Instance.UpdatePartyHealthBars();
			BattleManager.Instance.ChangeCharacter(BattleManager.Instance.cleric, BattleManager.Instance.cleric.animations.attackTime);
			healthBar.value = 50;
			healthBar.gameObject.SetActive(false);
			timer.gameObject.SetActive(false);
			StopAllCoroutines();
		}
		if(healthBar.value <= 0.2 && isPlaying)
		{
			Loser();
			StopAllCoroutines();
		}
		if(actualTime == 0)
		{
			Loser();
			StopAllCoroutines();
		}
	}

	public void Loser()
	{
		// GetComponent<AudioSource>().PlayOneShot(hitClip, .4f);

			isPlaying = false;
			BattleManager.Instance.cleric.animations.PlayHitAniamtion();
			BattleManager.Instance.cleric.stats.TakeDamage(BattleManager.Instance.activeMonster.stats.damage.GetValue());
			InventoryManager.Instance.ChangeHealth(Character.CLERIC, BattleManager.Instance.cleric.stats.currentHealth);
			BattleManager.Instance.activeMonster.animations.PlayAttackAnimation();
			BattleManager.Instance.ChangeCharacter(BattleManager.Instance.cleric, BattleManager.Instance.cleric.animations.hitTime);
			BattleManager.Instance.CheckStats();
			BattleManager.Instance.UpdatePartyHealthBars();
			healthBar.value = 50;
			healthBar.gameObject.SetActive(false);
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

	public void AddHealthBar(float qtdToAdd)
	{
		healthBar.value += qtdToAdd;
	}

	public void RemoveHealthBar(float qtdToRemove)
	{
		healthBar.value -=qtdToRemove;
	}

}
