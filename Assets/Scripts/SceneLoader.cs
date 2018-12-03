using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader> {
	protected override void Awake(){
        base.IsPersistentBetweenScenes = true;
        base.Awake();
    }

    public void LoadCombatScene(){
        AsyncOperation op = SceneManager.LoadSceneAsync("CombatScene", LoadSceneMode.Single);
        while(!op.isDone){}

        
    }
}
