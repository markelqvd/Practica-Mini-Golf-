using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public GameObject[] ballPrefabs; // Golf, Tenis, Ping Pong
    public Transform spawnPoint;

    void Start()
    {
        int selected = PlayerPrefs.GetInt("SelectedBall", 0);
        Instantiate(ballPrefabs[selected], spawnPoint.position, Quaternion.identity);
    }
}
