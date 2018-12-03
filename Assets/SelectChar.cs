using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectChar : MonoBehaviour {


	void Start(){
		
	}

	public void ShowChar(GameObject go){
		for(int i=0; i<transform.childCount; i++){
			transform.GetChild(i).gameObject.SetActive(false);
		}

		go.SetActive(true);
	}
}
