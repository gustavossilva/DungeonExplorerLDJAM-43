using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

[RequireComponent(typeof(SkeletonAnimation))]
public class BattleMonsterAnimation : MonoBehaviour {

	public AnimationReferenceAsset iddle, jump, attack, hit, death;
	public float iddleTime, jumpTime, attackTime, hitTime, deathTime;
	private SkeletonAnimation _skeletonAnimation;
	public bool testJump, testAttack, testHit, testDeath;
	void Start () {
		_skeletonAnimation = GetComponent<SkeletonAnimation>();
		_skeletonAnimation.AnimationState.SetAnimation(0,iddle,true);
	}

	//Debug Only
	private void Update() {
		if(testJump){
			PlayJumpAnimation();
		}
		if(testAttack){
			PlayAttackAnimation();
		}
		if(testHit){
			PlayHitAniamtion();
		}
		if(testDeath){
			PlayDeathAnimation();
		}
	}

	public void PlayJumpAnimation(){
		_skeletonAnimation.AnimationState.SetAnimation(1,jump,false);
		testJump = false;
	}

	public void PlayDeathAnimation(){
		_skeletonAnimation.AnimationState.SetAnimation(0,death,false);
		testDeath = false;
	}

	public void PlayAttackAnimation(){
		_skeletonAnimation.AnimationState.SetAnimation(0,attack,false);
		_skeletonAnimation.AnimationState.AddEmptyAnimation(0,0.2f,attackTime);
		_skeletonAnimation.AnimationState.AddAnimation(0,iddle,true,0.1f);
		testAttack = false;
	}

	public void PlayHitAniamtion(){
		_skeletonAnimation.AnimationState.SetAnimation(0,hit,false);
		_skeletonAnimation.AnimationState.AddEmptyAnimation(0,0.2f,hitTime);
		_skeletonAnimation.AnimationState.AddAnimation(0,iddle,true,0.1f);
		testHit = false;
	}


	
}
