using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
public class SlimeBehaviour : MonoBehaviour {
	public float cooldown = 0.35f;
	private float movementCooldown;
	private GridMovement _gridMovement;

	public bool upFirst = true;
	private Vector2 firstMove, lastMove;

	private SkeletonAnimation slimeSkeleton;

	public AnimationReferenceAsset jump;
	public AnimationReferenceAsset idle;

	[SerializeField] private Vector3 _startPosition;
	// Use this for initialization
	private void Awake () {
		slimeSkeleton = GetComponent<SkeletonAnimation> ();
		_gridMovement = GetComponent<GridMovement> ();
	}
	void Start () {
		movementCooldown = cooldown;
		_startPosition = this.transform.position;
		if (upFirst) {
			firstMove = Vector2.up;
			lastMove = Vector2.down;
		} else {
			firstMove = Vector2.down;
			lastMove = Vector2.up;
		}
		slimeSkeleton.AnimationState.SetAnimation (0, idle, true);
		slimeSkeleton.AnimationState.Event += MoveAfterAnimation;
	}
	private void MoveAfterAnimation (Spine.TrackEntry track, Spine.Event e) {
		// When the animation passes through the energy event, starts showing the energy text
		if (string.Equals ("jump", e.Data.Name, System.StringComparison.Ordinal))
			Move ();
	}

	// Update is called once per frame 
	void Update () {
		movementCooldown -= Time.deltaTime;
		if (movementCooldown <= 0) {
			movementCooldown = cooldown;
			slimeSkeleton.AnimationState.SetAnimation (1, jump, false);
		}
	}

	void Move () {
		if (MathUtil.IsApproximate (transform.position, _startPosition, .1f))
			_gridMovement.MoveBy (firstMove);
		else
			_gridMovement.MoveBy (lastMove);
	}

}