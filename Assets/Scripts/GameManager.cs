using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Opcional, si deseas reiniciar o cambiar escenas

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Configuración de hoyos")]
    // Arreglo de puntos de spawn para cada hoyo (coloca objetos vacíos en la escena en las posiciones de inicio de cada hoyo)
    public Transform[] holeSpawnPoints;
    [Header("Referencia a la bola")]
    public GameObject ball;

    [Header("UI durante el juego")]
    public Text holeText;                // Muestra el número de hoyo actual
    public Text currentHoleStrokeText;   // Tiros para el hoyo actual
    public Text totalStrokeText;         // Tiros totales del juego

    [Header("UI - Pantalla final")]
    public GameObject finalScreenPanel;  // Panel que se mostrará al finalizar el último hoyo
    public Text finalTotalStrokeText;    // Texto para mostrar los tiros totales en la pantalla final
    public Text finalHoleText;           // Texto para mostrar el número de hoyo final (opcional)

    // Variables de control
    private int currentHoleIndex = 0;    // Índice del hoyo actual (0 basado)
    private int currentHoleStrokes = 0;  // Tiros en el hoyo actual
    private int totalStrokes = 0;        // Tiros acumulados en todos los hoyos

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // Al iniciar, se esconde el panel final (si está asignado)
        if (finalScreenPanel)
            finalScreenPanel.SetActive(false);

        RespawnBall();
        UpdateUI();
    }

    // Método para registrar un tiro (invocado desde el script de disparo, por ejemplo en BallShooter)
    public void RegisterShot()
    {
        currentHoleStrokes++;
        totalStrokes++;
        UpdateUI();
    }

    // Llamado cuando la bola entra en el hoyo
    public void HoleCompleted()
    {
        Debug.Log("Hoyo completado: " + (currentHoleIndex + 1));

        // Si es el último hoyo...
        if (currentHoleIndex >= holeSpawnPoints.Length - 1)
        {
            ShowFinalScreen();
        }
        else
        {
            // Avanza al siguiente hoyo
            currentHoleIndex++;
            // Reinicia los tiros del hoyo actual para el siguiente hoyo (se pueden acumular si se prefiere)
            currentHoleStrokes = 0;
            RespawnBall();
            UpdateUI();
        }
    }

    // Mueve la bola al punto de spawn del hoyo actual y detiene su movimiento.
    void RespawnBall()
    {
        ball.transform.position = holeSpawnPoints[currentHoleIndex].position;

        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.Sleep();
        }
    }

    // Actualiza la UI con los contadores y el número de hoyo
    void UpdateUI()
    {
        if (holeText)
            holeText.text = "Hoyo: " + (currentHoleIndex + 1);
        if (currentHoleStrokeText)
            currentHoleStrokeText.text = "Tiros hoyo: " + currentHoleStrokes;
        if (totalStrokeText)
            totalStrokeText.text = "Tiros totales: " + totalStrokes;
    }

    // Muestra la pantalla final y muestra los resultados finales
    void ShowFinalScreen()
    {
        if (finalScreenPanel)
        {
            finalScreenPanel.SetActive(true);
            if (finalHoleText)
                finalHoleText.text = "Hoyo final: " + (currentHoleIndex + 1);
            if (finalTotalStrokeText)
                finalTotalStrokeText.text = "Tiros totales: " + totalStrokes;
        }
        else
        {
            Debug.LogWarning("No se ha asignado el panel de pantalla final.");
        }
    }
}
