using UnityEngine;
using System.Collections;

public class RocketLauncher : MonoBehaviour {

	public GameObject rocketPrefab;

	void Start () {
	
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			StartCoroutine(SpawnRockets(transform.forward));
		}
	}

	public IEnumerator SpawnRockets(Vector3 position) {
		SpawnRocket (position + new Vector3(0.25f,0.25f,0f));
		yield return new WaitForSeconds (0.1f);
		SpawnRocket (position + new Vector3(-0.25f,0.25f,0f));
		yield return new WaitForSeconds (0.1f);
		SpawnRocket (position + new Vector3(0.25f,-0.25f,0f));
		yield return new WaitForSeconds (0.1f);
		SpawnRocket (position + new Vector3(-0.25f,-0.25f,0f));
		yield return new WaitForSeconds (0.1f);
	}

	public void SpawnRocket(Vector3 finalTarget) {
		Rocket newRocket = ((GameObject)Instantiate (rocketPrefab)).GetComponent<Rocket>();
		newRocket.path.Add (transform.position);
		newRocket.path.Add (finalTarget - transform.position + new Vector3(0f,0.25f,0f));
		newRocket.path.Add (finalTarget + new Vector3(0f,0.25f,0f));
	}
}
