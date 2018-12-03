using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat {

	[SerializeField]
	public float baseValue;

	private List<float> modifiers = new List<float>();

	private int multiplier;
	public bool critical = false;

	public float numberOfModifiers {
		get { return modifiers.Count; }
	}

	public float GetValue () 
	{
		multiplier = 1;
		if(critical && Random.value > 0.5f)
			multiplier = 2;

		float finalValue = baseValue;
		modifiers.ForEach(x => finalValue += x);
		return finalValue * multiplier;
	}

	public void AddModifier(float modifier)
	{
		if(modifier != 0)
		{
			modifiers.Add(modifier);
		}
	}

	public void RemoveModifier(float modifier)
	{
		if(modifier != 0)
		{
			modifiers.Remove(modifier);
		}
	}

	public void RemoveAtIndex(int index)
	{
		if(index < numberOfModifiers){
			modifiers.RemoveAt(index);
		}else{
			Debug.Log("Error!!!!! there is not enough modifiers to be removed");
		}
	}

}
