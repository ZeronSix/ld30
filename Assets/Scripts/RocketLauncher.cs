using UnityEngine;
using System.Collections;

public class RocketSauce : MonoBehaviour {

	public GameObject rocketPrefab;

	void Start () {
	
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			//SpawnRockets();
		}
	}

	public IEnumerator SpawnRockets(Vector3 position) {
		SpawnRocket ();
		yield return new WaitForSeconds (1.0f);
	}

	public void SpawnRocket() {
		Rocket newRocket = ((GameObject)Instantiate (rocketPrefab)).GetComponent<Rocket>();
	}
}
