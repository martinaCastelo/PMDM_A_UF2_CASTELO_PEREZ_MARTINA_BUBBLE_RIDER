using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera2ToCamera1 : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera camera2;
    [SerializeField] CinemachineVirtualCamera camera1;
    [SerializeField] GameObject camera2Trigger;

    //ACCION PARA ACTIVAR LA CAMARA 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player2"))
        {
            //Debug.Log("2. El jugador ha entrado en el collider nuevo de la puerta");

            camera2.Priority = 10;
            camera1.Priority = 11;

            camera2Trigger.SetActive(true);
            gameObject.SetActive(false);
        }
    }

}
