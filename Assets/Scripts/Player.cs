using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridMovement))]
public class Player : MonoBehaviour {

	public float cooldown = 0.35f;
	private float movementCooldown;
	private GridMovement _gridMovement;
	private string _obstacleTag;

	// Use this for initialization
	void Start () {
		_gridMovement = GetComponent<GridMovement>();
		movementCooldown = cooldown;
	}
	
	// Update is called once per frame
	void Update () {
		movementCooldown -= Time.deltaTime;
		
		// Only resets cooldown if the player could successfuly perform the movement
		if(movementCooldown <= 0){
			_obstacleTag = string.Empty;

			// LEFT
			if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
				MoveAndCheck(Vector2.left);
				return;
			}

			// RIGHT
			if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
				MoveAndCheck(Vector2.right);
				return;
			}

			// UP
			if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)){
				MoveAndCheck(Vector2.up);
				return;
			}

			// DOWN
			if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)){
				MoveAndCheck(Vector2.down);
				return;
			}
		}
	}

	void MoveAndCheck(Vector2 direction){
		// Perform the movement
		_obstacleTag = _gridMovement.MoveBy(direction);
		
		// Verifies if the players got in contact with an enemy
		if(string.IsNullOrEmpty(_obstacleTag))
			movementCooldown = cooldown;
		else if(string.Equals(_obstacleTag, "Enemy")){
			// Starts new scene here!
			Debug.Log("Fight Scene");
		}
	}
}
