using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour {

	public LayerMask _layerMask;
	public float movementStep = 1f;
	public bool coroutineIsRunning = false;

	public event System.Action hasAnimation;
	///<summary>
	/// Move the player by the given amount
	///</summary>
	public string MoveBy (Vector3 amount) {
		// movement step is the hop length
		Vector2 finalPos = transform.position + (amount * movementStep);
		RaycastHit2D hit;

		// Verifica se ha algo
		if (!HasParede (finalPos, out hit)) {
			if (!coroutineIsRunning){
				StartCoroutine (Move (finalPos));
				if(this.transform.CompareTag("Player") && amount.y == 0f){
					GetComponent<Animator>().SetTrigger("jump");
				}
			}
			return string.Empty;
		}

		return hit.transform.tag;
	}

	bool HasParede (Vector3 pos, out RaycastHit2D hit) {
		hit = Physics2D.Raycast (transform.position, (pos - transform.position).normalized, movementStep + 0.01f, _layerMask);
		return hit.collider != null;
	}

	IEnumerator Move (Vector2 final) {
		coroutineIsRunning = true;
		// TODO: Do animation
		if (hasAnimation != null)
			hasAnimation ();
		if (this.transform.CompareTag ("Player"))
			PartyManager.Instance.positionPreBattle = final;
		// Change transform position
		while (!MathUtil.IsApproximate (transform.position, final, .1f)) {
			// transform.position = Vector3.Lerp(transform.position, final, Time.deltaTime * 5f);
			transform.position = Vector3.MoveTowards (transform.position, final, 5f * Time.deltaTime);
			yield return null;
		}
		transform.position = final;

		coroutineIsRunning = false;

	}
}