using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{

    public bool initalDirectionRight = false;
    private float directionMulti = 1f;
    public float speed = 1;
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private Vector3 groundCheckOffsetA, groundCheckOffsetB, frontPlayerCol, backPlayerCol;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer, playerLayer;
    private bool turning = false;
    private bool turningWall = false;
    private SpriteRenderer sp;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        sp = GetComponent<SpriteRenderer>();
        CheckScales();
        if (initalDirectionRight)
        {
            directionMulti = 1;
            sp.flipX = !sp.flipX;
        }
        else
        {
            directionMulti = -1;
        }
    }

     // Update is called once per frame
    void Update()
    {
        CalcMovement();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position - groundCheckOffsetA, groundCheckRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position - groundCheckOffsetB, groundCheckRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position - frontPlayerCol, groundCheckRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position - backPlayerCol, groundCheckRadius);

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(speed * directionMulti, 0, 0);
    }


    void CheckScales()
    {
        groundCheckOffsetA = -col.offset - new Vector2(col.size.x / 2f - (groundCheckRadius * 1.2f), -col.size.y / 2);
        groundCheckOffsetB = -col.offset - new Vector2(-col.size.x / 2f - (groundCheckRadius * 1.2f), -col.size.y / 2);

        frontPlayerCol = -col.offset - new Vector2(col.size.x / 2f, 0);
        backPlayerCol = -col.offset - new Vector2(-col.size.x / 2f, 0);
    }

    void CalcMovement()
    {
        bool platformed = CheckEndOfPlatform(groundCheckOffsetA, groundCheckOffsetB, groundLayer);
        bool hitwall = CheckPlayerOrWallCol(frontPlayerCol, backPlayerCol, groundLayer);
        bool hitPlayer = CheckPlayerOrWallCol(frontPlayerCol, backPlayerCol, playerLayer);
        if (!platformed && !turning || hitwall && !turningWall || hitPlayer && !turningWall)
        {
            directionMulti *= -1;
            turning = true;
            turningWall = true;
            sp.flipX = !sp.flipX;
        }
        if (platformed && turning || hitwall)
        {
            turning = false;
        }
        if(!hitwall && turningWall && !hitPlayer)
        {
            turningWall = false;
        }
        if (hitPlayer)
        {
            //killplayer
            GameObject.FindGameObjectWithTag("Player").transform.position = Manger.LastCheckPoint;
        }
    }

    bool CheckEndOfPlatform(Vector3 a, Vector3 b, LayerMask l)
    {
        if(Physics2D.OverlapCircle(transform.position - a, groundCheckRadius, l) && Physics2D.OverlapCircle(transform.position - b, groundCheckRadius, l))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool CheckPlayerOrWallCol(Vector3 a, Vector3 b, LayerMask l)
    {
        if (Physics2D.OverlapCircle(transform.position - a, groundCheckRadius, l) || Physics2D.OverlapCircle(transform.position - b, groundCheckRadius, l))
        {
            return true; 
        }
        else
        {
            return false;
        }
    }
}
