using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // la bola
    public Vector3 offset = new Vector3(0, 3, -6);
    public float rotationSpeed = 5f;
    public float zoomSpeed = 2f;
    public float minY = 1f;
    public float maxY = 85f;

    private float currentX = 0f;
    private float currentY = 30f;
    private float distance = 6f;

    void LateUpdate()
    {
        if (Input.GetMouseButton(1)) // botón derecho
        {
            currentX += Input.GetAxis("Mouse X") * rotationSpeed;
            currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;
            currentY = Mathf.Clamp(currentY, minY, maxY);
        }

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 dir = new Vector3(0, 0, -distance);
        transform.position = target.position + rotation * dir;
        transform.LookAt(target.position);
    }
}

