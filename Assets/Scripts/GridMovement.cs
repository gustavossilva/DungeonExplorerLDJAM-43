using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour {

	public void MoveBy(Vector3 portion){
		Vector2 finalPos = transform.position + portion;

		// Verifica se ha algo
		if(!HaParede(finalPos)){
			Move(finalPos);
		}
	}

	bool HaParede(Vector2 pos){
		return false;
	}

	void Move(Vector2 final){
		// Do animation
		
		// Change transform position
		transform.position = final;
	}
}
