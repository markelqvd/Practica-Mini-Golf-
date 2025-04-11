using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleTrigger : MonoBehaviour
{
    // Al detectar la entrada de otro collider...
    private void OnTriggerEnter(Collider other)
    {
        // Verifica que sea la bola (tag "Player")
        if (other.CompareTag("Player"))
        {
            // Notifica al GameManager que se completó un hoyo
            GameManager.Instance.HoleCompleted();
        }
    }
}
