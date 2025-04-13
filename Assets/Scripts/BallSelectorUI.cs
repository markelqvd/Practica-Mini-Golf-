using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSelectorUI : MonoBehaviour
{
    public GameObject selectionPanel;

    //Array con los prefabs de las pelotas
    public GameObject[] ballPrefabs;
    //Offset vertical para que la nueva pelota aparezca un poco mas arriba
    public float verticalOffset = 1.0f;     

    //Referencia a la pelota actual
    private GameObject currentBall;

    void Start()
    {
        if (selectionPanel != null)
            selectionPanel.SetActive(false);

        currentBall = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //Al presionar la tecla E se alterna la visibilidad del panel de seleccion
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!selectionPanel.activeSelf)
            {
                selectionPanel.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                selectionPanel.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    public void SelectBall(int index)
    {
        Vector3 spawnPos;
        BallShooter shooter = currentBall.GetComponent<BallShooter>();

        if (currentBall != null)
        {
            spawnPos = currentBall.transform.position + new Vector3(0, verticalOffset, 0);
            Destroy(shooter.arrowInstance);
            Destroy(currentBall);
        }
        else
        {
            spawnPos = Vector3.zero + new Vector3(0, verticalOffset, 0);
        }

        //Instanciar la nueva pelota en la posicion obtenida
        currentBall = Instantiate(ballPrefabs[index], spawnPos, Quaternion.identity);

        currentBall.tag = "Player";

        //Actualizar la referencia en el GameManager para que sepa cual es la pelota actual
        GameManager.Instance.ball = currentBall;

        //Actualizar la referencia de la camara
        Camera.main.GetComponent<CameraController>().target = currentBall.transform;

        //Ocultar el panel y reanudar el juego
        selectionPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
