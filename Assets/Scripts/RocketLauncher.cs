using UnityEngine;
using System.Collections;

public class RocketLauncher : Weapon {

	public GameObject rocketPrefab;

	public float delay = 0.18f;

	void Start () {
	
	}

	void Update () {

	}

    public override void Shoot(Vector3 target)
    {
        StartCoroutine(SpawnRockets(-transform.forward + transform.parent.position));
    }

	public IEnumerator SpawnRockets(Vector3 position) {
		SpawnRocket (position, new Vector3(0.35f,0.1f,0f));
		yield return new WaitForSeconds (delay);
		SpawnRocket (position, new Vector3(-0.35f,0.1f,0f));
		yield return new WaitForSeconds (delay);
		SpawnRocket (position, new Vector3(0.35f,-0.25f,0f));
		yield return new WaitForSeconds (delay);
		SpawnRocket (position, new Vector3(-0.35f,-0.25f,0f));
		yield return new WaitForSeconds (delay);
	}

	Vector3 GetRandomOffset() {
		return new Vector3 (Random.Range (-0.07f, 0.07f), Random.Range (-0.07f, 0.07f), Random.Range (-0.07f, 0.07f));
	}

	public void SpawnRocket(Vector3 finalTarget, Vector3 offset) {
		Rocket newRocket = ((GameObject)Instantiate (rocketPrefab)).GetComponent<Rocket>();
		newRocket.transform.position = transform.position;
		newRocket.path.Add (transform.position + offset + new Vector3(0f,-0.35f,0f) + GetRandomOffset());
		newRocket.path.Add (finalTarget + GetRandomOffset());
		newRocket.transform.parent = transform;
	}
}
