using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : DropPlace {
	public Item item;

	public bool isCharSlot;

	public bool IsEmpty(){
		return item == null;
	}
}
