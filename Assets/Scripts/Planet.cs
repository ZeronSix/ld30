using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Linq;

public class Planet : MonoBehaviour {

	//private GameController gameController;

	public GameObject orbit;
	public Vector3 orbitScale;

	public float realScale = 1f;
	
	void Start () {
		//gameController = GameObject.FindWithTag ("GameController").GetComponent<GameController> ();
	}
	
	void Update () {
		transform.RotateAround (transform.position, Vector3.forward, 9 * Time.deltaTime);

		orbit.transform.RotateAround (orbit.transform.position, Vector3.forward, orbitScale.x * 3 * Time.deltaTime);
	}
}
