using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathArea : MonoBehaviour
{
    //Respawnear al jugador si se sale del campo
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.RespawnBall();
        }
    }
}
