using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicHolder : Singleton<MusicHolder> {

	public AudioClip menuMusic;
	public AudioClip dungeonMusic;
	public AudioClip combatMusic;

	private AudioClip currentAudioClip;

	protected override void Awake(){
		base.IsPersistentBetweenScenes = true;
		base.Awake();
	}
	
	private void Start() {
		SceneManager.sceneLoaded += OnSceneLoadedMusic;
	}
	
	private void OnSceneLoadedMusic(Scene scene, LoadSceneMode mode){
		if(scene.name == "StartMenu"){
			PlayMusic(menuMusic, 0.5f);
		}else if(scene.name == "GameScene"){
			PlayMusic(dungeonMusic, 0.2f);
		}else if(scene.name == "Combat"){
			PlayMusic(combatMusic, 0.3f);
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

	protected override void OnEnable() {
		SceneManager.sceneLoaded += OnSceneLoadedMusic;
		base.OnEnable();
	}
	protected override void OnDisable() {
		SceneManager.sceneLoaded -= OnSceneLoadedMusic;
		base.OnDisable();
	}
	protected override void OnDestroy() {
		SceneManager.sceneLoaded -= OnSceneLoadedMusic;
		base.OnDestroy();
	}

}
