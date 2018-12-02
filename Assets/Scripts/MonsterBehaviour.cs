using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviour : MonoBehaviour {
	public float cooldown = 0.35f;
	private float movementCooldown;
	private GridMovement _gridMovement;
	[SerializeField] private MonsterType _monsterType;

	private Vector3 _startPosition;
	// Use this for initialization
	void Start () {
		_gridMovement = GetComponent<GridMovement> ();
		movementCooldown = cooldown;
		_startPosition = this.transform.position;
	}

	// Update is called once per frame
	void Update () {
		movementCooldown -= Time.deltaTime;
		if (movementCooldown <= 0) {
			switch (_monsterType) {
				case MonsterType.SLIME:
					//Dispara animação de movimento
					if(this.transform.position != _startPosition)
						_gridMovement.MoveBy (Vector2.up);
					else
						_gridMovement.MoveBy (Vector2.down);
					break;
				case MonsterType.GOBLIN:
					break;
				case MonsterType.SKELETON:
					break;
				case MonsterType.TROLL:
					break;
				default:
					break;
			}
			movementCooldown = cooldown;
		}
	}

}

public enum MonsterType {
	SLIME,
	GOBLIN,
	TROLL,
	SKELETON,
}