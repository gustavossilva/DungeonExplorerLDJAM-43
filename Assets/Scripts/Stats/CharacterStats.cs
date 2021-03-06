﻿
using UnityEngine;

public class CharacterStats : MonoBehaviour {

	private float _currentHealth;

	public Stat maxHealth;
	public float currentHealth { get{return _currentHealth;} set {_currentHealth = Mathf.Clamp(value, 0f, int.MaxValue);} }
	public Stat damage;
	public Stat armor;

	private void Awake() 
	{
		currentHealth = maxHealth.GetValue();
	}

	public void TakeDamage (float damage) 
	{
		damage -= armor.GetValue();
		damage = Mathf.Clamp(damage, 0, int.MaxValue);
		currentHealth -= damage;

		if (currentHealth <= 0)
		{
			Die();
		}
	}

	public void TakeHeal(float heal)
	{
		if(currentHealth <= 0)
			return;
		currentHealth += heal;
		currentHealth = Mathf.Clamp(currentHealth,0,maxHealth.GetValue()); 
	}

	public virtual void Die()
	{
		//Die in some way
		Debug.Log(transform.name + "died. ");
	}

}
