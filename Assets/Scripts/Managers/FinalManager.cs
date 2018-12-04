using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FinalManager : MonoBehaviour {

		public HashSet<Type> typesToDelete;

		void Start () 
    {
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
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			SceneManager.LoadScene("StartMenu");
		}
	}
}
