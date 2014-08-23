using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

	private GameController gameController;

	public float realScale = 1f;
	
	void Start () {
		gameController = GameObject.FindWithTag ("GameController").GetComponent<GameController> ();
	}
	
	void Update () {
		transform.RotateAround (transform.position, Vector3.forward, 9 * Time.deltaTime);
	}
}
