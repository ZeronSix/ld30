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
    }

    private void UpdateMovement(Vector2 input)
    {
        transform.Translate(input * Speed, Space.Self);
    }
}
