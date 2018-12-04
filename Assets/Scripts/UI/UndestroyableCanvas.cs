using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UndestroyableCanvas : Singleton<UndestroyableCanvas> {

	protected override void Awake(){

		SceneManager.sceneLoaded += OnSceneLoaded;

		// Dont destroy to keep the items on place in the UI (lazy approach =P)
		base.IsPersistentBetweenScenes = true;
		base.Awake();
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode){

		GetComponent<PointerOverUI>()._eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

		// Disable the inventory canvas when you are in combat
		if(scene.name == "Combat" || scene.name == "GameOver" || scene.name == "FinalScene")
			gameObject.SetActive(false);
		else
			gameObject.SetActive(true);
		if(scene.name == "GameOver" || scene.name == "FinalScene")
		{
			Destroy(gameObject);
		}
	}
}
