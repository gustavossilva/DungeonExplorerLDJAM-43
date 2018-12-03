using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class historyController : MonoBehaviour {

	public RectTransform textBox;
	public Image textBoxImage;

	public List<Animator> animations;
	public TextMeshProUGUI historyText;
	private int actualScene = -1;

	private List<string> history;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start () {
		history = new List<string> ();
		history.Add ("O fantasma pegou geral");
		history.Add ("Todo mundo caiu e se fodeu");
	}
	// Use this for initialization
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetMouseButtonDown (0)) {
			actualScene++;
			if (actualScene >= animations.Count) {
				SceneManager.LoadSceneAsync ("SelectChar", LoadSceneMode.Single);
			} else {
				animations[actualScene].SetTrigger ("SceneChange");
				historyText.text = history[actualScene];
			}
		}

	}
}