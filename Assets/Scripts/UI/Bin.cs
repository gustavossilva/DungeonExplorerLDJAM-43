using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : DropPlace {

	public void ThrowAway(GameObject go){
		Destroy(go);
	} 
}
