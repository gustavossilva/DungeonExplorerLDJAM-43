using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour {

	CharacterStats charStats;
	public Slot slot;

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

	public void UseItemOnStart(){
		if(!slot.IsEmpty()){
			switch(slot.itemUI.effect){
				case Effect.AUMENTA_DANO:
					// Tell the Party Manager to increase damage of all the characters
					// PartyManager.Instance.IncreaseDamage(); ---> this will iterate through all the CharacterCombat scripts and called the IncreaseDamage below
					break;
				case Effect.REDUZ_DANO_RECEBIDO:
					// Tell the PartyManager to increase the armor of all the characters
					// PartyManager.Instance.IncreaseArmor(); ---> this will iterate through all the CharacterCombat scripts and called the IncreaseArmor below
					break;
				case Effect.RESTAURA_HP:
					// Tell the PartyManager to heal  all the characters
					// PartyManager.Instance.HealEveryone() ---> this will iterate through all the CharacterCombat scripts and called the Heal below
					break;
			}
		}
	}

	public void UseItemOnAttack(){
		if(!slot.IsEmpty()){
			switch(slot.itemUI.effect){
				case Effect.CRITICO:
					// This will take the sum of the damage and double it.
					// Example: your base damage is 2, you receive 5 from the AUMENTA_DANO effect from other char
					// and then you have the CRITICO effect, which will double your damage 5 + 2 + (5 + 2) = 14
					float value = Random.value;
					if(value > 0.5)
						charStats.damage.AddModifier(charStats.damage.GetValue());
					else{
						// If there are 2 damage modifiers, it means the last is the critic x2
						// then remove it
						if(charStats.damage.numberOfModifiers == 2){
							charStats.damage.RemoveAtIndex(2);
						}
					}
					break;
				
				// TODO: Effect.NAO_RECEBE_DANO
			}
		}
		
	}

	public void IncreaseDamage(float modifier){
		charStats.damage.AddModifier(modifier);
	}

	public void IncreaseArmor(float modifier){
		charStats.armor.AddModifier(modifier);
	}

	public void Heal(float amount){
		charStats.TakeHeal(amount);
	}
}
