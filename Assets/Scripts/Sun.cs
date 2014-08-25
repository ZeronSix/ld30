using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {

	public GameObject captionPrefab;

	private StarSystemController starSystemController;

	public float galacticLightRange = 3.0f;
	public float systemLightRange = 300f;

	public bool connected = false;

	private TextMesh caption;

	public Vector3 savedPosition = new Vector3();

	void Start () {
		starSystemController = GameObject.FindWithTag ("StarSystemController").GetComponent<StarSystemController> ();

		GenerateName ();

		caption = ((GameObject)Instantiate (captionPrefab)).GetComponent<TextMesh>();
		caption.transform.parent = transform;
		caption.transform.localPosition = new Vector3 (0f, 0.6549172f, -0.5f);
		caption.transform.localScale = Vector3.one * 0.009f;
		caption.text = name;
		caption.name = "Caption";

		DontDestroyOnLoad (gameObject);
	}

	void OnMouseDown() {
		if (starSystemController.gameState == StarSystemController.ViewState.GALACTIC_VIEW) {
			starSystemController.SelectSystem(this);
		}
	}

	void Update () {
		caption.transform.rotation = Quaternion.identity;
		caption.transform.position = transform.position + new Vector3 (0f,1.4f,0f);

		foreach (Transform planet in transform) {
			if (planet.gameObject != caption.gameObject) {
				Planet planetComponent = planet.GetComponent<Planet>();
				//TODO: Slava, fix it plz
				if (starSystemController.gameState == StarSystemController.ViewState.SYSTEM_VIEW && starSystemController.selectedSystem == this) {
					planet.transform.localScale = Vector3.Lerp(planet.transform.localScale, Vector3.one * planetComponent.realScale, 10f * Time.deltaTime);
					
					planet.renderer.enabled = true;
					//planet.gameObject.GetComponentInChildren<TextMesh>().renderer.enabled = true;
					
					planetComponent.orbit.transform.localScale = 
						Vector3.Lerp(planetComponent.orbit.transform.localScale, planetComponent.orbitScale, 10f * Time.deltaTime);
				}
				else if (starSystemController.selectedSystem != this && starSystemController.gameState == StarSystemController.ViewState.SYSTEM_VIEW) {
					planet.transform.localScale = Vector3.Lerp(planet.transform.localScale, Vector3.zero, 10f * Time.deltaTime);
					
					planetComponent.orbit.transform.localScale = 
						Vector3.Lerp(planetComponent.orbit.transform.localScale, Vector3.zero, 10f * Time.deltaTime);
				}
				else if (starSystemController.gameState != StarSystemController.ViewState.SYSTEM_VIEW) {
					planet.transform.localScale = Vector3.Lerp(planet.transform.localScale, Vector3.zero, 10f * Time.deltaTime);
					
					planet.renderer.enabled = false;
					
					planetComponent.orbit.transform.localScale = 
						Vector3.Lerp(planetComponent.orbit.transform.localScale, planetComponent.orbitScale * 0.5f, 10f * Time.deltaTime);
				}
			}
		}	

		light.range = starSystemController.gameState == StarSystemController.ViewState.GALACTIC_VIEW ? galacticLightRange : systemLightRange;

		if (starSystemController.gameState == StarSystemController.ViewState.SYSTEM_VIEW && starSystemController.selectedSystem != this) {
			//transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 15f * Time.deltaTime);		
			renderer.enabled = false;
			light.enabled = false;
			caption.renderer.enabled = false;
		} else {
			//transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 15f * Time.deltaTime);	
			renderer.enabled = true;
			light.enabled = true;
			caption.renderer.enabled = true;
		}

		transform.RotateAround (transform.position, Vector3.forward, 9 * Time.deltaTime);
	}

	public void GenerateName() {
		int num = Random.Range(0, 25);
		char let = (char)('a' + num);
		
		name = ((char)('a' + num)).ToString() + "-" + Random.Range (0, 9).ToString() + Random.Range (0, 9).ToString();
		name = name.ToUpper ();
	}
}
