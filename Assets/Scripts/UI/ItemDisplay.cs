using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	UIDragAndRelease _dragAndDrop;
	PointerOverUI _pointerOverUI;
	RectTransform _rectTranform;


	private Transform _tempParent;
	private Transform _currentParent;
	private bool _isHovered;
	private bool _clicked;

	[HideInInspector] public Image image;
	[HideInInspector] public Effect effect;
	[HideInInspector] public string description;
	private float timeHovering;
	public float timeHover = 1.5f;
	public GameObject descriptionImage;
	private bool _messageShown = false;
	private Text text;

	// Use this for initialization
	void Awake () {
		_pointerOverUI = GameObject.Find("Canvas").GetComponent<PointerOverUI>();
		_dragAndDrop = GetComponent<UIDragAndRelease>();
		_rectTranform = GetComponent<RectTransform>();
		image = GetComponent<Image>();

		text = descriptionImage.GetComponentInChildren<Text>();

		_dragAndDrop.released += OnReleased;
		_dragAndDrop.clicked += OnClicked;

		_tempParent = GameObject.Find("Canvas").transform;
		_currentParent = transform.parent;
	}


	void Update(){
		if(_isHovered && !_clicked){
			timeHovering += Time.deltaTime;
			if(timeHovering >= timeHover && !_messageShown){
				ShowMessageInfo();
			}
		}
	}


	private bool OnReleased(){
		_clicked = false;
		DropPlace placeToDrop = _pointerOverUI.IsPointerOverDropPlace(Input.mousePosition);
		transform.SetParent(_currentParent);
		// If there is somewhere to drop the item
		if(placeToDrop != null){
			
			// If it is a slot...
			if(placeToDrop is Slot){
				Slot newSlot = (Slot)placeToDrop;
				if(newSlot.IsEmpty()){

					Slot previousSlot = transform.parent.GetComponent<Slot>();
					previousSlot.itemUI = null;

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
					newSlot.itemUI = this;
					return true;
				}
			}
			else if(placeToDrop is Bin){
				Bin bin = (Bin)placeToDrop;
				Slot previousSlot = transform.parent.GetComponent<Slot>();
				previousSlot.itemUI = null;

				if(!previousSlot.isCharSlot)
					InventoryManager.Instance.itemsQuantity--;
				
				bin.ThrowAway(gameObject);
			}
		}

		return false;
	}

	private bool OnClicked(){
		_clicked = true;
		transform.SetParent(_tempParent);
		if(_messageShown){
			HideMessageInfo();
		}
		return true;
	}

	private void ShowMessageInfo(){
		_messageShown = true;
		text.text = description;
		descriptionImage.SetActive(true);
	}

	private void HideMessageInfo(){
		_messageShown = false;
		descriptionImage.SetActive(false);
	}

	void OnDestroy(){
		_dragAndDrop.released -= OnReleased;
		_dragAndDrop.clicked -= OnClicked;
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
		_isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
		HideMessageInfo();
		_isHovered = false;
		timeHovering = 0f;
    }
}
