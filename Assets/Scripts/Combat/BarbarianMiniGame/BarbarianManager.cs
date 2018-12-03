using UnityEngine;

public class BarbarianManager : Singleton<BarbarianManager> {

	public bool isPlaying = false;
	private bool canAttack = false;


	protected override void Awake() {
		IsPersistentBetweenScenes = false;
		base.Awake();
	}

	protected override void OnEnable() {
		base.OnEnable();
		isPlaying = true;
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
				BattleManager.Instance.ChangeCharacter(BattleManager.Instance.barbarian, BattleManager.Instance.barbarian.animations.attackTime);
			}
			else
			{
				BattleManager.Instance.barbarian.animations.PlayHitAniamtion();
				BattleManager.Instance.activeMonster.animations.PlayAttackAnimation();
				BattleManager.Instance.barbarian.stats.TakeDamage(BattleManager.Instance.activeMonster.stats.damage.GetValue());
				BattleManager.Instance.CheckStats();
				BattleManager.Instance.ChangeCharacter(BattleManager.Instance.barbarian, BattleManager.Instance.barbarian.animations.hitTime);
			}
				isPlaying = false;
				canAttack = false;
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
