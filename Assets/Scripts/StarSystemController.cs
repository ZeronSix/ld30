using UnityEngine;
using System.Collections;

public class StarSystemController : MonoBehaviour {

	public enum ViewState {
		GALACTIC_VIEW, SYSTEM_VIEW
	}
	
	public ViewState gameState = ViewState.GALACTIC_VIEW;
	
	public Sun selectedSystem;
	public int lastSelectedSystemNumber;

	public void SelectSystem(Sun sun) {
		gameState = ViewState.SYSTEM_VIEW;
		selectedSystem = sun;
	}
	
	public void UnselectSystem() {
		gameState = ViewState.GALACTIC_VIEW;
		selectedSystem = null;
	}
}
