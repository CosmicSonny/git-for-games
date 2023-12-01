using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D col;
    private Vector2 inputVector, movevector;
    private Vector3 groundcheckA, groundcheckB, ceilingCheckA, ceilingCheckB;
    private float yVel;
    public float gravity = 9.81f;
    public float jumpVel = 9.18f;
    public float climbVel = 9.18f;
    public float speed = 5f;
    public float GroundCheckRadius = 0.1f;
    public LayerMask groundLayers, enemyLayer;
    bool grounded, jumpPressed, jumping, squishEnemy, extraJump, ceilinged, climbing;
    public bool ladderd, wasladderd;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        CalculateScales();
        Manger.LastCheckPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        CalculateMovement();

        //print("The input vector is " + inputVector);
    }

    void GetInputs()
    {
        inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        jumpPressed = Input.GetButtonDown("Jump");
    }

    void CalculateMovement()
    {
        grounded = CheckCollision(groundcheckA, groundcheckB, groundLayers);
        ceilinged = CheckCollision(ceilingCheckA, ceilingCheckB, groundLayers);

        //print("grounded = " + grounded);

        if (jumpPressed)
        {
            jumpPressed = false;
            if (grounded)
            {
                jumping = true;
                yVel = jumpVel;
            }
            if (extraJump)
            {
                extraJump = false;
                jumping = true;
                yVel = jumpVel;
            }
        }

        if(!grounded && yVel < 0f)
        {
            squishEnemy = CheckCollision(groundcheckA, groundcheckB, enemyLayer);
            if (squishEnemy)
            {
                extraJump = true;
                jumping = true;
                yVel = jumpVel * 0.5f;
            }
        }

        if(grounded && yVel <=0f || ceilinged && yVel >0f)
        {
            yVel = 0f;
            jumping = false;
        }
        else
        {
            yVel -= gravity * Time.deltaTime;
        }

        if(ladderd && !wasladderd)
        {
            if(inputVector.y != 0f)
            {
                climbing = true;
                wasladderd = true;
            }
        }

        if(wasladderd && !ladderd)
        {
            climbing = false;
            wasladderd = false;
        }

        if (climbing)
        {
            yVel = climbVel * inputVector.y;
        }

        yVel -= gravity * Time.deltaTime;
        movevector.y = yVel;
        movevector.x = inputVector.x * speed;
    }

    bool CheckCollision(Vector3 a, Vector3 b, LayerMask l)
    {
        Collider2D colA = Physics2D.OverlapCircle(transform.position - a, GroundCheckRadius, l);
        Collider2D colB = Physics2D.OverlapCircle(transform.position - b, GroundCheckRadius, l);
        if (colA)
        {
            if(l == enemyLayer && yVel < 0f)
            {
                colA.gameObject.GetComponent<EnemyHealthSystem>().RecieveHit(1);
            }
            return true;
        }
        else if (colB)
        {
            if (l == enemyLayer && yVel < 0f)
            {
                colB.gameObject.GetComponent<EnemyHealthSystem>().RecieveHit(1);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    void CalculateScales()
    {
        groundcheckA = -col.offset - new Vector2(col.size.x/2f - (GroundCheckRadius * 1.2f), -col.size.y/2);
        groundcheckB = -col.offset - new Vector2(-col.size.x/2f + (GroundCheckRadius* 1.2f), -col.size.y/2);

        ceilingCheckA = -col.offset - new Vector2(col.size.x / 2f - (GroundCheckRadius * 1.2f), col.size.y / 2);
        ceilingCheckB = -col.offset - new Vector2(-col.size.x / 2f + (GroundCheckRadius * 1.2f), col.size.y / 2);
    }


    private void FixedUpdate()
    {
        rb.velocity = movevector;
    }

    private void OnDrawGizmos()
    {
      Gizmos.DrawWireSphere(transform.position - groundcheckA, GroundCheckRadius);
        Gizmos.DrawWireSphere(transform.position - groundcheckB, GroundCheckRadius);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position - ceilingCheckA, GroundCheckRadius);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position - ceilingCheckB, GroundCheckRadius);

    }
}

