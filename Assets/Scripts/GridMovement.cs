using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour {

	public LayerMask _layerMask;
	public float movementStep = 1f;

	public event System.Action hasAnimation;
	///<summary>
	/// Move the player by the given amount
	///</summary>
	public string MoveBy(Vector3 amount){
		// movement step is the hop length
		Vector2 finalPos = transform.position + (amount * movementStep);
		RaycastHit2D hit;

		// Verifica se ha algo
		if(!HasParede(finalPos, out hit)){
			Move(finalPos);
			return string.Empty;
		}

		return hit.transform.tag;
	}

	bool HasParede(Vector3 pos, out RaycastHit2D hit){
		hit = Physics2D.Raycast(transform.position, (pos - transform.position).normalized, movementStep + 0.01f, _layerMask);
		return hit.collider != null;
	}

	void Move(Vector2 final){
		// TODO: Do animation
		if(hasAnimation != null)
			hasAnimation(); 
		// Change transform position
		transform.position = final;
	}
}
