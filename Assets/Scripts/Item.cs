using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	UIDragAndRelease _dragAndDrop;
	PointerOverUI _pointerOverUI;

	RectTransform _rectTranform;


	private Transform _tempParent;
	private Transform _currentParent;

	// Use this for initialization
	void Awake () {
		_pointerOverUI = GameObject.Find("Canvas").GetComponent<PointerOverUI>();
		_dragAndDrop = GetComponent<UIDragAndRelease>();
		_rectTranform = GetComponent<RectTransform>();
		_dragAndDrop.released += OnReleased;
		_dragAndDrop.clicked += OnClicked;

		_tempParent = GameObject.Find("Canvas").transform;
		_currentParent = transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private bool OnReleased(){
		DropPlace placeToDrop = _pointerOverUI.IsPointerOverDropPlace(Input.mousePosition);
		transform.SetParent(_currentParent);
		// If there is somewhere to drop the item
		if(placeToDrop != null){
			
			// If it is a slot...
			if(placeToDrop is Slot){
				Slot slot = (Slot)placeToDrop;
				if(slot.IsEmpty()){

					Slot previousSlot = transform.parent.GetComponent<Slot>();
					previousSlot.item = null;

					// Move this gameobject to be the child of the slot
					transform.SetParent(slot.transform);
					_currentParent = transform.parent;
					_rectTranform.offsetMax = Vector2.one * -2f;
					_rectTranform.offsetMin = Vector2.one * 2f;
					// Assign the item variable as this item
					slot.item = this;
					return true;
				}
			}
			// TODO
			// else if(placeToDrop is Bin){

			// }
		}

		return false;
	}

	private bool OnClicked(){
		transform.SetParent(_tempParent);
		return true;
	}
}
