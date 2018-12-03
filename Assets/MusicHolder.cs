using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicHolder : Singleton<MusicHolder> {

	public AudioClip menuMusic;
	public AudioClip dungeonMusic;

	private AudioClip currentAudioClip;

	protected override void Awake(){
		base.IsPersistentBetweenScenes = true;
		base.Awake();


		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
		if(scene.name == "StartMenu"){
			PlayMusic(menuMusic, 0.5f);
		}else if(scene.name == "GameScene"){
			PlayMusic(dungeonMusic, 0.2f);
		}
	}

	private void PlayMusic(AudioClip clip, float volume){
		AudioSource source = GetComponent<AudioSource>();
		if(currentAudioClip != clip){
			source.Stop();
			currentAudioClip = clip;
			source.clip = currentAudioClip;
			source.loop = true;
			source.volume = volume;
			source.Play();
		}
	}

}
