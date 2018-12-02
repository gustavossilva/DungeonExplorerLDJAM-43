using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntensity : MonoBehaviour {

	public Light _lightPoint;
	[SerializeField]private float maxFireIntensity,minFireIntensity;
	// Use this for initialization
	void Start () {
		_lightPoint = GetComponent<Light>();
		StartCoroutine ( FireIntensity());
	}

	private void Update() {
       
	}

	IEnumerator FireIntensity(){
		while(true){
			yield return new WaitForEndOfFrame();
			_lightPoint.intensity = Random.Range(minFireIntensity,maxFireIntensity);
		}
	}
}
