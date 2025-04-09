using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public float maxForce = 1000f;
    public GameObject arrowPrefab;

    private Rigidbody rb;
    private Vector3 shootDirection;
    private float forceAmount;

    private bool isDragging = false;
    private GameObject arrowInstance;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (arrowPrefab)
        {
            arrowInstance = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            arrowInstance.SetActive(false);
        }
    }

    void Update()
    {
        if (rb.velocity.magnitude > 0.1f) return;
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, transform.position);

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                shootDirection = (transform.position - hitPoint).normalized;
                forceAmount = Mathf.Clamp(Vector3.Distance(hitPoint, transform.position), 0, 5f);

                // Mostrar flecha
                if (arrowInstance)
                {
                    arrowInstance.SetActive(true);
                    arrowInstance.transform.position = transform.position;
                    arrowInstance.transform.rotation = Quaternion.LookRotation(shootDirection);
                    arrowInstance.transform.localScale = new Vector3(0.1f, 0.1f, forceAmount); // alargar con fuerza
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            rb.AddForce(shootDirection * forceAmount * maxForce);
            isDragging = false;

            if (arrowInstance)
                arrowInstance.SetActive(false);
        }
    }
    void FixedUpdate()
    {
        if (rb.velocity.magnitude < 0.35f && rb.velocity.magnitude > 0)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.Sleep(); // Opcional: detiene la simulación hasta que reciba fuerza
        }
    }
}
