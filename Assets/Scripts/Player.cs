using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

[RequireComponent(typeof(GridMovement))]
public class Player : Singleton<Player> {

	public float cooldown = 0.35f;
	private float movementCooldown;
	private GridMovement _gridMovement;
	private string _obstacleTag;
	public bool canMove;

	public event System.Action moved;

	public SkeletonAnimation skeleton;
	public SkeletonDataAsset _barbarianAsset;
	public SkeletonDataAsset _clericAsset;
	public SkeletonDataAsset _rangerAsset;
	public SkeletonDataAsset _wizardAsset;
	public SkeletonDataAsset _paladinAsset;
	public AudioClip footstepClip;
	public AudioSource audioSource;
	public Animator anim;

	protected override void  Awake(){
		base.IsPersistentBetweenScenes = false;
		base.Awake();
		

		skeleton = GetComponentInChildren<SkeletonAnimation>();
		switch(SelectedCharacter.Instance.selectedCharName){
			case "Barbarian":
				skeleton.skeletonDataAsset = _barbarianAsset;
				break;
			case "Wizard":
				skeleton.skeletonDataAsset = _wizardAsset;
				break;
			case "Ranger":
				skeleton.skeletonDataAsset = _rangerAsset;
				break;
			case "Paladin":
				skeleton.skeletonDataAsset = _paladinAsset;
				break;
			case "Cleric":
				skeleton.skeletonDataAsset = _clericAsset;
				break;
		}
	}

	// Use this for initialization
	void Start () {
		PartyManager.Instance.party = transform.gameObject;
		_gridMovement = GetComponent<GridMovement>();
		movementCooldown = cooldown;
		canMove = true;
		skeleton.ClearState();
		skeleton.Initialize(false);
		skeleton.state.SetAnimation(0, "Iddle", true);
		skeleton.gameObject.GetComponent<MeshRenderer>().sortingLayerName = "Foreground";
		skeleton.gameObject.GetComponent<MeshRenderer>().sortingOrder = 1000;
	}
	
	// Update is called once per frame
	void Update () {
		if(!canMove)
			return;

		movementCooldown -= Time.deltaTime;
		
		// Only resets cooldown if the player could successfuly perform the movement
		if(movementCooldown <= 0){
			_obstacleTag = string.Empty;

			// LEFT
			if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
				MoveAndCheck(Vector2.left);
				transform.GetChild(1).localScale = new Vector3(-0.3f, transform.GetChild(1).localScale.y, transform.GetChild(1).localScale.z);
				return;
			}

			// RIGHT
			if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
				MoveAndCheck(Vector2.right);
				transform.GetChild(1).localScale = new Vector3(0.3f, transform.GetChild(1).localScale.y, transform.GetChild(1).localScale.z);
				return;
			}

			// UP
			if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)){
				MoveAndCheck(Vector2.up);
				return;
			}

			// DOWN
			if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)){
				MoveAndCheck(Vector2.down);
				return;
			}
		}
	}

	void MoveAndCheck(Vector2 direction){
		// Perform the movement
		// anim.SetTrigger("jump");
		_obstacleTag = _gridMovement.MoveBy(direction);
		
		// Verifies if the players got in contact with an enemy
		if(string.IsNullOrEmpty(_obstacleTag)){
			audioSource.PlayOneShot(footstepClip);
			movementCooldown = cooldown;
			if(moved != null)
				moved();
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Enemy")){
			canMove = false;
			// Start combat scene here
			
		}
	}
}
