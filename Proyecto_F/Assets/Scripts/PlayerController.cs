using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //velocidad del vehículo
    public float speed = 5.0f;

    public float turnSpeed = 0.0f;
    //public float horizontalInput;
    //public float forwardInput;

    // Variables de la cámara
    //public Camera mainCamera;
    //public Camera hoodCamera;
    //public KeyCode switchKey; // Esta será la tecla que nos permitirá cambiar entre cámaras 

    // Variable Multijugador
    //public string inputId;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()

    {
        // Se declara los inputs por teclado del movimiento horizontal
        //horizontalInput = Input.GetAxis("Horizontal" + inputId);
        // Se declara los inputs por teclado del movimiento vertical
        //forwardInput = Input.GetAxis("Vertical" + inputId);

        //transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed);

        // Se declara el movimiento hacia adelante o hacia atrás del carrito usando deltaTime 
        transform.Translate(Vector3.forward * Time.deltaTime * speed );
        //transform.Translate(Vector3.forward * Time.deltaTime * speed * -(forwardInput));

        //Funcionalidad de cambio de cámaras
        //if (Input.GetKeyDown(switchKey))
        //{
        //    mainCamera.enabled = !mainCamera.enabled;
        //    hoodCamera.enabled = !hoodCamera.enabled;
        //}
    }
}