using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehaviour : MonoBehaviour {
	public float cooldown = 0.35f;
	private float movementCooldown;
	private GridMovement _gridMovement;
	private string _obstacleTag;

	public bool downFirst = false;

	// Use this for initialization
	void Start () {
		_gridMovement = GetComponent<GridMovement> ();
		movementCooldown = cooldown;
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
}

