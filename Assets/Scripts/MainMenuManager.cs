using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour {

	public GameObject tutorial;

	private void Start() {
		if(!PlayerPrefs.HasKey("First")){
			PlayerPrefs.SetInt("First",0);
		}
		if(PlayerPrefs.GetInt("First") == 0)
		{
			tutorial.SetActive(true);
			PlayerPrefs.SetInt("First",1);
		}
	}
	
	public void StartGame()
	{
		SceneManager.LoadScene("GameIntro");
	}
}
