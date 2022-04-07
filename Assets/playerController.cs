using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum States
{
    idle,
    run,
    die,
    jump,
    damageble,
    attack_1
}
public class playerController : MonoBehaviour
{
    private Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource attackSound;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private LayerMask Enemy;

    private bool isDie = false;
    private bool isAttack = false;

    private groundCheck groundCheck;
    private eatController eatController;


    private States State
    {
        get
        {
            return (States)anim.GetInteger("Anim");
        }
        set
        {
            anim.SetInteger("Anim", (int)value);
        }
    }

    void Start()
    {
        groundCheck = FindObjectOfType<groundCheck>();
        eatController = FindObjectOfType<eatController>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDie && !isAttack)
            State = States.idle;
        //groundChecking();

        if (Input.GetButton("Horizontal") && !isDie)
            horisontalMovement(); 
        if (Input.GetButton("Vertical") && !isDie)
            upMovement();
        if (Input.GetMouseButtonDown(0) && !isDie && !isAttack )
        {
            isAttack = true;
            onAttack();
        }
        if (FindObjectOfType<timeLine>().timeToDie)
        {
            isDie = true;
            //Debug.Log("Смееерть!");
            State = States.die;
        }
        groundChecking();
        //isDamageble();

    }

    private void horisontalMovement()
    {
        State = States.run;
        //var moveVector = direction;
        //rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, transform.position.y);
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sr.flipX = dir.x < 0.0f;
    }

    private void upMovement()
    {
        if ((groundCheck.onGround || groundCheck.onGroundStatic) && Input.GetKey(KeyCode.W))
        {
            rb.velocity = Vector2.up * jumpForce;
            jumpSound.Play();
        }
            //rb.AddForce(Vector2.up * jumpForce);

        if (groundCheck.onGround && Input.GetKey(KeyCode.S))
        {
            //rb.velocity = Vector2.down * jumpForce;
            downMovement();
            //Debug.Log(Input.GetKey(KeyCode.S)); 
        }

    }

    private void downMovement()
    {
        Physics2D.IgnoreLayerCollision(3, 6, true);
        Invoke("IgnoreLayerOff", .4f);
        //Reset_crouch();
    }
    private void IgnoreLayerOff()
    {
        //Debug.Log("Откл");
        Physics2D.IgnoreLayerCollision(3, 6, false);
    }

    private void groundChecking()
    {
        if(!(groundCheck.onGround || groundCheck.onGroundStatic))
        {
            State = States.jump;
        }
    }

    public void isDamageble()
    {
        //if (eatController.onPoison)
        //{
        if(!isAttack)
            State = States.damageble;
        sr.color = new Vector4(1f, .5f, .5f, 1);
        Invoke("basicColor", .1f);
        //}

    }
    private void basicColor() { sr.color = Color.white; }

    private void onAttack()
    {
        State = States.attack_1;
        Collider2D[] enemies;
        attackSound.Play();
        if (sr.flipX == false)
        {
            enemies = Physics2D.OverlapCircleAll(new Vector2(transform.position.x + .4f, transform.position.y), 0.5f, Enemy);
        } else
        {
            enemies = Physics2D.OverlapCircleAll(new Vector2(transform.position.x - .4f, transform.position.y), 0.5f, Enemy);
        }
        //Debug.Log("asdasd");
        if (enemies.Length == 0)
            Debug.Log("Пусто");
        else
        {
            //var asd = enemies.GetType();
            FindObjectOfType<slaimController>().isDamageble(3);
            Debug.Log("Не пусто");
        }
        Invoke("notAttack", .4f);

        //Debug.DrawRay(Physics2D.OverlapCircleAll(transform.localPosition, 0.001f, Enemy));
        
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + .4f, transform.position.y), 0.5f);
        Gizmos.DrawWireSphere(new Vector2(transform.position.x - .4f, transform.position.y), 0.5f);
        //Gizmos.DrawWireSphere(new Vector2(transform.localPosition.x  , transform.localPosition.y), .001f);
    }
    private void notAttack() { isAttack = false; }

}
