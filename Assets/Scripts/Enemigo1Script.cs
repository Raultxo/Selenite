using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo1Script : MonoBehaviour
{

    private int health = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hit()
    {
        health--;
        if (health == 0) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MovimientoDakari dakari = collision.GetComponent<MovimientoDakari>();
        if (dakari != null)
        {
            dakari.hit(1);
        }


    }
}
