using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour {

	public TextMeshProUGUI gameOver;
	public GameObject pressAny;

	private bool gameOverComplete = false;
	// Use this for initialization
	void Start () {
		StartCoroutine(GameOver());
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown && gameOverComplete)
		{
			StopAllCoroutines();
			SceneManager.LoadScene("StartMenu");
		}
	}

	IEnumerator GameOver()
	{
		WaitForSeconds waitType = new WaitForSeconds(0.2f);
		gameOver.text = "G";
		yield return waitType;
		gameOver.text = "GA";
		yield return waitType;
		gameOver.text = "GAM";
		yield return waitType;
		gameOver.text = "GAME";
		yield return waitType;
		gameOver.text = "GAME O";
		yield return waitType;
		gameOver.text = "GAME OV";
		yield return waitType;
		gameOver.text = "GAME OVE";
		yield return waitType;
		gameOver.text = "GAME OVER";
		yield return waitType;
		gameOverComplete = true;
		StartCoroutine(PressAny());
	}

	IEnumerator PressAny()
	{
		WaitForSeconds blinkTime = new WaitForSeconds(0.5f);
		while (true)
		{
				pressAny.SetActive(!pressAny.activeSelf);
				yield return blinkTime;
		}
	}
}
