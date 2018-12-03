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
	void Start () {
		target = rightLimit;
	}

	private void OnEnable() {
		mouseUP = false;
		onTarget = false;
		transform.position = leftLimit.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(!WizardManager.Instance.isPlaying)
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
				WizardManager.Instance.winner = true;
				Debug.Log("Venceu");
			}
			else
			{
				WizardManager.Instance.loser = true;
				Debug.Log("Perdeu");
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
