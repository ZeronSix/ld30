using UnityEngine;
using System.Collections;

public class GeneratorEnabler : MonoBehaviour {

	void Start() {
		DontDestroyOnLoad (gameObject);
		//DontDestroyOnLoad (GameObject.FindWithTag("StarSystemController"));
	}

	void OnLevelWasLoaded(int level) {
		if (level == 1) {
			GetComponent<GalacticGenerator>().enabled = true;
		}
	}
}
