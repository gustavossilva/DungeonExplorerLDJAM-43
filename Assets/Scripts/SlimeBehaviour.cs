using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour : MonoBehaviour {
	public float cooldown = 0.35f;
	private float movementCooldown;
	private GridMovement _gridMovement;

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
			
			if(this.transform.position != _startPosition)
				_gridMovement.MoveBy (Vector2.up);
			else
				_gridMovement.MoveBy (Vector2.down);
			movementCooldown = cooldown;
		}
	}

}
