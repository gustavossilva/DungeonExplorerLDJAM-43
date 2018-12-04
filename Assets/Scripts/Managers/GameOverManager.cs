using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour {

	public TextMeshProUGUI gameOver;
	public GameObject pressAny;
	public HashSet<Type> typesToDelete;

	private bool gameOverComplete = false;
	// Use this for initialization
		void Start () 
    {
			StartCoroutine(GameOver());
			typesToDelete = new HashSet<Type>();

        // Mark the type of singletons that must be destroyed
        MarkTypesToDelete();

        // Get all monobehaviours that exist in the scene
        MonoBehaviour[] monoBehaviours = FindObjectsOfType<MonoBehaviour>();

        for (int i = 0; i < monoBehaviours.Length; i++)
        {
            MonoBehaviour possibleSingleton = monoBehaviours[i];
            Type type = possibleSingleton.GetType();

            // Destroy some singletons (StoreManager, VirtualPetManager, )
            if(typesToDelete.Contains(type))
                Destroy(possibleSingleton.gameObject);
        }
    }

    /// <summary>
    /// Mark the types of singleton this script will delete
    /// </summary>
    void MarkTypesToDelete()
    {
        typesToDelete.Add(typeof(ItemManager));
        typesToDelete.Add(typeof(InventoryManager));
        typesToDelete.Add(typeof(MonsterManager));
        typesToDelete.Add(typeof(PartyManager));
        typesToDelete.Add(typeof(SceneLoader));
        typesToDelete.Add(typeof(UndestroyableCanvas));
        typesToDelete.Add(typeof(SelectedCharacter));
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space) && gameOverComplete)
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
