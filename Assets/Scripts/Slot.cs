using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : DropPlace {
	[HideInInspector] public Item item;

	public bool IsEmpty(){
		return item == null;
	}
}
