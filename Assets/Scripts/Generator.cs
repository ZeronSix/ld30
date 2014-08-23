using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generator : MonoBehaviour {

	public GameObject planetPrefab;

	public List<List<GameObject>> systems = new List<List<GameObject>>();

	public float minDistance = 2f;
	public float maxDistance = 3f;

	public float minBodySize = 0.8f;
	public float maxBodySize = 1.1f;

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

		int bodies = Random.Range (3, 6);
		float distance = Random.Range (minDistance, maxDistance);

		for (int i = 0; i < bodies; i++) {
			GameObject newBody = (GameObject)Instantiate(planetPrefab, RandomCirclePosition(i * distance, Vector3.zero), Quaternion.identity);
			if (i != 0) {
				newBody.transform.parent = newSystem[0].transform;
				newBody.transform.localScale *= Random.Range(minBodySize, maxBodySize);
			}

			newSystem.Add (newBody);
		}

		newSystem [0].transform.position = rootPosition;

		systems.Add (newSystem);
	}
	
	void Start () {
		GenerateSystem (Vector3.zero);
		GenerateSystem (new Vector3(15f, 0f, 0f));
	}

	void Update () {
		foreach (List<GameObject> system in systems) {
			for (int i = 1; i < system.Count; i++) {
				system[i].transform.RotateAround (system[0].transform.position, Vector3.forward, 25 * Time.deltaTime);
			}
		}
	}
}
