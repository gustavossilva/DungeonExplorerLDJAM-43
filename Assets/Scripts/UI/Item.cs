using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {

	UIDragAndRelease _dragAndDrop;
	PointerOverUI _pointerOverUI;

	RectTransform _rectTranform;


	private Transform _tempParent;
	private Transform _currentParent;
	public Image image;

	// Use this for initialization
	void Awake () {
		_pointerOverUI = GameObject.Find("Canvas").GetComponent<PointerOverUI>();
		_dragAndDrop = GetComponent<UIDragAndRelease>();
		_rectTranform = GetComponent<RectTransform>();
		image = GetComponent<Image>();

		_dragAndDrop.released += OnReleased;
		_dragAndDrop.clicked += OnClicked;

		_tempParent = GameObject.Find("Canvas").transform;
		_currentParent = transform.parent;
	}

	private bool OnReleased(){
		DropPlace placeToDrop = _pointerOverUI.IsPointerOverDropPlace(Input.mousePosition);
		transform.SetParent(_currentParent);
		// If there is somewhere to drop the item
		if(placeToDrop != null){
			
			// If it is a slot...
			if(placeToDrop is Slot){
				Slot newSlot = (Slot)placeToDrop;
				if(newSlot.IsEmpty()){

					Slot previousSlot = transform.parent.GetComponent<Slot>();
					previousSlot.item = null;

					if(!previousSlot.isCharSlot)
						InventoryManager.Instance.itemsQuantity--;

					if(!newSlot.isCharSlot)
						InventoryManager.Instance.itemsQuantity++;

					// Move this gameobject to be the child of the slot
					transform.SetParent(newSlot.transform);
					_currentParent = transform.parent;
					_rectTranform.offsetMax = Vector2.one * -2f;
					_rectTranform.offsetMin = Vector2.one * 2f;
					// Assign the item variable as this item
					newSlot.item = this;
					return true;
				}
			}
			else if(placeToDrop is Bin){
				Bin bin = (Bin)placeToDrop;
				Slot previousSlot = transform.parent.GetComponent<Slot>();
				previousSlot.item = null;

				if(!previousSlot.isCharSlot)
					InventoryManager.Instance.itemsQuantity--;
				
				bin.ThrowAway(gameObject);
			}
		}

		return false;
	}

	private bool OnClicked(){
		transform.SetParent(_tempParent);
		return true;
	}
}
