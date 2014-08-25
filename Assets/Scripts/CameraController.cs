using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float Speed;

    void Update()
    {
        if (Input.GetButton("DragMouse"))
        {
            UpdateMovement(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
        }

		if (GameObject.FindWithTag ("StarSystemController").GetComponent<StarSystemController> ().gameState == StarSystemController.GameState.SYSTEM_VIEW) {
			ZoomIn (GameObject.FindWithTag ("StarSystemController").GetComponent<StarSystemController> ().selectedSystem.transform.position);
		} 
		else {
			ZoomOut();
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
}
