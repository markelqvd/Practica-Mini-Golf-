using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HoleMessageManager : MonoBehaviour
{
    public Text messageText;
    public GameObject panel;

    //Mensajes tutorial
    private string[] holeMessages = {
        "Usa el click izquierdo para disparar",
        "Usa el click derecho para mover la camara",
        "Pulsa 'E' para abrir el selector de pelotas"
    };

    public void ShowMessage(int holeIndex)
    {
        if (holeIndex < holeMessages.Length)
        {
            messageText.text = holeMessages[holeIndex];
            messageText.gameObject.SetActive(true);
        }
        else
        {
            //A partir del cuarto hoyo desactiva permanentemente
            messageText.gameObject.SetActive(false);
            panel.gameObject.SetActive(false);
        }
    }
}
