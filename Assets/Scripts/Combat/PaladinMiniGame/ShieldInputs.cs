using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldInputs : MonoBehaviour {

	public Transform topLimit, bottomLimit;
	// Update is called once per frame
	void Update () {
		if(!PaladinManager.Instance.isPlaying)
			return;
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = new Vector3(transform.position.x,Mathf.Clamp(mousePos.y,bottomLimit.position.y,topLimit.position.y),0);
	}
}
