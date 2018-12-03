using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour {

	public CharacterStats charStats;
	public Slot slot;

	void Awake () {
		// charStats = GetComponent<CharacterStats>();
		// PartyManager.Instance.AddCharacter(this);
	}

	void Start(){
		// Take the item from the slot first!!!
		// switch(character){
		// 	case Char.CHAR_A:
		// 		slot = InventoryManager.Instance.slotCharA;
		// 		break;
		// 	case Char.CHAR_B:
		// 		slot = InventoryManager.Instance.slotCharB;
		// 		break;
		// 	case Char.CHAR_C:
		// 		slot = InventoryManager.Instance.slotCharC;
		// 		break;
		// 	case Char.CHAR_D:
		// 		slot = InventoryManager.Instance.slotCharD;
		// 		break;
		// 	case Char.MAIN_CHAR:
		// 		slot = InventoryManager.Instance.slotMainChar;
		// 		break;
		// }

		// use item
		// UseItemOnStart();
	}

	// public void Attack (CharacterStats target)
	// {
	// 	//Do attack Animation
	// 	target.TakeDamage(charStats.damage.GetValue());
	// }

	// public void Heal (CharacterStats target)
	// {
	// 	//Do heal animation
	// 	target.TakeHeal(charStats.damage.GetValue());
	// }

	public void UseItemOnStart(){
		if(!slot.IsEmpty()){
			switch(slot.itemUI.effect){
				case Effect.AUMENTA_DANO:
					// Tell the Party Manager to increase damage of all the characters
					// This will iterate through all the CharacterCombat scripts and called the IncreaseDamage below
					PartyManager.Instance.IncreasePartyDamage(5);
					break;
				case Effect.REDUZ_DANO_RECEBIDO:
					// Tell the PartyManager to increase the armor of all the characters
					// This will iterate through all the CharacterCombat scripts and called the IncreaseArmor below
					PartyManager.Instance.IncreasePartyArmor(10);
					break;
				case Effect.RESTAURA_HP:
					// Tell the PartyManager to heal  all the characters
					// This will iterate through all the CharacterCombat scripts and called the Heal below
					PartyManager.Instance.HealParty(15);
					break;
				case Effect.CRITICO:
					charStats.damage.critical = true;
					break;
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
