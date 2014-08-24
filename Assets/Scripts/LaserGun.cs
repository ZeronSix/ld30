using UnityEngine;
using System.Collections;
using System.Timers;
public class LaserGun : MonoBehaviour {

	public float laserTime = 1.0f;

	public Color laserBeamColor = Color.red;

	private LineRenderer lineRenderer;
	
	void Start () {
		lineRenderer = GetComponent<LineRenderer> ();

		lineRenderer.enabled = false;
	}

	void Update () {
		lineRenderer.material.color = laserBeamColor;

		//if (Input.GetKeyDown (KeyCode.Space)) Attack (new Vector3(10f, 0f, 10f));
	}

	public void Attack(Vector3 target) {
		lineRenderer.enabled = true;
		lineRenderer.SetPosition (1, target);
		Invoke ("HideLaser", laserTime);
	}

	private void HideLaser() {
		lineRenderer.enabled = false;
	}
}
