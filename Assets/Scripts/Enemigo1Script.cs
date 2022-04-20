using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo1Script : MonoBehaviour
{
    public GameObject BulletPrefab;
    private int health = 3;
    public GameObject Dakari;
    private float LastShoot;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Dakari == null)
        {
            return;
        }
        Vector3 direction = Dakari.transform.position - transform.position;

        if (direction.x >= 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        float distance = Mathf.Abs(Dakari.transform.position.x - transform.position.x);

        if (distance < 1.0f && Time.time > LastShoot + 1.0f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.left;
        else direction = Vector3.right;

        GameObject enemyBullet = Instantiate(BulletPrefab, transform.position + direction * 0.2f, Quaternion.identity);
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
