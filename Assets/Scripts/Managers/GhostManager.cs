using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostManager : Singleton<GhostManager> {

	public GameObject ghostUI;
	public GameObject textHandler;
	public GameObject free;
	public GameObject pay;

	private string[] characterNames = new string[]{"Barbarian", "Paladin", "Ranger", "Cleric", "Wizard"};
	protected override void Awake() {
		IsPersistentBetweenScenes = false;
		base.Awake();
	}

	public void ActiveFreeGhost()
	{
		ghostUI.SetActive(true);
		textHandler.SetActive(true);
		free.SetActive(true);
	}

	public void ActivePayGhost(int index){
		ghostUI.SetActive(true);
		textHandler.SetActive(true);
		pay.GetComponent<TMPro.TextMeshProUGUI>().text = "A life for a life, you shall pay! Sacrifice your <size=50><style=\"Title\">" + characterNames[index] + "</style></size> in order to play!";
		pay.SetActive(true);
	}

	public void SetTime()
	{
		SceneManager.LoadScene("Combat");
		ghostUI.SetActive(false);
		textHandler.SetActive(false);
		pay.SetActive(false);
		free.SetActive(false);
		Time.timeScale = 1;
	}

}
