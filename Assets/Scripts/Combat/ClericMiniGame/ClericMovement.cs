using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClericMovement : MonoBehaviour {
	public Transform left,right;
	public float pullForce;
	public float clickForce;

	public string direction;

	private bool isHealing;

	private void Start() {
		direction = "Right";
	}

	private void Update() {
		switch (direction)
		{
			case "Right":
				transform.position = Vector2.MoveTowards(transform.position, right.position, pullForce * Time.deltaTime);
				break;
			case "Left":
				transform.position = Vector2.MoveTowards(transform.position, left.position, pullForce * Time.deltaTime);
				break;
			default:
				Debug.Log("Bug");
				break;
		}
		if(Input.GetMouseButtonDown(1))
		{
			transform.position = Vector2.MoveTowards(transform.position, right.position, clickForce * Time.deltaTime);	
		}
		if(Input.GetMouseButtonDown(0))
		{
			transform.position = Vector2.MoveTowards(transform.position, left.position, clickForce * Time.deltaTime);
		}
		Debug.Log(transform.position.x - left.position.x);
		if((transform.position.x - left.position.x) < 3.9){
			direction = "Left";
		}
		else{
			direction = "Right";
		}
		if(isHealing)
		{
			ClericManager.Instance.AddHealthBar(10*Time.deltaTime);
		}
		else
		{
			ClericManager.Instance.RemoveHealthBar(6*Time.deltaTime);
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("GameController"))
		{
			isHealing = true;
		}
	}
	
	
	private void OnTriggerStay2D(Collider2D other) {
		if(other.CompareTag("GameController"))
		{
			isHealing = true;
		}		
	}

	private void OnTriggerExit2D(Collider2D other) 
	{
		if(other.CompareTag("GameController"))
			isHealing = false;	
	}
}
