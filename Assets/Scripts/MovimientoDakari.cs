using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoDakari : MonoBehaviour
{

    public GameObject BulletPrefab;
    public float JumpForce;
    public float Speed;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    private bool Grounded;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Horizontal < 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        Debug.DrawRay(transform.position, Vector3.down * 0.25f, Color.cyan);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.25f))
            Grounded = true;
        else
            Grounded = false;

        if(Input.GetKeyDown(KeyCode.W) && Grounded)
            jump();

        if (Input.GetKeyDown(KeyCode.F))
            shoot();
    }

    private void shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.left;
        else direction = Vector3.right;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
    }

    private void jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
    }
}