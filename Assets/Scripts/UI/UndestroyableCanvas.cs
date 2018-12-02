using UnityEngine;
using UnityEngine.SceneManagement;

public class UndestroyableCanvas : Singleton<UndestroyableCanvas> {

	protected override void Awake(){

		SceneManager.sceneLoaded += OnSceneLoaded;

		// Dont destroy to keep the items on place in the UI (lazy approach =P)
		base.IsPersistentBetweenScenes = true;
		base.Awake();
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode){
		// Disable the inventory canvas when you are in combat
		if(scene.name == "Combat")
			gameObject.SetActive(false);
		else
			gameObject.SetActive(true);
	}
}
