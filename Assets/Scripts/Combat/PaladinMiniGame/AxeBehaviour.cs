using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeBehaviour : MonoBehaviour {
	public const string PLAYER_TAG = "GameController";
	public Transform rightLimit;
	public Vector2 targetPosition;
	public float speed = 15f;
	public bool doDamage = true;
	private void OnEnable() {
		targetPosition = new Vector2(rightLimit.position.x,transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = Vector2.MoveTowards(transform.position,targetPosition, speed * Time.deltaTime);
		if(transform.position.x >= rightLimit.position.x)
		{
			PaladinManager.Instance.missAxe = true;
			gameObject.SetActive(false);
		}
		transform.Rotate(new Vector3(0,0,-200*Time.deltaTime));
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag(PLAYER_TAG) && doDamage)
		{
			doDamage = false;
			PaladinManager.Instance.remainDefenses--;
			gameObject.SetActive(false);
		}
	}
}
