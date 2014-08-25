using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rocket : MonoBehaviour {

	public List<Vector3> path = new List<Vector3>();

	public int currentPoint = 0;

	public float nextPointTreshold = 0.18f;

	public float rocketSpeed = 8f;

	private ParticleSystem particleSystem;

	void Start () {
		particleSystem = GetComponentsInChildren<ParticleSystem> (true) [0];
	}

	void Update () {
		Vector3 pos = path[path.Count-1]-transform.position;
		Quaternion newRot = Quaternion.LookRotation(pos);
		transform.rotation = Quaternion.Lerp(transform.rotation, newRot, 5f * Time.deltaTime);

		transform.position = Vector3.Lerp (transform.position, path[currentPoint], rocketSpeed * (currentPoint+1) * Time.deltaTime);
		if (Vector3.Distance (transform.position, path [currentPoint]) < nextPointTreshold) {
			if (currentPoint < path.Count-1) currentPoint++;
			else if (Vector3.Distance (transform.position, path [currentPoint]) < nextPointTreshold / 5f) {
				Explode();
			}
		}
	}	

	public void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.GetComponentInChildren<Unit>() != null) {
			Explode();	
		}
	}

	public void Explode() {
		particleSystem.gameObject.SetActive (true);
		particleSystem.gameObject.transform.parent = null;
		particleSystem.gameObject.transform.position = path[path.Count-1];
		particleSystem.gameObject.audio.Play ();
		Destroy (gameObject);
	}
}
