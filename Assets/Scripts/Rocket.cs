using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rocket : MonoBehaviour {

	public List<Vector3> path = new List<Vector3>();

	public int currentPoint = 0;

	public float nextPointTreshold = 0.2f;

	void Start () {
	}

	void Update () {
		Vector3 relativePos = path[currentPoint] - transform.position;
		Quaternion rotation = Quaternion.LookRotation(relativePos);
		transform.rotation = rotation;

		transform.position = Vector3.Lerp (transform.position, path[currentPoint], 5f * Time.deltaTime);
		if (Vector3.Distance (transform.position, path [currentPoint]) < nextPointTreshold) {
			if (currentPoint < path.Count-1) currentPoint++;
			else {
				Explode();
			}
		}
	}	

	public void Explode() {
		Destroy (gameObject);
	}
}
