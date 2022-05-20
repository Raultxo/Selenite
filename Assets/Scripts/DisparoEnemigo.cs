using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoEnemigo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    //Cuando el ataque del enemigo choque con el jugador, el ataque desaparecera y este recibira daño
    private void OnTriggerEnter2D (Collider2D collision)
    {
        MovimientoDakari dakari = collision.GetComponent<MovimientoDakari>();

        if (dakari != null)
        {
            dakari.hit(1);
            DestroyBullet();
        }


       
    }
    
}
