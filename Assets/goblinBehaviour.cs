using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class goblinBehaviour : MonoBehaviour {

	[SerializeField] private Transform topLeft, bottomLeft, topRight, bottomRight;
	[SerializeField] private StartPosition startPosition;
	public float cooldown = 0.35f;
	private float movementCooldown;
	private GridMovement _gridMovement;

	[SerializeField] private List<StartPosition> nextPosition = new List<StartPosition> ();
	private int positionIndex = 0;

	int jumpTimes = 0;
	private int _startPosition;
	// Use this for initialization
	void Start () {
		_gridMovement = GetComponent<GridMovement> ();
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
	}

	// Update is called once per frame
	void Update () {
		movementCooldown -= Time.deltaTime;
		if (movementCooldown <= 0) {
			GoblinMovement (startPosition);
			movementCooldown = cooldown;
		}
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
					_gridMovement.MoveBy (Vector3.up);
				else
					_gridMovement.MoveBy (Vector3.left);
				jumpTimes++;
				break;
			case StartPosition.TOP_RIGHT:
				if (movementType == StartPosition.TOP_LEFT)
					_gridMovement.MoveBy (Vector3.right);
				else
					_gridMovement.MoveBy (Vector3.up);
				jumpTimes++;
				break;
			case StartPosition.BOTTOM_LEFT:
				if (movementType == StartPosition.TOP_LEFT)
					_gridMovement.MoveBy (Vector3.left);
				else
					_gridMovement.MoveBy (Vector3.down);
				jumpTimes++;
				break;
			case StartPosition.BOTTOM_RIGHT:
				if (movementType == StartPosition.TOP_LEFT)
					_gridMovement.MoveBy (Vector3.down);
				else
					_gridMovement.MoveBy (Vector3.right);
				jumpTimes++;
				break;
		}
	}

}

public enum StartPosition {
	TOP_LEFT,
	TOP_RIGHT,
	BOTTOM_LEFT,
	BOTTOM_RIGHT,
}