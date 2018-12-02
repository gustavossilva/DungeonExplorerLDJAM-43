using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerOverUI : MonoBehaviour {

	private List<RaycastResult> _results;
	private PointerEventData _eventDataCurrentPosition;
	public EventSystem _eventSystem;

	void Awake () {
		// _eventSystem = EventSystem.current;

		_results = new List<RaycastResult> ();
		_eventDataCurrentPosition = new PointerEventData (_eventSystem);
	}

	public bool IsPointerOverUIObject (Vector3 mousePosition) {
		// Clear the raycast result array
		_results.Clear ();

		// Set the position of the container at the position where the mouse/touch is
		_eventDataCurrentPosition.position = new Vector2 (mousePosition.x, mousePosition.y);

		// Raycast from the pointer position and stores the result into the list
		_eventSystem.RaycastAll (_eventDataCurrentPosition, _results);

		// If there is something in the results array, it means we touched/clicked on a UI element 
		return _results.Count > 0;
	}
	public bool IsPointerOverSpecificObject (Vector3 mousePosition, int specificObjectId) {
		// Clear the raycast result array
		_results.Clear ();

		// Set the position of the container at the position where the mouse/touch is
		_eventDataCurrentPosition.position = new Vector2 (mousePosition.x, mousePosition.y);

		// Raycast from the pointer position and stores the result into the list
		_eventSystem.RaycastAll (_eventDataCurrentPosition, _results);

		//Percorre a lista de resultados e teste se algum dos objetos em _results é o objeto específico, caso seja, retorna true
		for (int i = 0; i < _results.Count; i++) {
			if (_results[i].gameObject.GetInstanceID () == specificObjectId)
				return true;
		}
		// If there is something in the results array, it means we touched/clicked on a UI element 
		return false;
	}

	public DropPlace IsPointerOverDropPlace(Vector3 mousePosition){
		// Clear the raycast result array
		_results.Clear ();

		// Set the position of the container at the position where the mouse/touch is
		_eventDataCurrentPosition.position = new Vector2 (mousePosition.x, mousePosition.y);

		// Raycast from the pointer position and stores the result into the list
		_eventSystem.RaycastAll (_eventDataCurrentPosition, _results);

		//Percorre a lista de resultados e teste se algum dos objetos em _results é o slot, caso seja, retorna-o
		for (int i = 0; i < _results.Count; i++) {
			if (_results[i].gameObject.tag == "Drop Place"){
				return _results[i].gameObject.GetComponent<DropPlace>();
			}
		}

		return null;
	}
}