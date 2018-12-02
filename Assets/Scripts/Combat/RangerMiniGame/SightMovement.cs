using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightMovement : MonoBehaviour {

	public Transform posController;
	private Vector2 initialPos;
	private bool generateNewPos = false;
	private bool canShoot = false;
	private Vector2 newPos;
	void Start () {
		initialPos = transform.position;
	}
	
		void Update () {
		if(!generateNewPos)
		{
			newPos = Random.insideUnitCircle * 1.17f + initialPos;
			posController.position = newPos;
			generateNewPos = true;
		}
		transform.position = Vector2.MoveTowards(transform.position,newPos, 2 * Time.deltaTime);
		if(Input.GetMouseButtonDown(0))
		{
			if(canShoot)
			{
				Debug.Log("hit enemy");
			}
			else
			{
				Debug.Log("hit player");
			}
		}

	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("GameController"))
		{
			generateNewPos = false;
		}
		if(other.CompareTag("Enemy"))
		{
			canShoot = true;
		}
	}
	private void OnTriggerStay2D(Collider2D other) {
		if(other.CompareTag("GameController"))
		{
			generateNewPos = false;
		}		
	}

	private void OnTriggerExit2D(Collider2D other) {
		if(other.CompareTag("Enemy"))
		{
			canShoot = false;
		}
	}
}
