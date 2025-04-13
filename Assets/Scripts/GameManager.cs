using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //Spawn de cada hoyo
    public Transform[] holeSpawnPoints;
    public GameObject ball;

    //Indice del hoyo actual
    private int currentHoleIndex = 0;

    //Referencia a los mensajes del tutorial
    public HoleMessageManager messageManager;

    public GameObject finalScreenPanel;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        finalScreenPanel.SetActive(false);
        RespawnBall();
    }

    //Llamado cuando la bola entra en el hoyo
    public void HoleCompleted()
    {
        Debug.Log("Hoyo completado: " + (currentHoleIndex + 1));
        
        //Si es el ultimo hoyo
        if (currentHoleIndex >= holeSpawnPoints.Length - 1)
        {
            ShowFinalScreen();
        }
        else
        {
            // Avanza al siguiente hoyo
            currentHoleIndex++;
            RespawnBall();
        }    
    }

    //Mueve la bola al punto de spawn del hoyo actual
    public void RespawnBall()
    {
        ball.transform.position = holeSpawnPoints[currentHoleIndex].position;

        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;  
        }
        messageManager.ShowMessage(currentHoleIndex);
    }

    void ShowFinalScreen()
    {
            finalScreenPanel.SetActive(true);
    }
}
