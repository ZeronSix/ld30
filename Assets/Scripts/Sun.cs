using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {

	private StarSystemController starSystemController;

	public float galacticLightRange = 3.0f;
	public float systemLightRange = 300f;

	void Start () {
		starSystemController = GameObject.FindWithTag ("StarSystemController").GetComponent<StarSystemController> ();


	}

	void OnMouseDown() {
		if (starSystemController.gameState == StarSystemController.GameState.GALACTIC_VIEW) {
			starSystemController.SelectSystem(this);
		}
	}

	void Update () {
		foreach (Transform planet in transform) {
			Planet planetComponent = planet.GetComponent<Planet>();
			//TODO: Slava, fix it plz
			if (starSystemController.gameState == StarSystemController.GameState.SYSTEM_VIEW && starSystemController.selectedSystem == this) {
				planet.transform.localScale = Vector3.Lerp(planet.transform.localScale, Vector3.one * planetComponent.realScale, 10f * Time.deltaTime);

				planet.renderer.enabled = true;

				planetComponent.orbit.transform.localScale = 
					Vector3.Lerp(planetComponent.orbit.transform.localScale, planetComponent.orbitScale, 10f * Time.deltaTime);
			}
			else if (starSystemController.selectedSystem != this && starSystemController.gameState == StarSystemController.GameState.SYSTEM_VIEW) {
				planet.transform.localScale = Vector3.Lerp(planet.transform.localScale, Vector3.zero, 10f * Time.deltaTime);

				planetComponent.orbit.transform.localScale = 
					Vector3.Lerp(planetComponent.orbit.transform.localScale, Vector3.zero, 10f * Time.deltaTime);
			}
			else if (starSystemController.gameState != StarSystemController.GameState.SYSTEM_VIEW) {
				planet.transform.localScale = Vector3.Lerp(planet.transform.localScale, Vector3.zero, 10f * Time.deltaTime);

				planet.renderer.enabled = false;

				planetComponent.orbit.transform.localScale = 
					Vector3.Lerp(planetComponent.orbit.transform.localScale, planetComponent.orbitScale * 0.5f, 10f * Time.deltaTime);
			}
		}	

		light.range = starSystemController.gameState == StarSystemController.GameState.GALACTIC_VIEW ? galacticLightRange : systemLightRange;

		if (starSystemController.gameState == StarSystemController.GameState.SYSTEM_VIEW && starSystemController.selectedSystem != this) {
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
