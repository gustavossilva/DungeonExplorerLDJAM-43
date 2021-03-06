﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UndestroyableCanvas : Singleton<UndestroyableCanvas> {

	protected override void Awake(){
		// Dont destroy to keep the items on place in the UI (lazy approach =P)
		base.IsPersistentBetweenScenes = true;
		base.Awake();
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode){
		// if(scene.name == "GameScene"){
			try{
				GetComponent<PointerOverUI>()._eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
			}
			catch{
				Debug.Log("Couldnt find");
			}
			// Disable the inventory canvas when you are in combat
			if(scene.name == "Combat" || scene.name == "GameOver" || scene.name == "FinalScene")
				transform.GetChild(0).gameObject.SetActive(false);
			else
				transform.GetChild(0).gameObject.SetActive(true);
			if(scene.name == "GameOver" || scene.name == "FinalScene")
			{
				Destroy(gameObject);
			}
		// }
	}

protected override void SubscribeEvents(){
	SceneManager.sceneLoaded += OnSceneLoaded;
	base.SubscribeEvents();
}

// protected override void OnDisable() {
// 	SceneManager.sceneLoaded -= OnSceneLoaded;
// }

protected override void UnsubscribeEvents() {
		SceneManager.sceneLoaded -= OnSceneLoaded;
		base.UnsubscribeEvents();
	}
}
