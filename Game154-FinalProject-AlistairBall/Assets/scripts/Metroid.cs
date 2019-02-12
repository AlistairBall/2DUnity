using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metroid : MonoBehaviour {

   public GameObject player;
   Rigidbody2D rb;
   Vector2 distance;
    public float attackRadius;
    public float speed;
    bool isDead = false;
    public AudioSource explode;
 

    private Animator anim;
    void Awake()
     
    {
        anim = GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        explode = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        if (isDead == false)
        {


            distance = player.transform.position - transform.position;
            if (distance.magnitude <= attackRadius)
            {
                rb.velocity = distance.normalized * speed;
            }
            else
            {
                rb.velocity = new Vector2(0.0f, 0.0f);
            }
        }
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
            explode.Play();
            enemyDeath();

        }
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
            explode.Play();
            enemyDeath();

        }

    }

    void enemyDeath()
    {
        anim.SetInteger("Explosion", 1);
        isDead = true;
        Invoke("death", 2);
    }

    void death()
    {
        rb.velocity = new Vector2(0.0f, 0.0f);
        rb.velocity = distance.normalized * -speed;
        Destroy(gameObject);
    }


}
