﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinManager : Singleton<PaladinManager> {


	public GameObject axe1,axe2,axe3,axe4,axe5,axe6;
	public Vector2 axeInitPos = new Vector2(0.2123f,0);
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
		axe1.transform.position = axeInitPos;
		axe2.transform.position = axeInitPos;
		axe3.transform.position = axeInitPos;
		axe4.transform.position = axeInitPos;
		axe5.transform.position = axeInitPos;
		axe6.transform.position = axeInitPos;
		isPlaying = true;
	}

	private void Start() {
		StartCoroutine(PlayGame());
	}

	private void Update() {
		if(!isPlaying)
			return;
		if(missAxe)
		{
			StopCoroutine(PlayGame());
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
			BattleManager.Instance.ChangeCharacter(BattleManager.Instance.paladin, BattleManager.Instance.paladin.animations.attackTime);
			//Enemy Hit Animation
			//Do Damage
		}
	}

	IEnumerator PlayGame() {
		WaitForSeconds wait = new WaitForSeconds(0.7f);
		axe1.transform.position = new Vector2(axe1.transform.position.x,Random.Range(bottomLimit.position.y,topLimit.position.y));
		axe2.transform.position = new Vector2(axe2.transform.position.x,Random.Range(bottomLimit.position.y,topLimit.position.y));
		axe3.transform.position = new Vector2(axe3.transform.position.x,Random.Range(bottomLimit.position.y,topLimit.position.y));
		axe4.transform.position = new Vector2(axe4.transform.position.x,Random.Range(bottomLimit.position.y,topLimit.position.y));
		axe5.transform.position = new Vector2(axe5.transform.position.x,Random.Range(bottomLimit.position.y,topLimit.position.y));
		axe6.transform.position = new Vector2(axe6.transform.position.x,Random.Range(bottomLimit.position.y,topLimit.position.y));
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
