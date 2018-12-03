using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
public class goblinBehaviour : MonoBehaviour {

	[SerializeField] private Transform topLeft, bottomLeft, topRight, bottomRight;
	[SerializeField] private StartPosition startPosition;
	public float cooldown = 0.35f;
	private float movementCooldown;
	private GridMovement _gridMovement;
	private SkeletonAnimation goblinSkeleton;

	[SerializeField] private List<StartPosition> nextPosition = new List<StartPosition> ();
	private int positionIndex = 0;

	public AnimationReferenceAsset jump;
	public AnimationReferenceAsset idle;
	public string id;

	float scaleX;

	int jumpTimes = 0;
	private int _startPosition;
	private void Awake () {
		goblinSkeleton = GetComponent<SkeletonAnimation> ();
		_gridMovement = GetComponent<GridMovement> ();
		scaleX = transform.localScale.x;
	}

	// Use this for initialization
	void Start () {
		if (!MonsterManager.Instance.monstersInGame.ContainsKey (id))
			MonsterManager.Instance.monstersInGame.Add (id, true);
		else {
			gameObject.SetActive (MonsterManager.Instance.monstersInGame[id]);
		}
		//Inicia o goblin com uma das posições finais possíveis(isso irá diferenciar cada possível goblin)
		switch (startPosition) {
			case StartPosition.TOP_LEFT: //horário
				this.transform.position = topLeft.position;
				nextPosition.Add (StartPosition.TOP_RIGHT);
				nextPosition.Add (StartPosition.BOTTOM_RIGHT);
				nextPosition.Add (StartPosition.BOTTOM_LEFT);
				nextPosition.Add (StartPosition.TOP_LEFT);
				break;
			case StartPosition.TOP_RIGHT: //antihorario
				this.transform.position = topRight.position;
				nextPosition.Add (StartPosition.TOP_LEFT);
				nextPosition.Add (StartPosition.BOTTOM_LEFT);
				nextPosition.Add (StartPosition.BOTTOM_RIGHT);
				nextPosition.Add (StartPosition.TOP_RIGHT);
				break;
			default:
				this.transform.position = topLeft.position;
				break;
		}
		movementCooldown = cooldown;
		Player.Instance.moved += Move;
		goblinSkeleton.AnimationState.SetAnimation (0, idle, true);
		goblinSkeleton.AnimationState.Event += MoveAfterAnimation;
	}

	private void MoveAfterAnimation (Spine.TrackEntry track, Spine.Event e) {
		// When the animation passes through the energy event, starts showing the energy text
		if (string.Equals ("Jump", e.Data.Name, System.StringComparison.Ordinal)) {
			GoblinMovement (startPosition);
		}
	}

	// Update is called once per frame
	void Move () {
		goblinSkeleton.AnimationState.SetAnimation (1, jump, false);
	}

	private void GoblinMovement (StartPosition movementType) {
		if (jumpTimes >= 2) {
			jumpTimes = 0;
			positionIndex++;
		}
		if (positionIndex >= nextPosition.Count)
			positionIndex = 0;
		switch (nextPosition[positionIndex]) {
			case StartPosition.TOP_LEFT:
				if (movementType == StartPosition.TOP_LEFT)
				{
					_gridMovement.MoveBy (Vector3.up);	
				}
				else{
					transform.localScale = new Vector3(-scaleX, transform.localScale.y, transform.localScale.z);
					_gridMovement.MoveBy (Vector3.left);
				}
				jumpTimes++;
				break;
			case StartPosition.TOP_RIGHT:
				if (movementType == StartPosition.TOP_LEFT){
					transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
					_gridMovement.MoveBy (Vector3.right);
				}
				else{
					_gridMovement.MoveBy (Vector3.up);
				}
				jumpTimes++;
				break;
			case StartPosition.BOTTOM_LEFT:
				if (movementType == StartPosition.TOP_LEFT)
				{	
					transform.localScale = new Vector3(-scaleX, transform.localScale.y, transform.localScale.z);
					_gridMovement.MoveBy (Vector3.left);
				}else{
					_gridMovement.MoveBy (Vector3.down);
				}
				jumpTimes++;
				break;
			case StartPosition.BOTTOM_RIGHT:
				if (movementType == StartPosition.TOP_LEFT)
				{	
					transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
					_gridMovement.MoveBy (Vector3.down);
				}else{
					transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
					_gridMovement.MoveBy (Vector3.right);
				}
				jumpTimes++;
				break;
		}
	}
	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			MonsterManager.Instance.monsterName = "Goburin";
			MonsterManager.Instance.StartBattle (other.transform,id);
		}
	}
}

public enum StartPosition {
	TOP_LEFT,
	TOP_RIGHT,
	BOTTOM_LEFT,
	BOTTOM_RIGHT,
}