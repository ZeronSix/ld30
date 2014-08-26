using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GalacticGenerator : MonoBehaviour {

	public GameObject sunPrefab;
	public GameObject orbitPrefab;
	public List<GameObject> planets = new List<GameObject> ();

	protected List<List<GameObject>> systems = new List<List<GameObject>>();
	protected Transform orbits;

	public float minDistance = 2f;
	public float maxDistance = 3f;

	public float minBodySize = 0.6f;
	public float maxBodySize = 0.75f;

	public int minBodies = 3;
	public int maxBodies = 5;

	public int maxSystems = 6;

	public GUISkin skin;

	Vector3 CirclePosition(float radius, Vector3 center, float angle) {
		Vector3 position = Vector3.zero;
		position.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
		position.y = center.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
		position.z = center.z;
		return position;
	}

	void GenerateSystem(Vector3 rootPosition) {
		List<GameObject> newSystem = new List<GameObject> ();

		int bodies = Random.Range (Mathf.Max(3, minBodies), maxBodies);
		float distance = Random.Range (minDistance, maxDistance);

		for (int i = 0; i < bodies; i++) {
			GameObject newBody = null;
			if (i != 0) { //Planets
				newBody = (GameObject)Instantiate(planets[i-1], CirclePosition((i * distance) + distance, rootPosition, Random.value * 360), Quaternion.identity);

				newBody.name = "Planet " + i.ToString();
				newBody.transform.parent = newSystem[0].transform;
				newBody.GetComponent<Planet>().realScale = Random.Range(minBodySize, maxBodySize);
				newBody.transform.localScale = Vector3.zero;

				GameObject orbit = (GameObject)Instantiate(orbitPrefab);
				orbit.transform.parent = GameObject.FindWithTag ("Orbits").transform;
				orbit.transform.position = rootPosition;

				newBody.GetComponent<Planet>().orbit = orbit;
				newBody.GetComponent<Planet>().orbitScale = Vector3.one * ((i * distance) + distance) * 0.21f;

				orbit.transform.localScale = newBody.GetComponent<Planet>().orbitScale * 0.5f;
			}
			else { //Star
				newBody = (GameObject)Instantiate(sunPrefab, rootPosition, Quaternion.identity);

				newBody.name = "Sun " + (systems.Count+1).ToString();
				newBody.transform.localScale = Vector3.one * 2f;

				DontDestroyOnLoad(newBody);
			}

			newSystem.Add (newBody);
		}

		systems.Add (newSystem);
	}

	void OnGUI() {
		GUI.skin = skin;
		if (GameObject.FindWithTag("StarSystemController").GetComponent<StarSystemController>().gameState == StarSystemController.ViewState.GALACTIC_VIEW && maxSystems > systems.Count) {
			if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height - 100, 200, 75), "SCAN FOR SYSTEM")) {
				Vector3 newPosition = new Vector3();
				bool systemGenerated = false;
				bool noConflicts = true;
				Vector3 center = new Vector3();

				while (!systemGenerated) {
					noConflicts = true;
					center = new Vector3();

					float x = Random.Range(0.05f, 0.8f);
					float y = Random.Range(0.05f, 0.8f);
					newPosition = new Vector3(x, y, -Camera.main.transform.position.z);
					newPosition = Camera.main.ViewportToWorldPoint(newPosition);

					foreach (List<GameObject> system in systems) {
						center += system[0].transform.position;
						if (Vector3.Distance(system[0].transform.position, newPosition) < 4f * maxDistance) {
							noConflicts = false;
						}
					}

					if (noConflicts) {
						GenerateSystem (newPosition);
						systemGenerated = true;
						break;
					}
				}

				center += newPosition;
				center /= systems.Count;

				GameObject.FindWithTag("MainCamera").GetComponent<CameraController>().SpecialZ = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>().DefaultZ - (systems.Count);
				GameObject.FindWithTag("MainCamera").GetComponent<CameraController>().Center = center;
			}
		}
		else if (GameObject.FindWithTag("StarSystemController").GetComponent<StarSystemController>().gameState == StarSystemController.ViewState.SYSTEM_VIEW &&
		         !GameObject.FindWithTag("StarSystemController").GetComponent<StarSystemController>().selectedSystem.connected) {
			if (GUI.Button (new Rect (Screen.width / 2 - 205, Screen.height - 100, 200, 75), "START")) {
				foreach (List<GameObject> system in systems) {
					system[0].GetComponent<Sun>().savedPosition = system[0].transform.position;

					if (system[0].GetComponent<Sun>() != GameObject.FindWithTag("StarSystemController").GetComponent<StarSystemController>().selectedSystem) {
						//system[0].SetActive(false);
//						system[0].renderer.enabled = false;
						foreach (var p in system[0].GetComponentsInChildren<Planet>()) {
							p.gameObject.SetActive(false);
						}
						system[0].gameObject.layer = LayerMask.NameToLayer("Invisible");
						foreach (Transform body in system[0].transform) {
							body.gameObject.layer = LayerMask.NameToLayer("Invisible");
						}
					}
					else {
						system[0].GetComponent<Sun>().enabled = false;

						foreach (TextMesh caption in system[0].GetComponentsInChildren<TextMesh>()) {
							caption.renderer.enabled = false;
						}

						system[0].transform.position = new Vector3(10f, 10f, 1.5f);
						foreach (Transform child in system[0].transform) {
							if (child.gameObject.name != "Caption") {
								child.gameObject.GetComponent<Planet>().enabled = false;
							}
						}
					}
				}
				foreach (GameObject orbit in GameObject.FindGameObjectsWithTag("Orbit")) {
					orbit.renderer.enabled = false;
				}
				//gameObject.SetActive(false);
				this.enabled = false;

				//GameObject.FindWithTag("StarSystemController").GetComponent<StarSystemController>().selectedSystem = null;
				GameObject.FindWithTag("StarSystemController").GetComponent<StarSystemController>().gameState = StarSystemController.ViewState.GALACTIC_VIEW;
				Application.LoadLevel("Battle");
			}

			if (GUI.Button (new Rect (Screen.width / 2 + 5, Screen.height - 100, 200, 75), "BACK")) {
				GameObject.FindWithTag("StarSystemController").GetComponent<StarSystemController>().UnselectSystem();
			}
		}
		else if (GameObject.FindWithTag("StarSystemController").GetComponent<StarSystemController>().gameState == StarSystemController.ViewState.SYSTEM_VIEW && 
		         GameObject.FindWithTag("StarSystemController").GetComponent<StarSystemController>().selectedSystem.connected) {
			if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height - 100, 200, 75), "BACK")) {
				GameObject.FindWithTag("StarSystemController").GetComponent<StarSystemController>().UnselectSystem();
			}
		}
	}

	void Awake() {
		foreach (var sun in Resources.FindObjectsOfTypeAll<Sun>()) {
			sun.gameObject.SetActive(true);
		}

		foreach (var system in systems) {
			system[0].SetActive(true);
			system[0].GetComponent<Sun>().enabled = true;
			foreach (var body in system) {
				body.SetActive(true);
				foreach (var r in body.GetComponentsInChildren<Renderer>()) {
					r.enabled = true;
				}
			}
		}
	}

	void OnLevelWasLoaded(int level) {
		if (level == 1) {
			if (orbits == null) {
				orbits = GameObject.Find ("Orbits").transform;
			}
			foreach (Transform orbit in orbits) {
				orbit.renderer.enabled = true;
			}

			foreach (var system in systems) {
				system[0].GetComponent<Sun>().ReturnToSavedPosition();
				system[0].gameObject.layer = LayerMask.NameToLayer("Default");
				foreach (Transform body in system[0].transform) {
					body.gameObject.layer = LayerMask.NameToLayer("Default");
				}
				foreach (var r in system[0].gameObject.GetComponentsInChildren<MonoBehaviour>()) {
					r.gameObject.SetActive(true);
					r.enabled = true;
				}
				foreach (Transform body in system[0].transform) {
					foreach (var r in body.gameObject.GetComponentsInChildren<MonoBehaviour>()) {
						r.gameObject.SetActive(true);
						r.enabled = true;
					}
				}
			}

			foreach (var planet in Resources.FindObjectsOfTypeAll<Planet>()) {
				planet.gameObject.SetActive(true);
			}

			foreach (var pt in GameObject.FindGameObjectsWithTag("Planet")) {
				pt.SetActive(true);
				foreach (Transform child in pt.transform) {
					child.gameObject.renderer.enabled = false;
				}
			}

			Vector3 center = new Vector3(0f,0f);
			foreach (var system in systems) {
				center += system[0].transform.position;
			}
			if (systems.Count != 0) center /= systems.Count;
			
			Camera.main.gameObject.GetComponent<CameraController>().SpecialZ = Camera.main.gameObject.GetComponent<CameraController>().DefaultZ - (systems.Count);
			Camera.main.gameObject.GetComponent<CameraController>().Center = center;
		}
	}
	
	void Start () {
		DontDestroyOnLoad (GameObject.FindWithTag("Orbits"));
		GenerateSystem (Vector3.zero);
		systems [0] [0].GetComponent<Sun> ().connected = true;
	}

	void Update () {

	}
}
