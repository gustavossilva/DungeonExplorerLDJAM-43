using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameItem : MonoBehaviour {

	public Item item; // Scriptable Object

	public Sprite chestImagem;
	public SpriteRenderer chestRenderer;

	public Collider2D chestCollider;

	private bool isOpen = false;

	public string id;

	public AudioClip clip;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start () {
		chestRenderer = GetComponent<SpriteRenderer> ();
		if(!ItemManager.Instance.ChestsOpened.ContainsKey(id))
			ItemManager.Instance.ChestsOpened.Add(id,false);
		else{
			if(ItemManager.Instance.ChestsOpened[id])
				openChest ();
		}
	}
	void Update () {
		if (!isOpen) {
			/// TODO: Change this behaviour to a collision detection
			if (chestCollider.OverlapPoint (Camera.main.ScreenToWorldPoint (Input.mousePosition))) {
				if (Input.GetMouseButtonDown (1)) {
					if (InventoryManager.Instance.ItemCollected (item)) { 
						ItemManager.Instance.ChestsOpened[id] = true;
						openChest ();
						GetComponent<AudioSource>().PlayOneShot(clip, .5f);
					} else 
						Debug.Log ("Inventory Full!");
				}

			}
		}
	}
	void openChest () {
		chestRenderer.sprite = chestImagem;
		isOpen = true;
	}
}