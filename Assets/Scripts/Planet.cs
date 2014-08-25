using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Linq;

public class Planet : MonoBehaviour {

	public GameObject orbit;
	public Vector3 orbitScale;

	public float realScale = 1f;

	void Start () {
	}
	
	void Update () {
		transform.RotateAround (transform.position, Vector3.forward, 9 * Time.deltaTime);
		transform.RotateAround (transform.parent.position, Vector3.forward, 0.3f * 25f * Time.deltaTime / (Vector3.Distance(transform.position, transform.parent.position) * 0.05f));

		orbit.transform.RotateAround (orbit.transform.position, Vector3.forward, orbitScale.x * 3 * Time.deltaTime);
	}

    void OnMouseOver()
    {
        if (Input.GetButtonDown("Action"))
        {
//            if (gc.SelectedUnit)
//                gc.SelectedUnit.Target = transform;
        }
    }
}
