using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent (typeof (EventTrigger))]
public class UIDragAndRelease : MonoBehaviour {

	private RectTransform _rectTransform; 			// The rect transform which will be useful for repositioning and rescaling
	private Vector2 _initialPosition; 				// Initial position to reposition this gameobject
	private Vector2 _initialDelta; 					// The size of this rect transform to make it reapper
	private Vector2 _worldPositionAux; 				// The size of this rect transform to make it reapper
	private bool _coroutineIsRunning; 				// Control variable to ignore input event when reposition coroutine is running
	private bool _ignoreInput; 						// Control variable to ignore input event
	private bool _canDrag; 							// Controls whether this gameobject can be dragged or not

	private bool _isWorldElement = false; 			//Controls the element space (world position or not)
	private Coroutine _repositionCoroutine; 		// Reference to the reposition coroutine
	private EventTrigger _eventTrigger; 			// The event trigger responsible to handle drag behaviour
	private Camera _mainCamera;						// Reference to the camera to translate screen units to world units


	public delegate bool InputEvent (); 			// delegate to issue events when this gameobject is clicked or released 
	public event InputEvent released;
	public event InputEvent clicked;
	public event InputEvent onDrag;

	bool dragBehaviourContinue = true;
	private GameObject _backObjectWhenFinish;
	public GameObject BackObjectWhenFinish
    {
        get { return this._backObjectWhenFinish; }
		set {this._backObjectWhenFinish = value; }
    }
    public bool IsWorldElement
    {
        get { return this._isWorldElement; }
		set {this._isWorldElement = value; }
    }
	void Awake () {
		if (_rectTransform == null)
			_rectTransform = GetComponent<RectTransform> ();

		_initialPosition = _rectTransform.anchoredPosition;
		_initialDelta = _rectTransform.sizeDelta;

		_eventTrigger = GetComponent<EventTrigger> ();
		_eventTrigger.triggers.RemoveRange (0, _eventTrigger.triggers.Count);

		// Register three event trigger types for start dragging, dragging and finish dragging
		RegisterEventTriggerType (EventTriggerType.BeginDrag, OnStartDrag);
		RegisterEventTriggerType (EventTriggerType.Drag, OnDragging);
		RegisterEventTriggerType (EventTriggerType.EndDrag, OnEndDrag);
	}
	private void Start(){
		_mainCamera = Camera.main;
	}

	/// <summary>
	/// Called when this gameobject is about to start being dragged
	/// </summary>
	public void OnStartDrag () {
		// Apply some tolerance to the new input when the gameobject is getting back to its original position
		// So the player does not need to wait the coroutine to finish in order to interact again with the gameobject 
		if (_coroutineIsRunning) {
			if (MathUtil.IsApproximate (_rectTransform.anchoredPosition, _initialPosition, 10f)) {
				StopCoroutine (_repositionCoroutine);
				_coroutineIsRunning = false;
				_ignoreInput = false;
			} else
				_ignoreInput = true;
		}

		if (clicked != null)
			_canDrag = clicked ();
		else
			_canDrag = true;
			
		if(_backObjectWhenFinish != null)
			_backObjectWhenFinish.SetActive(false);
	}

	/// <summary>
	/// Called when this gameobject is being dragged
	/// </summary>
	public void OnDragging () {
		// Ignore inputs when either the coroutine is running or when the player clicks on the gameobject when
		// it was returning to its original position
		if (_coroutineIsRunning || _ignoreInput || !_canDrag)
			return;

		// Follows the position of the respective input
		if(_isWorldElement){
			_worldPositionAux = _mainCamera.ScreenToWorldPoint( Input.mousePosition);
			_rectTransform.position = new Vector3(_worldPositionAux.x,_worldPositionAux.y,0);
		}
		else
			_rectTransform.position = Input.mousePosition;

		if (onDrag != null && dragBehaviourContinue)
			dragBehaviourContinue = onDrag();
	}

	/// <summary>
	/// Called when this gameobject finishes being dragged
	/// </summary>
	public void OnEndDrag () {
		// Ignore if the gameobject could not be dragged before
		if (!_canDrag)
			return;

		bool hasBeenConsumed = false;

		if (released != null)
			hasBeenConsumed = released ();

		// If it has not been cosumed, reposition it. Otherwise, reset its position and scale it up
		if (!hasBeenConsumed)
			_repositionCoroutine = StartCoroutine (GoBackToInitialPosition ());
		else
			StartCoroutine (ReapperAndScale ());

		// Allows further input interactions when the player releases the gameobject. 
		// (PS.: the interactions also depends on WHERE and WHEN the player clicks on the gameobject again)
		_ignoreInput = false;
	}

	/// <summary>
	/// Reposition this gameobject to its original position
	/// </summary>
	IEnumerator GoBackToInitialPosition () {
		_coroutineIsRunning = true;

		while (!MathUtil.IsApproximate (_rectTransform.anchoredPosition, _initialPosition)) {
			_rectTransform.anchoredPosition = Vector2.Lerp (_rectTransform.anchoredPosition, _initialPosition, 10f * Time.deltaTime);
			yield return null;
		}
		if(_backObjectWhenFinish != null)
			_backObjectWhenFinish.SetActive(true);
		_coroutineIsRunning = false;
	}

	/// <summary>
	/// Reset this gameobject position and scale it up to its original delta size
	/// </summary>
	private IEnumerator ReapperAndScale () {
		_rectTransform.anchoredPosition = _initialPosition;
		_rectTransform.sizeDelta = Vector2.zero;

		while (!MathUtil.IsApproximate (_rectTransform.sizeDelta, _initialDelta)) {
			_rectTransform.sizeDelta = Vector2.Lerp (_rectTransform.sizeDelta, _initialDelta, 8f * Time.deltaTime);
			yield return null;
		}
	}

	private void RegisterEventTriggerType (EventTriggerType type, Action method) {
		EventTrigger.Entry entry = new EventTrigger.Entry ();
		entry.eventID = type;
		entry.callback.AddListener ((data) => {
			method ();
		});

		_eventTrigger.triggers.Add (entry);
	}

	/// <summary>
	/// Removes all listeners for each event trigger type and remove all the trigger types from the event trigger
	/// </summary>
	private void RemoveEventTriggerListeners () {
		for (int i = 0; i < _eventTrigger.triggers.Count; i++) {
			_eventTrigger.triggers[i].callback.RemoveAllListeners ();
		}

		_eventTrigger.triggers.RemoveRange (0, _eventTrigger.triggers.Count);
	}

	void OnDisable () {
		// This is necessary when the panel is disabled but reposition or resizing coroutines do not complete
		StopAllCoroutines ();
		_rectTransform.anchoredPosition = _initialPosition;
		_rectTransform.sizeDelta = _initialDelta;
	}

	void OnDestroy () {
		RemoveEventTriggerListeners ();
	}
}