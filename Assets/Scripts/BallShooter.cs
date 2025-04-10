using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    [Header("Disparo")]
    public float maxForce = 1000f;
    public GameObject arrowPrefab;

    [Header("Frenado dinámico")]
    public float slowDownMultiplier = 2f;
    public float minVelocity = 0.05f;
    public float shotDelay = 0.2f; // Tiempo mínimo tras el disparo para aplicar el frenado dinámico

    private Rigidbody rb;
    private Vector3 shootDirection;
    private float forceAmount;

    private bool isDragging = false;
    private GameObject arrowInstance;

    private float shotTimer = 10f; // Inicializamos en un valor alto para que no interfiera al inicio

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        if (arrowPrefab)
        {
            arrowInstance = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            arrowInstance.SetActive(false);
        }
    }

    void Update()
    {
        // Permitir input solo si la bola está prácticamente quieta
        if (rb.velocity.magnitude > 0.1f)
            return;

        // Si el usuario pulsa el botón izquierdo, iniciar el arrastre
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
        }

        // Mientras arrastra, calcular dirección y fuerza según el mouse
        if (Input.GetMouseButton(0) && isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, transform.position);

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                shootDirection = (transform.position - hitPoint).normalized;
                forceAmount = Mathf.Clamp(Vector3.Distance(hitPoint, transform.position), 0, 5f);

                // Mostrar flecha indicadora
                if (arrowInstance)
                {
                    arrowInstance.SetActive(true);
                    arrowInstance.transform.position = transform.position;
                    arrowInstance.transform.rotation = Quaternion.LookRotation(shootDirection);
                    arrowInstance.transform.localScale = new Vector3(0.1f, 0.1f, forceAmount); // La longitud indica la potencia
                }
            }
        }

        // Al soltar el botón, dispara la bola y reinicia la flecha
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            rb.AddForce(shootDirection * forceAmount * maxForce);
            isDragging = false;
            shotTimer = 0;  // Reiniciamos el timer para no aplicar el frenado inmediato

            if (arrowInstance)
                arrowInstance.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        // Actualizamos el timer tras el disparo
        shotTimer += Time.fixedDeltaTime;

        // Durante el tiempo de retardo tras disparar, no aplicamos frenado extra
        if (shotTimer < shotDelay)
            return;

        float speed = rb.velocity.magnitude;

        // Si la velocidad es muy baja, detenemos por completo la bola
        if (speed < minVelocity)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.Sleep();
            return;
        }

        // Aplicar frenado dinámico solo cuando la bola va lenta (menor a 1 m/s)
        if (speed < 1f)
        {
            // Escalar el slowdown: cuando speed=1, slowdown=0; cuando speed -> 0, slowdown -> 1
            float slowdown = Mathf.Lerp(0f, 1f, 1f - speed);
            rb.velocity *= 1f - (Time.fixedDeltaTime * slowdown * slowDownMultiplier);
        }
    }
}
