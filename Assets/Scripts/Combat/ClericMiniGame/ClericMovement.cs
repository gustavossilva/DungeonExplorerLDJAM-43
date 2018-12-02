using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClericMovement : MonoBehaviour {

	public Transform finalPos;
	private Rigidbody2D rbody;
	public float speed = 3;
	public float jumpForce = 2;

	private bool isGrounded = true;
	public Transform feetPos;
	public float checkRadius;
	public LayerMask whatIsGrounded;
	private bool gameOver = false;

	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x >= finalPos.position.x)
		{
			Debug.Log("Healing");
			return;
		}
		if(gameOver)
		{
			return;
		}
		isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGrounded);
		rbody.velocity = new Vector2(1 * speed,rbody.velocity.y);
		if(isGrounded && Input.GetMouseButtonDown(0))
		{
			rbody.velocity = Vector2.up * jumpForce;
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Enemy"))
		{
			speed = 0;
			gameOver = true;
			Debug.Log("Hit player");
			gameObject.SetActive(false);
		}
	}
}
