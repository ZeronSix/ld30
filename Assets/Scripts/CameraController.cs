using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float Speed;

	public float DefaultZ = -25f;
	public float SpecialZ = -25f;
	public Vector2 Center = new Vector2();

    void Update()
    {
        if (Input.GetButton("DragMouse"))
        {
            UpdateMovement(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
        }

		if (GameObject.FindWithTag ("StarSystemController").GetComponent<StarSystemController> ().gameState == StarSystemController.ViewState.SYSTEM_VIEW) {
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

	public void ZoomIn(Vector3 point, float distance = -20f)
	{
		transform.position = Vector3.Lerp (transform.position, new Vector3(point.x, point.y, distance), Speed * Time.deltaTime * 0.5f);
	}

	public void ZoomOut()
	{
		transform.position = new Vector3 (Center.x, Center.y, Mathf.Lerp (transform.position.z, SpecialZ, Speed * Time.deltaTime));
	}
}
