using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Verifica que sea el Player
        if (other.CompareTag("Player"))
        {
            //Notifica que se completo
            GameManager.Instance.HoleCompleted();
        }
    }
}
