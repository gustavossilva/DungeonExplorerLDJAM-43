using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour {

	public Button _openInventoryButton;
	public Button _closeInventoryButton;
	Animator _anim;

	// Use this for initialization
	void Awake () {
		_anim = GetComponent<Animator>();
		_openInventoryButton.onClick.AddListener(Open);
		_closeInventoryButton.onClick.AddListener(Close);
	}

	void Open(){
		_anim.SetBool("isOpen", true);
		_openInventoryButton.interactable = false;
	}

	void Close(){
		_anim.SetBool("isOpen", false);
		_openInventoryButton.interactable = true;
	}
}
