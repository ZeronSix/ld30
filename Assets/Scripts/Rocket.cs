using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rocket : MonoBehaviour {

	public List<Vector3> path = new List<Vector3>();

	public int currentPoint = 0;

	public float nextPointTreshold = 0.2f;

	public float rocketSpeed = 4f;

	void Start () {
	}

	void Update () {
		Vector3 pos = path[path.Count-1]-transform.position;
		Quaternion newRot = Quaternion.LookRotation(pos);
		transform.rotation = Quaternion.Lerp(transform.rotation, newRot, 10f * Time.deltaTime);

		transform.position = Vector3.Lerp (transform.position, path[currentPoint], rocketSpeed * Time.deltaTime);
		if (Vector3.Distance (transform.position, path [currentPoint]) < nextPointTreshold) {
			if (currentPoint < path.Count-1) currentPoint++;
			else if (Vector3.Distance (transform.position, path [currentPoint]) < nextPointTreshold / 5f) {
				Explode();
			}
		}
	}	

	public void OnCollisionEnter(Collision collision) {
		if (collision.gameObject != transform.parent.parent.gameObject) {
			Explode();		
		}
	}

	public void Explode() {
		GetComponentInChildren<ParticleEmitter> ().emit = true;
		GetComponentInChildren<ParticleAnimator> ().autodestruct = true;
		GetComponentInChildren<ParticleEmitter> ().transform.parent = null;
		Destroy (gameObject);
	}
}
