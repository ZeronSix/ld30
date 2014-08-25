using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float Speed;

	public float DefaultFieldOfView = 80;
	public float SpecialFieldOfView = 80;

    void Update()
    {
        if (Input.GetButton("DragMouse"))
        {
            UpdateMovement(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
        }

		if (GameObject.FindWithTag ("StarSystemController").GetComponent<StarSystemController> ().gameState == StarSystemController.GameState.SYSTEM_VIEW) {
			ZoomIn (GameObject.FindWithTag ("StarSystemController").GetComponent<StarSystemController> ().selectedSystem.transform.position);
			LerpToDefaultFieldOfView();
		} 
		else {
			ZoomOut();
			LerpToSpecialFieldOfView();
		}
    }

    private void UpdateMovement(Vector2 input)
    {
        transform.Translate(-input * Speed * Time.deltaTime, Space.Self);
    }

	public void ZoomIn(Vector3 point, float distance = -13f)
	{
		transform.position = Vector3.Lerp (transform.position, new Vector3(point.x, point.y, distance), Speed * Time.deltaTime * 0.5f);
	}

	public void ZoomOut()
	{
		transform.position = new Vector3 (transform.position.x, transform.position.y, Mathf.Lerp (transform.position.z, -15f, Speed * Time.deltaTime));
	}

	public void LerpToDefaultFieldOfView() {
		camera.fieldOfView = Mathf.Lerp (camera.fieldOfView, DefaultFieldOfView, 10f * Time.deltaTime);
	}

	public void LerpToSpecialFieldOfView() {
		camera.fieldOfView = Mathf.Lerp (camera.fieldOfView, SpecialFieldOfView, 10f * Time.deltaTime);
	}
}
