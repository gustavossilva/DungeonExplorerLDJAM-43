using System.Collections;
using UnityEngine;
using TMPro;

public class BarbarianManager : Singleton<BarbarianManager> {

	public TextMeshProUGUI timer;
	public int actualTime;
	public int maxTime;
	public bool isPlaying = false;
	private bool canAttack = false;


	protected override void Awake() {
		IsPersistentBetweenScenes = false;
		base.Awake();
	}

	protected override void OnEnable() {
		base.OnEnable();
		isPlaying = true;
		timer.text = maxTime.ToString();
		actualTime = maxTime;
		timer.gameObject.SetActive(true);
		StartCoroutine(Timer());

	}
	
	void Update () {
		if(!isPlaying)
			return;
		if(Input.GetMouseButtonDown(0))
		{
			if(canAttack)
			{
				BattleManager.Instance.barbarian.animations.PlayAttackAnimation();
				BattleManager.Instance.activeMonster.animations.PlayHitAniamtion();
				BattleManager.Instance.activeMonster.stats.TakeDamage(BattleManager.Instance.barbarian.stats.damage.GetValue());
				BattleManager.Instance.CheckStats();
				BattleManager.Instance.UpdateEnemyHealthBar();
				BattleManager.Instance.ChangeCharacter(BattleManager.Instance.barbarian, BattleManager.Instance.barbarian.animations.attackTime);
			}
			else
			{
				Loser();
			}
				timer.gameObject.SetActive(false);
				StopAllCoroutines();
				isPlaying = false;
				canAttack = false;
		}
		if(actualTime == 0)
		{
			Loser();
			isPlaying = false;
			canAttack = false;
			timer.gameObject.SetActive(false);
			StopAllCoroutines();
		}
	}

	public void Loser()
	{
		BattleManager.Instance.barbarian.animations.PlayHitAniamtion();
		BattleManager.Instance.activeMonster.animations.PlayAttackAnimation();
		BattleManager.Instance.barbarian.stats.TakeDamage(BattleManager.Instance.activeMonster.stats.damage.GetValue());
		InventoryManager.Instance.ChangeHealth(Character.BARBARIAN, BattleManager.Instance.barbarian.stats.currentHealth);
		BattleManager.Instance.CheckStats();
		BattleManager.Instance.UpdatePartyHealthBars();
		BattleManager.Instance.ChangeCharacter(BattleManager.Instance.barbarian, BattleManager.Instance.barbarian.animations.hitTime);
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


	private void OnTriggerEnter2D(Collider2D other) 
	{
		if(!isPlaying)
			return;
		if(!canAttack)
		{
			canAttack = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other) 
	{
		if(!isPlaying)
			return;
		if(canAttack)
		{
			canAttack = !canAttack;
		}

	}
}
