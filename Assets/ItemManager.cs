using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager> {

	public Dictionary<string,bool> ChestsOpened; 
	// Use this for initialization

	private void Awake() {
		ChestsOpened = new Dictionary<string,bool>();
		IsPersistentBetweenScenes = true;
		base.Awake();
	}

}
