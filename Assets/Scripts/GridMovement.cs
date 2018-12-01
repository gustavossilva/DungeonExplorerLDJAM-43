using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour {

	public LayerMask _layerMask;

	///<summary>
	/// Move the player by the given amount
	///</summary>
	public string MoveBy(Vector3 amount){
		Vector2 finalPos = transform.position + amount;
		RaycastHit2D hit;

		// Verifica se ha algo
		if(!HasParede(finalPos, out hit)){
			Move(finalPos);
			return string.Empty;
		}

		return hit.transform.tag;
	}

	bool HasParede(Vector3 pos, out RaycastHit2D hit){
		hit = Physics2D.Raycast(transform.position, (pos - transform.position).normalized, .5f, _layerMask);
		return hit.collider != null;
	}

	void Move(Vector2 final){
		// Do animation
		
		// Change transform position
		transform.position = final;
	}
}
