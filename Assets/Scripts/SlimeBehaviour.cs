using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour : MonoBehaviour {
	public float cooldown = 0.35f;
	private float movementCooldown;
	private GridMovement _gridMovement;

	public bool upFirst = true;
	private Vector2 firstMove,lastMove;

	[SerializeField]private Vector3 _startPosition;
	// Use this for initialization
	void Start () {
		_gridMovement = GetComponent<GridMovement> ();
		movementCooldown = cooldown;
		_startPosition = this.transform.position;
		if(upFirst){
			firstMove = Vector2.up;
			lastMove = Vector2.down;
		}else{
			firstMove = Vector2.down;
			lastMove = Vector2.up;
		}

	}

	// Update is called once per frame 
	void Update () {
			movementCooldown -= Time.deltaTime;
			if(movementCooldown <= 0){ 
				if(MathUtil.IsApproximate(transform.position, _startPosition, .1f))
					_gridMovement.MoveBy (firstMove); 	 
				else
					_gridMovement.MoveBy (lastMove);
				movementCooldown = cooldown;
			}
		}

}
