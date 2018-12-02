using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour {
	[HideInInspector] public Item item;

	public bool IsEmpty(){
		return item == null;
	}
}
