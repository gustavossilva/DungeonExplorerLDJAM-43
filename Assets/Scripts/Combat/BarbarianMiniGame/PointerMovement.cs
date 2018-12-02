using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerMovement : MonoBehaviour {


	public Transform leftLimit, rightLimit;
	public float speed = 5f;
	public Transform target;
	void Start () {
		target = rightLimit;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x == rightLimit.position.x)
		{
			target = leftLimit;
		}
		if(transform.position.x == leftLimit.position.x)
		{
			target = rightLimit;
		}
		transform.position = Vector2.MoveTowards(transform.position, target.position, speed* Time.deltaTime);
	}
}
