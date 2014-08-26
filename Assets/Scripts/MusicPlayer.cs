using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
class MusicPlayer : MonoBehaviour {
	private static MusicPlayer instance = null;
	public static MusicPlayer Instance { get { return instance; } }

	public AudioClip idleMusic;
	public AudioClip battleMusic;
	
	void Awake() {
		if (instance != null){
			return;
		} 
		else {
			instance = this;
			DontDestroyOnLoad(gameObject);
			PlayIdleTheme();
		}
	}

	void OnLevelWasLoaded(int level) {
		if (Application.loadedLevelName == "Battle" && (audio.clip != battleMusic)) PlayBattleTheme ();
		else if (audio.clip != idleMusic) PlayIdleTheme();
	}
	
	public void PlayBattleTheme() {
		audio.clip = battleMusic;
		audio.Play();
	}

	public void PlayIdleTheme() {
		audio.clip = idleMusic;
		audio.Play();
	}
	
	public void Stop() {
		audio.Stop();
	}
}