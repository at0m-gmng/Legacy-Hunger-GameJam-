using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StatesSlaim
{
    slaim_idle,
    slaim_attack,
    slaim_die
}
public class slaimController : MonoBehaviour
{
    SpriteRenderer sr;
    Animator anim;

    [SerializeField] private GameObject goldenApple;
    [SerializeField] private Transform parentGoldenApple;
    [SerializeField] private int distance;
    private float maxDistance, minDistance;
    [SerializeField] private int speed = 1;
    private int staticSpeed = 0;
    [SerializeField] private float slaimDamage = 1;
    [SerializeField] private float slaimLives = 1;

    private Transform target;
    private StatesSlaim StatesSlaim
    {
        get
        {
            return (StatesSlaim)anim.GetInteger("Slaim");
        }
        set
        {
            anim.SetInteger("Slaim", (int)value);
        }
    }
    void Start()
    {
        parentGoldenApple = GameObject.Find("Canvas").GetComponent<Transform>();
        staticSpeed = speed;
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        maxDistance = transform.localPosition.x + distance;
        minDistance = transform.localPosition.x - distance;

        target = FindObjectOfType<playerController>().GetComponent<Transform>();

    }

    public void isDamageble(float damage)
    {
        slaimLives -= damage;
        sr.color = new Vector4(1f, .5f, .5f, 1);
        Invoke("basicColor", .1f);
        if (slaimLives <= 0)
        {
            spawnGoldenZero();
            gameObject.SetActive(false); 
            Destroy(gameObject, .1f);
        }
    }
    public void spawnGoldenZero()
    {
        goldenApple = Instantiate(goldenApple, parentGoldenApple);
        goldenApple.transform.localScale = new Vector3(2.5f, 2.5f, 1);
        goldenApple.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y , gameObject.transform.localPosition.z);
        goldenApple.GetComponent<SpriteRenderer>().color = new Vector4(goldenApple.GetComponent<SpriteRenderer>().color.r, goldenApple.GetComponent<SpriteRenderer>().color.g, goldenApple.GetComponent<SpriteRenderer>().color.b, 1);
    }
    private void basicColor() { sr.color = Color.white; }

    private void FixedUpdate()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
        if (transform.localPosition.x > maxDistance)
        {
            speed = -speed;
            sr.flipX = false;
        }
        else if (transform.localPosition.x < minDistance)
        {
            speed = -speed;
            sr.flipX = true;
        }
        sr.material.color = new Color(1f, 1f, 1f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {

            if (collision.GetComponent<eatController>().onPoison)
            {
                //gameController.Instance.popUpTextSpawner(gameObject.transform);
                slaimLives += 2f;
                slaimDamage += 2f;
            }
            else
            {
                //gameController.Instance.popUpTextSpawner(gameObject.transform);
                slaimLives += 1f;
                slaimDamage += 1f;
            }
            //Debug.Log(slaimDamage);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            staticSpeed = speed;
            StatesSlaim = StatesSlaim.slaim_attack;
            speed = 0;
            gameController.Instance.addScore(slaimDamage, true, collision.gameObject);
            collision.gameObject.GetComponent<playerController>().isDamageble();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            StatesSlaim = StatesSlaim.slaim_idle;
            gameController.Instance.addScore(slaimDamage, true, collision.gameObject);
            collision.gameObject.GetComponent<playerController>().isDamageble();

            speed = -staticSpeed;
            sr.flipX = !sr.flipX;
            //speed = -staticSpeed;
            //sr.flipX = !sr.flipX;
        }
    }
}
