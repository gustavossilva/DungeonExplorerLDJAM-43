using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
public class TrollBehaviour : MonoBehaviour {
	public List<MovementType> trollMoves;
	public List<MovementType> trollMoves2;
	public List<MovementType> usedMoves;
	public Collider2D playerNearby;
	public Transform playerPosition;

	private GridMovement _gridMoviment;

	private int moveIndex = 0;

	private bool _isMoving = false;
	private bool backToHome = false;
	public bool hasTwoWays = false;
	public float cooldown = 1f;
	public float timeToNextMovement;

	public AnimationReferenceAsset idle;
	public AnimationReferenceAsset jump;

	private SkeletonAnimation trollSkeleton;
	// Use this for initialization
	private void Awake () {
		trollSkeleton = GetComponent<SkeletonAnimation> ();
		_gridMoviment = GetComponent<GridMovement> ();
	}
	void Start () {
		trollSkeleton.AnimationState.SetAnimation (0, idle, true);
		timeToNextMovement = cooldown;
		trollSkeleton.AnimationState.Event += MoveAfterAnimation;
	}

	private void MoveAfterAnimation (Spine.TrackEntry track, Spine.Event e) {
		// When the animation passes through the energy event, starts showing the energy text
		if (string.Equals ("Jump", e.Data.Name, System.StringComparison.Ordinal))
			Move ();
	}
	// Update is called once per frame
	void Update () {
		if (playerNearby.OverlapPoint (playerPosition.position) && !_isMoving && !backToHome) {
			_isMoving = true;
			moveIndex = 0;
			if(hasTwoWays){
				if(Random.Range(0,100) >= 50){
					usedMoves = trollMoves2;
				}else{
					usedMoves = trollMoves;
				}
			}else{
				usedMoves = trollMoves;
			}
		}
		if (_isMoving) {
			timeToNextMovement -= Time.deltaTime;
			if (timeToNextMovement <= 0) {
				trollSkeleton.AnimationState.SetAnimation (1, jump, false);
				timeToNextMovement = cooldown; 
			}
		}
	}

	void Move () {
		if (moveIndex >= usedMoves.Count && !backToHome && _isMoving) {
			backToHome = true;
			moveIndex = usedMoves.Count - 1;
		}
		if (moveIndex < 0 && backToHome && _isMoving) {
			_isMoving = false;	 
			backToHome = false;
			
		}
		if(_isMoving){
			switch (usedMoves[moveIndex]) {
				case MovementType.UP:
					if (backToHome)
						_gridMoviment.MoveBy (Vector3.down);
					else
						_gridMoviment.MoveBy (Vector3.up);
					break;
				case MovementType.DOWN:
					if (backToHome)
						_gridMoviment.MoveBy (Vector3.up);
					else
						_gridMoviment.MoveBy (Vector3.down);
					break;
				case MovementType.LEFT:
					if (backToHome)
						_gridMoviment.MoveBy (Vector3.right);
					else
						_gridMoviment.MoveBy (Vector3.left);
					break;
				case MovementType.RIGHT:
					if (backToHome)
						_gridMoviment.MoveBy (Vector3.left);
					else
						_gridMoviment.MoveBy (Vector3.right);
					break;
			} 
		}
		if (!backToHome)
			moveIndex++;
		else
			moveIndex--;
	}
}
public enum MovementType {
	UP,
	LEFT,
	DOWN,
	RIGHT

}