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


        Horizontal = Input.GetAxisRaw("Horizontal");
        Flip();

       /* if (Horizontal < 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);*/

        Debug.DrawRay(transform.position, Vector3.down * 0.25f, Color.cyan);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.25f))
            Grounded = true;
        else
            Grounded = false;

        if(Input.GetKeyDown(KeyCode.W) && Grounded)
            jump();


        if (Input.GetKeyDown(KeyCode.F))
            shoot();

        if (Input. GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }



    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(Horizontal * Speed, rb.velocity.y);
    }

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

    private void shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.left;
        else direction = Vector3.right;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.2f, Quaternion.identity);
    }

    private void jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);

    }


    public void hit(int potencia)
    {
        health--;


        if (Horizontal < 0.0f) Rigidbody2D.velocity = Vector2.right * potencia;
        else if (Horizontal > 0.0f) Rigidbody2D.velocity = Vector2.left * potencia;



        if (health == 0) Destroy(gameObject);
    }

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
