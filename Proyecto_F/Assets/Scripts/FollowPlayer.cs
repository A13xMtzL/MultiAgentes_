using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    // Declaramos la posición de la
    // cámara justo como la tenemos en Unity
    private Vector3 offset = new Vector3(0, 6, -7); 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Se modifica la posción de la
        // cámara de nuestro jugador agregando el offset para que
        // se vea desde una perspectiva de arriba 
        transform.position = player.transform.position + offset; 

    }
}