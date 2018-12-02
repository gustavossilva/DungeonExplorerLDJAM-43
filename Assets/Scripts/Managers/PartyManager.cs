using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : Singleton<PartyManager> {

	public GameObject party;
	public int playersNumber = 4;
	public List<GameObject> partyMembers = new List<GameObject>();
	
	protected override void Awake() {
		IsPersistentBetweenScenes = false;
		base.Awake();
	}
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
