using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {

	private GameController gameController;

	public float galacticLightRange = 3.0f;
	public float systemLightRange = 300f;

	void Start () {
		gameController = GameObject.FindWithTag ("GameController").GetComponent<GameController> ();


	}

	void OnMouseDown() {
		if (gameController.gameState == GameController.GameState.GALACTIC_VIEW) {
			gameController.SelectSystem(this);
		}
	}

	void Update () {
		foreach (Transform planet in transform) {
			Debug.Log(planet.name);
			if (gameController.gameState == GameController.GameState.SYSTEM_VIEW && gameController.selectedSystem == this) {
				planet.transform.localScale = Vector3.Lerp(planet.transform.localScale, Vector3.one * planet.GetComponent<Planet>().realScale, 10f * Time.deltaTime);
			}
			else {
				planet.transform.localScale = Vector3.Lerp(planet.transform.localScale, Vector3.zero, 10f * Time.deltaTime);
			}
		}	

		light.range = gameController.gameState == GameController.GameState.GALACTIC_VIEW ? galacticLightRange : systemLightRange;

		if (gameController.gameState == GameController.GameState.SYSTEM_VIEW && gameController.selectedSystem != this) {
			//transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 15f * Time.deltaTime);		
			renderer.enabled = false;
			light.enabled = false;
		} else {
			//transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 15f * Time.deltaTime);	
			renderer.enabled = true;
			light.enabled = true;
		}

		transform.RotateAround (transform.position, Vector3.forward, 9 * Time.deltaTime);
	}
}
