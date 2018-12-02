using UnityEngine;

public class BarbarianManager : Singleton<BarbarianManager> {

	public bool isPlaying = false;
	private bool canAttack = false;


	protected override void Awake() {
		IsPersistentBetweenScenes = false;
		base.Awake();
	}
	
	void Update () {
		if(!isPlaying)
			return;
		if(Input.GetMouseButtonDown(0))
		{
			if(canAttack)
			{
				Debug.Log("Attack");
				gameObject.SetActive(false);
			}
			else
			{
				Debug.Log("Char hit");
			}
		}
	}


	private void OnTriggerEnter2D(Collider2D other) 
	{
		if(!isPlaying)
			return;
		if(!canAttack)
		{
			canAttack = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other) 
	{
		if(!isPlaying)
			return;
		if(canAttack)
		{
			canAttack = !canAttack;
		}

	}
}
