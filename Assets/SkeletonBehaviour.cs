using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
public class SkeletonBehaviour : MonoBehaviour {
	public float cooldown = 0.35f;
	private float movementCooldown;
	private GridMovement _gridMovement;
	private string _obstacleTag;

	public bool downFirst = false;

	public AnimationReferenceAsset jumpAnimation;
	public AnimationReferenceAsset idleAnimation;

	private SkeletonAnimation skeletonSkeleton;

	public string id;

	// Use this for initialization
	void Start () {
		if (!MonsterManager.Instance.monstersInGame.ContainsKey (id))
			MonsterManager.Instance.monstersInGame.Add (id, true);
		else {
			gameObject.SetActive (MonsterManager.Instance.monstersInGame[id]);
		}
		skeletonSkeleton = GetComponent<SkeletonAnimation> ();
		_gridMovement = GetComponent<GridMovement> ();
		movementCooldown = cooldown;
		skeletonSkeleton.AnimationState.SetAnimation (0, idleAnimation, true);
		Player.Instance.moved += MoveWhenPlayerMove;
		skeletonSkeleton.AnimationState.Event += MoveAfterAnimation;
	}

	private void MoveAfterAnimation (Spine.TrackEntry track, Spine.Event e) {
		// When the animation passes through the energy event, starts showing the energy text
		if (string.Equals ("Jump", e.Data.Name, System.StringComparison.Ordinal))
			Move ();
	}

	private void MoveWhenPlayerMove () {
		skeletonSkeleton.AnimationState.SetAnimation (1, jumpAnimation, false);
	}

	// Update is called once per frame
	void Move () {
		_obstacleTag = string.Empty;
		if (!downFirst) {
			MoveAndCheck (Vector2.up);
		} else {
			MoveAndCheck (Vector2.down);
		}
	}
	void MoveAndCheck (Vector2 direction) {
		// Perform the movement
		_obstacleTag = _gridMovement.MoveBy (direction);

		// Verifies if the players got in contact with an enemy
		if (string.IsNullOrEmpty (_obstacleTag)) {
			movementCooldown = cooldown;

		} else {
			downFirst = !downFirst;
		}
	}
	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			MonsterManager.Instance.monsterName = "Skeleton";
			MonsterManager.Instance.StartBattle (other.transform,id);
		}
	}
}