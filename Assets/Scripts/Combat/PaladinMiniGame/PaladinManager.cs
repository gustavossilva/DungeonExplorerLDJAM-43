using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinManager : Singleton<PaladinManager> {


	public GameObject axe1,axe2,axe3,axe4,axe5,axe6;
	public Transform topLimit, bottomLimit;
	public bool isPlaying = false;
	public bool missAxe = false;
	public int remainDefenses = 6;

	protected override void Awake() {
		IsPersistentBetweenScenes = false;
		base.Awake();
	}

	private void Start() {
		StartCoroutine(PlayGame());
	}

	private void Update() {
		if(missAxe)
		{
			Debug.Log("perdeu");
		}
		if(remainDefenses <= 0){
			Debug.Log("ganhou");
		}
	}

	IEnumerator PlayGame() {
		axe1.transform.position = new Vector2(axe1.transform.position.x,Random.Range(bottomLimit.position.y,topLimit.position.y));
		axe2.transform.position = new Vector2(axe2.transform.position.x,Random.Range(bottomLimit.position.y,topLimit.position.y));
		axe3.transform.position = new Vector2(axe3.transform.position.x,Random.Range(bottomLimit.position.y,topLimit.position.y));
		axe4.transform.position = new Vector2(axe4.transform.position.x,Random.Range(bottomLimit.position.y,topLimit.position.y));
		axe5.transform.position = new Vector2(axe5.transform.position.x,Random.Range(bottomLimit.position.y,topLimit.position.y));
		axe6.transform.position = new Vector2(axe6.transform.position.x,Random.Range(bottomLimit.position.y,topLimit.position.y));
		axe1.SetActive(true);
		yield return new WaitForSeconds(0.7f);
		axe2.SetActive(true);
		yield return new WaitForSeconds(0.7f);
		axe3.SetActive(true);
		yield return new WaitForSeconds(0.7f);
		axe4.SetActive(true);
		yield return new WaitForSeconds(0.7f);
		axe5.SetActive(true);
		yield return new WaitForSeconds(0.7f);
		axe6.SetActive(true);
		yield return null;
		StopCoroutine(PlayGame());
	}
}
