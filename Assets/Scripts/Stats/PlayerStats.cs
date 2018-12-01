using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats {

	private int attackByTurn = 1; //Qtd attacks per turn

	public void AddAttackTurn()
	{
		if(attackByTurn >= 2)
		{
			return;
		}
		attackByTurn++;
	}

	public void RemoveAttackTurn()
	{
		if(attackByTurn <= 1){
			return;
		}
		attackByTurn--;
	}

}
