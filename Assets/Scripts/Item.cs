using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	UIDragAndRelease _dragAndDrop;
	PointerOverUI _pointerOverUI;

	// Use this for initialization
	void Awake () {
		_pointerOverUI = GameObject.Find("Canvas").GetComponent<PointerOverUI>();
		_dragAndDrop = GetComponent<UIDragAndRelease>();
		_dragAndDrop.released += OnReleased;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private bool OnReleased(){
		_pointerOverUI.IsPointerOverSlot(Input.mousePosition);
		return false;
	}
}
