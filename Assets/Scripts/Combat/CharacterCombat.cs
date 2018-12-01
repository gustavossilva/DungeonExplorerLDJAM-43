using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour {

	CharacterStats charStats;
	void Start () {
		charStats = GetComponent<CharacterStats>();
	}

	public void Attack (CharacterStats target)
	{
		//Do attack Animation
		target.TakeDamage(charStats.damage.GetValue());
	}

	public void Heal (CharacterStats target)
	{
		//Do heal animation
		target.TakeHeal(charStats.damage.GetValue());
	}
}
