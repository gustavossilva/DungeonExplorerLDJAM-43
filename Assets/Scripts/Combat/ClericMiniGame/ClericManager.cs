using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClericManager : Singleton<ClericManager> {

	public Slider healthBar;


	protected override void Awake(){
		IsPersistentBetweenScenes = false;
		base.Awake();
	}
	// Update is called once per frame
	void Update () {
		if(healthBar.value >= 99.5)
		{
			Debug.Log("Heal Everyone");
		}
		if(healthBar.value <= 0.2)
		{
			Debug.Log("Hit player");
		}
	}

	public void AddHealthBar(float qtdToAdd)
	{
		healthBar.value += qtdToAdd;
	}

	public void RemoveHealthBar(float qtdToRemove)
	{
		healthBar.value -=qtdToRemove;
	}

}
