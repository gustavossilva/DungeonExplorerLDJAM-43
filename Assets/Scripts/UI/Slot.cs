using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : DropPlace {
	public ItemDisplay itemUI;

	public bool isCharSlot;

	public bool IsEmpty(){
		return itemUI == null;
	}
}
