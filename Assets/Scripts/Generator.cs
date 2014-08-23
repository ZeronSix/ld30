using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generator : MonoBehaviour {

	public GameObject sunPrefab;
	public GameObject planetPrefab;

	public List<List<GameObject>> systems = new List<List<GameObject>>();

	public float minDistance = 2f;
	public float maxDistance = 3f;

	public float minBodySize = 0.6f;
	public float maxBodySize = 0.75f;

	public int minBodies = 3;
	public int maxBodies = 5;

	Vector3 RandomCirclePosition(float radius, Vector3 center) {
		float angle = Random.value * 360;
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
				newBody = (GameObject)Instantiate(planetPrefab, RandomCirclePosition((i * distance) + distance, rootPosition), Quaternion.identity);

				newBody.name = "Planet " + i.ToString();
				newBody.transform.parent = newSystem[0].transform;
				newBody.GetComponent<Planet>().realScale = Random.Range(minBodySize, maxBodySize);
				newBody.transform.localScale = Vector3.zero;
				//newBody.SetActive(false);
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
