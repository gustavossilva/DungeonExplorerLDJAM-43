using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridMovement))]
public class Player : MonoBehaviour {

	public float cooldown = 0.5f;
	private float movementCooldown;
	private GridMovement _gridMovement;

	// Use this for initialization
	void Start () {
		_gridMovement = GetComponent<GridMovement>();
		movementCooldown = cooldown;
	}
	
	// Update is called once per frame
	void Update () {
		movementCooldown -= Time.deltaTime;
		if(movementCooldown <= 0){
			if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
				movementCooldown = cooldown;
				_gridMovement.MoveBy(Vector2.left);
				return;
			}

			if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
				movementCooldown = cooldown;
				_gridMovement.MoveBy(Vector2.right);
				return;
			}

			if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)){
				movementCooldown = cooldown;
				_gridMovement.MoveBy(Vector2.up);
				return;
			}

			if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)){
				movementCooldown = cooldown;
				_gridMovement.MoveBy(Vector2.down);
				return;
			}
		}
	}
}
