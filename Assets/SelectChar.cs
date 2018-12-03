using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectChar : MonoBehaviour {


	void Start(){
		SelectedCharacter.Instance.selectedCharName = "Barbarian";
	}

	public void ShowChar(GameObject go){
		for(int i=0; i<transform.childCount; i++){
			transform.GetChild(i).gameObject.SetActive(false);
		}

		go.SetActive(true);
		SelectedCharacter.Instance.selectedCharName = go.name;
	}

	public void StartGame(){
		SceneManager.LoadScene("GameScene");
	}
}
