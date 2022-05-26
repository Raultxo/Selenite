using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
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

    //Cuando el ataque del jugador choque con un enemigo, el ataque desaparecera y este recibirá daño
    private void OnTriggerEnter2D (Collider2D collision)
    {

        Enemigo1Script enemigo1 = collision.GetComponent<Enemigo1Script>();

        if (enemigo1 != null)
        {
            enemigo1.hit();
            DestroyBullet();
        }
        
    }
    
}
