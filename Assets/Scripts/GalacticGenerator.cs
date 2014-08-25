using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GalacticGenerator : MonoBehaviour {

	public GameObject sunPrefab;
	public GameObject orbitPrefab;
	public List<GameObject> planets = new List<GameObject> ();

	public List<List<GameObject>> systems = new List<List<GameObject>>();

	public float minDistance = 2f;
	public float maxDistance = 3f;

	public float minBodySize = 0.6f;
	public float maxBodySize = 0.75f;

	public int minBodies = 3;
	public int maxBodies = 5;

	public int maxSystems = 6;

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
			if (i != 0) {
				newBody = (GameObject)Instantiate(planets[i-1], CirclePosition((i * distance) + distance, rootPosition, Random.value * 360), Quaternion.identity);

				newBody.name = "Planet " + i.ToString();
				newBody.transform.parent = newSystem[0].transform;
				newBody.GetComponent<Planet>().realScale = Random.Range(minBodySize, maxBodySize);
				newBody.transform.localScale = Vector3.zero;

				GameObject orbit = (GameObject)Instantiate(orbitPrefab);
				orbit.transform.parent = GameObject.FindWithTag("Orbits").transform;
				orbit.transform.position = rootPosition;

				newBody.GetComponent<Planet>().orbit = orbit;
				newBody.GetComponent<Planet>().orbitScale = Vector3.one * ((i * distance) + distance) * 0.21f;

				orbit.transform.localScale = newBody.GetComponent<Planet>().orbitScale * 0.5f;
			}
			else {
				newBody = (GameObject)Instantiate(sunPrefab, rootPosition, Quaternion.identity);

				newBody.name = "Sun " + (systems.Count+1).ToString();
				newBody.transform.localScale = Vector3.one * 2f;
			}

			newSystem.Add (newBody);
		}

		systems.Add (newSystem);
	}

	void OnGUI() {
		if (GameObject.FindWithTag("StarSystemController").GetComponent<StarSystemController>().gameState == StarSystemController.ViewState.GALACTIC_VIEW && maxSystems > systems.Count) {
			if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height - 170, 200, 100), "Generate System")) {
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
	}
	
	void Start () {
		GenerateSystem (Vector3.zero);
	}

	void Update () {

	}
}
