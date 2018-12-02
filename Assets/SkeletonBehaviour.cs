using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class SkeletonBehaviour : MonoBehaviour {
	public float cooldown = 0.35f;
	private float movementCooldown;
	private GridMovement _gridMovement;
	private string _obstacleTag;

	public bool downFirst = false;

	public AnimationReferenceAsset jumpAnimation;
	public AnimationReferenceAsset idleAnimation;

	private SkeletonAnimation skeletonSkeleton;

	// Use this for initialization
	void Start () {
		skeletonSkeleton = GetComponent<SkeletonAnimation>();
		_gridMovement = GetComponent<GridMovement> ();
		movementCooldown = cooldown;
		skeletonSkeleton.AnimationState.SetAnimation(0,idleAnimation,true); 
		_gridMovement.hasAnimation += StartJumpAnimation;
	}

	// Update is called once per frame
	void Update () {
		movementCooldown -= Time.deltaTime;
		if (movementCooldown <= 0) {
			_obstacleTag = string.Empty;
			if(!downFirst){
				MoveAndCheck(Vector2.up);
			}else{
				MoveAndCheck(Vector2.down);
			}
		}
	}
	void MoveAndCheck(Vector2 direction){
		// Perform the movement
		
		_obstacleTag = _gridMovement.MoveBy(direction);
		
		// Verifies if the players got in contact with an enemy
		if(string.IsNullOrEmpty(_obstacleTag)){
			movementCooldown = cooldown;
			
		}
		else{
			downFirst = !downFirst;
		}
	}

	void StartJumpAnimation(){
		skeletonSkeleton.AnimationState.SetAnimation(1,jumpAnimation,false);
	}
}

