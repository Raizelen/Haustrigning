using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    [SerializeField]private float moveSpeed =8;
    [SerializeField]private float jumpStrength=16;
    [SerializeField]private Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    private Rigidbody2D rb;
    //attack
    public Transform attackPoint;
    public float attackRange = 0.8f;
    public LayerMask playerAttacked;

    //fight manager
    public FightManager fm;
    //private bool isFacingRight = true;

    //attacks
    [SerializeField] private float lightHit = 10;
    [SerializeField] private float HardHit = 20;
    //Animation
    [SerializeField]private Animator anim;
    //opponent
    [SerializeField] private Transform opponent;
    

    public enum PlayerType
    {
        Player1,
        Player2
    }

    public PlayerType player;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player == PlayerType.Player1 )
            Player1();
        else if(player == PlayerType.Player2)
            Player2();
        
    }
    private void Player1()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(moveSpeed * Time.deltaTime*50, rb.velocity.y);
        } 
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-moveSpeed * Time.deltaTime*50, rb.velocity.y);
    
        }
        if (Input.GetKeyDown(KeyCode.W) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpStrength );
        }
        if (Input.GetKeyDown(KeyCode.S) && !IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x ,-jumpStrength );
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            anim.SetTrigger("attack1");
            Attack("player1", lightHit);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Attack("player1",HardHit);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            fm.Block("player1",true);
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            fm.Block("player1",false);
        }

        if(transform.position.x < opponent.transform.position.x)
        {
            transform.localScale = new Vector3(1f,transform.localScale.y,transform.localScale.z);
            opponent.transform.localScale = new Vector3(-1f, opponent.transform.localScale.y, opponent.transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-1f,transform.localScale.y,transform.localScale.z);
            opponent.transform.localScale = new Vector3(1f, opponent.transform.localScale.y, opponent.transform.localScale.z);
        }

    }
        private void Player2()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(moveSpeed * Time.deltaTime*50, rb.velocity.y);
        } 
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-moveSpeed * Time.deltaTime*50, rb.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpStrength);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x ,- jumpStrength );
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            anim.SetTrigger("attack1");
            Attack("player2",lightHit);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Attack("player2",HardHit);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            fm.Block("player2", true);
        }
        if (Input.GetKeyUp(KeyCode.Keypad3))
        {
            fm.Block("player2", false);
        }


        if (transform.position.x > opponent.transform.position.x)
        {
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
            opponent.transform.localScale = new Vector3(-1f, opponent.transform.localScale.y, opponent.transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
            opponent.transform.localScale = new Vector3(1f, opponent.transform.localScale.y, opponent.transform.localScale.z);
        }
    }
    private bool IsGrounded()
    {

        return Physics2D.OverlapCircle(groundCheck.position,0.02f,groundLayer);
    }


    private void Attack(string playerAttacks,float AttackType)
    {
        
        if (Physics2D.OverlapCircle(attackPoint.position, attackRange, playerAttacked))
        {
            Debug.Log("Attack layer");
            fm.UpdateHealth(playerAttacks,AttackType);
        }
        
    }
}
