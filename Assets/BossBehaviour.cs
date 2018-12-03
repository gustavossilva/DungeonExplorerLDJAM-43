using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
public class BossBehaviour : MonoBehaviour {

	public string id;
	private SkeletonAnimation bossSkeleton;

	public AnimationReferenceAsset idle;

	public GameObject openPortal;
	
	// Use this for initialization
	void Start () {
		if (!MonsterManager.Instance.monstersInGame.ContainsKey (id))
			MonsterManager.Instance.monstersInGame.Add (id, true);
		else {
			gameObject.SetActive (MonsterManager.Instance.monstersInGame[id]);
			openPortal.SetActive(true);
		}
		bossSkeleton = GetComponent<SkeletonAnimation> ();
		bossSkeleton.AnimationState.SetAnimation (0, idle, true);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			MonsterManager.Instance.monsterName = "Boss";
			MonsterManager.Instance.StartBattle (other.transform, id);
		}
	}
}