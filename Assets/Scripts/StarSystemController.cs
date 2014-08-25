using UnityEngine;
using System.Collections;

public class StarSystemController : MonoBehaviour {

	public enum ViewState {
		GALACTIC_VIEW, SYSTEM_VIEW
	}
	
	public ViewState gameState = ViewState.GALACTIC_VIEW;
	
	public Sun selectedSystem;

	public void SelectSystem(Sun sun) {
		gameState = ViewState.SYSTEM_VIEW;
		selectedSystem = sun;
	}
	
	public void UnselectSystem() {
		gameState = ViewState.GALACTIC_VIEW;
		selectedSystem = null;
	}

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape) && gameState == StarSystemController.ViewState.SYSTEM_VIEW) UnselectSystem();
	}
}
