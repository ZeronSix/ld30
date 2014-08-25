using UnityEngine;
using System.Collections;
using System.Timers;
public class LaserGun : Weapon {

	public float laserTime = 1.0f;

	public Color laserBeamColor = Color.red;

	private LineRenderer lineRenderer;
	
	void Start () {
		lineRenderer = GetComponent<LineRenderer> ();

		lineRenderer.enabled = false;
	}

	void Update () {
		lineRenderer.material.color = laserBeamColor;

		if (Input.GetKeyDown (KeyCode.Space)) {
			Shoot (new Vector3(1,1,1));		
		}
	}

	public override void Shoot(Vector3 target) {
		lineRenderer.enabled = true;
		lineRenderer.SetPosition (0, transform.position);
		lineRenderer.SetPosition (1, target);
		Invoke ("HideLaser", laserTime);
	}

	private void HideLaser() {
		lineRenderer.enabled = false;
	}
}
