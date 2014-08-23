using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generator : MonoBehaviour {

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

	GameObject GetRandomPlanet() {
		return planets[Random.Range(0, planets.Count)];
	}

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
				newBody = (GameObject)Instantiate(GetRandomPlanet(), CirclePosition((i * distance) + distance, rootPosition, Random.value * 360), Quaternion.identity);

				newBody.name = "Planet " + i.ToString();
				newBody.transform.parent = newSystem[0].transform;
				newBody.GetComponent<Planet>().realScale = Random.Range(minBodySize, maxBodySize);
				newBody.transform.localScale = Vector3.zero;

				GameObject orbit = (GameObject)Instantiate(orbitPrefab);
				orbit.transform.parent = null;
				orbit.transform.position = rootPosition;
				orbit.transform.localScale = Vector3.one * ((i * distance) + distance) * 0.21f;

				newBody.GetComponent<Planet>().orbit = orbit;
				newBody.GetComponent<Planet>().orbitScale = Vector3.one * ((i * distance) + distance) * 0.21f;
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
	
	void Start () {
		GenerateSystem (Vector3.zero);
		GenerateSystem (new Vector3(8f, 0f, 0f));
		GenerateSystem (new Vector3(4f, 5f, 0f));
		GenerateSystem (new Vector3(-4f, 5f, 0f));
		GenerateSystem (new Vector3(4f, -5f, 0f));
	}

	void Update () {
		foreach (List<GameObject> system in systems) {
			for (int i = 1; i < system.Count; i++) {
				system[i].transform.RotateAround (system[0].transform.position, Vector3.forward, i * 0.3f * 25 * Time.deltaTime);
				Debug.DrawLine(system[i].transform.position, system[0].transform.position);
			}
		}
	}
}
