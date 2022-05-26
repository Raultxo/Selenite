using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoDakari : MonoBehaviour
{
    private Animator Animator;
    public GameObject BulletPrefab;
    public float JumpForce;
    public float Speed;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    private bool isFacingRight = true;
    private bool Grounded;
    private int health = 5;

    private float activeMoveSpeed;


    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;


    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        activeMoveSpeed = Speed;
    }

    // Update is called once per frame
    void Update()
    {

        if (isDashing)
        {
            return;
        }

        //el jugador mira hacia donde avanza
        Horizontal = Input.GetAxisRaw("Horizontal");
        Flip();

        //comprueba si el jugador esta tocando el suelo
        Debug.DrawRay(transform.position, Vector3.down * 0.25f, Color.cyan);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.25f))
            Grounded = true;
        else
            Grounded = false;

        //salto solo si esta tocando el suelo
        if(Input.GetKeyDown(KeyCode.W) && Grounded)
            jump();
       
        //ataque del jugador
        if (Input.GetKeyDown(KeyCode.F))
        {
            shoot();
        }

        //esquiva del jugador
        if (Input. GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(Horizontal * Speed, rb.velocity.y);
    }

    //metodo para dar la vuelta al jugador
    private void Flip()
    {
        if (isFacingRight && Horizontal > 0f || !isFacingRight && Horizontal < 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    //metodo para atacar
    private void shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.left;
        else direction = Vector3.right;
        Animator.SetTrigger("attack");
        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.2f, Quaternion.identity);
    }
    
    //metodo para saltar
    private void jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);

    }

    //metodo que maneja cuando el jugador recibe un golpe y su salud
    public void hit(int potencia)
    {
        health--;


        if (Horizontal < 0.0f) Rigidbody2D.velocity = Vector2.right * potencia;
        else if (Horizontal > 0.0f) Rigidbody2D.velocity = Vector2.left * potencia;



        if (health == 0) Destroy(gameObject);
    }
    
    //rutina de la esquiva
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        activeMoveSpeed = dashingPower;
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
        activeMoveSpeed = Speed;

    }
}
