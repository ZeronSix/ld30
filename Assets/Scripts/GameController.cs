using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public enum GameState {
		GALACTIC_VIEW, SYSTEM_VIEW
	}

	public GameState gameState = GameState.GALACTIC_VIEW;

	public Sun selectedSystem;
	
	void Start () {

	}

	public void SelectSystem(Sun sun) {
		gameState = GameState.SYSTEM_VIEW;
		selectedSystem = sun;
	}

	public void UnselectSystem() {
		gameState = GameState.GALACTIC_VIEW;
		selectedSystem = null;
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) && gameState == GameController.GameState.SYSTEM_VIEW) UnselectSystem();
	}
}
