using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Dakari;

    // Update is called once per frame
    void Update()
    {

        //Seguimiento de camara en el jugador
        if (Dakari == null) return;
        Vector3 position = transform.position;
        position.x = Dakari.transform.position.x;
        transform.position = position;
    }
}
