using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameItem : MonoBehaviour {

	public Item item; // Scriptable Object

	public Sprite chestImagem;
	public SpriteRenderer chestRenderer;

	public Collider2D chestCollider;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start () {
		chestRenderer = GetComponent<SpriteRenderer> ();
	}
	void Update () {

		/// TODO: Change this behaviour to a collision detection
		if (chestCollider.OverlapPoint ( Camera.main.ScreenToWorldPoint(Input.mousePosition))) {
			if (Input.GetMouseButtonDown (1)) {
				if (InventoryManager.Instance.ItemCollected (item))
					chestRenderer.sprite = chestImagem;
				else
					Debug.Log ("Inventory Full!");
			}

		}
	}
}