using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WizardMovement : MonoBehaviour {



	public Transform leftLimit, rightLimit;
	public float speed = 5f;
	public Transform target;
	public bool mouseUP = false;
	public bool onTarget = false;
	public bool gameOver = false;
	void Start () {
		target = rightLimit;
	}
	
	// Update is called once per frame
	void Update () {
		if(gameOver)
			return;
		if(transform.position.x == rightLimit.position.x)
		{
			target = leftLimit;
		}
		if(transform.position.x == leftLimit.position.x)
		{
			target = rightLimit;
		}
		if(Input.GetMouseButton(0))
		{
			transform.position = Vector2.MoveTowards(transform.position, target.position, speed* Time.deltaTime);
		}
		if(Input.GetMouseButtonUp(0))
		{
			mouseUP = true;
		}
		if(mouseUP)
		{
			if(onTarget)
			{
				Debug.Log("Venceu");
				gameOver = true;
			}
			else
			{
				Debug.Log("Perdeu");
				gameOver = true;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("GameController"))
		{
			onTarget = true;
		}
		else if(other.CompareTag("Enemy"))
		{
			onTarget = false;
		}
	}

	private void OnTriggerStay2D(Collider2D other) {
		if(other.CompareTag("GameController") && mouseUP)
		{
			onTarget = true;
		}
		else if(other.CompareTag("Enemy"))
		{
			onTarget = false;
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if(other.CompareTag("GameController"))
		{
			onTarget = false;
		}
		else if(other.CompareTag("Enemy"))
		{
			onTarget = true;
		}
	}
}
