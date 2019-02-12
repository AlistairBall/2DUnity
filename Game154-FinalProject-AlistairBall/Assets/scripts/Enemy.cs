using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    bool walkLeft = true;
    bool walkRight;
    public float speed;
    bool isDead = false;


    private Rigidbody2D rb;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "reverseDirection")
        {
            if (walkLeft == true)
            {

                walkRight = true;
                walkLeft = false;
            }
            else if (walkRight == true)
            {

                walkLeft = true;
                walkRight = false;
            }
        }


        if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
            enemyDeath();
         
        }
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
            enemyDeath();
           
        }

    }

    void enemyDeath()
    {
        anim.SetInteger("Death", 1);
        isDead = true;
        Invoke("death", 1);
    }
    void death()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (isDead == false)
        {
            if (walkLeft == true)
            {
                anim.SetInteger("enemyWalk", 1);
                transform.Translate(new Vector2(-speed * Time.deltaTime, 0f));
            }
            else if (walkRight == true)
            {
                anim.SetInteger("enemyWalk", 2);
                transform.Translate(new Vector2(speed * Time.deltaTime, 0f));
            }
        }
        

    }
}
