using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCharacter : Singleton<SelectedCharacter> {

	public string selectedCharName;

	protected override void Awake(){
		base.IsPersistentBetweenScenes = true;
		base.Awake();
	}



}
