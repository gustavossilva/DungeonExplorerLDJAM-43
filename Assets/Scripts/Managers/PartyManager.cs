using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : Singleton<PartyManager> {

	public GameObject party;
	public List<GameObject> partyMembers = new List<GameObject>();
	
	protected override void Awake() {
		IsPersistentBetweenScenes = false;
		base.Awake();
	}
	
	// Use this for initialization
	void Start () {
		for(int i = 0; i<party.transform.childCount;i++)
		{
			partyMembers.Add(party.transform.GetChild(i).gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
