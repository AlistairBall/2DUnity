using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player_controller : MonoBehaviour
{

    public float speed;
    public float maxSpeed;
    public float maxJump;
    public int health = 5;
    public int rocketCount = 10;
    public Text rocketText;
    public Transform BulletSpawnRight;
    public Transform BulletSpawnLeft;
    public Text saved;


    private Rigidbody2D rb;
             Animator anim;
    bool faceLeft;
    bool faceRight;
    bool shotRight;
    bool shotLeft;
    bool shootOnce;
    bool damaged = false;
    bool swapHurt = false;

    bool grounded = true;
    bool Loaded = false;
    bool crouching = false;

    float damageTimer;
    float colourTimer;

    public GameObject Music;
    public Transform lineCastStart;
    public Transform lineCastEnd;
    public GameObject powerUpPrefab;
    public GameObject[] healthBlocks;
    public GameObject BulletRight;
    public GameObject BulletLeft;
    public SpriteRenderer sr;

    public AudioClip walking;
    public AudioClip shootSound;
    public AudioClip Jump;
    public AudioSource allSounds;
    public AudioSource walking1;

   public static bool horizontal;

    public void Awake()
    {
        if (PlayerPrefs.GetInt("Bool") == 1)
        {
            transform.position = (new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ")));
        }
    }
    // Use this for initialization
    void Start()
    {

        anim = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Instantiate(powerUpPrefab, new Vector3(0f, 2f, 0), Quaternion.identity);
        rocketText.text = "" + rocketCount;
  
        allSounds = GetComponent<AudioSource>();
        walking1 = GetComponent<AudioSource>();
        saved.color = new Color(saved.color.r, saved.color.g, saved.color.b, 0.0f);

       
    }
    public void death()
    {
        SceneManager.LoadScene("Game Over");
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
       
        if(other.gameObject.tag == "Enemy")
        {
            if (health >= 1)
            {
                health -= 1;
                healthBlocks[health].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            }
            else if(health < 1)
            {
                health = 0;
                healthBlocks[health].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                anim.SetInteger("Dead", 1);
                Invoke("death", 2);

            }

            damaged = true;
            Debug.Log("HIT: " + other.gameObject.tag);
        }
     
        else if (other.gameObject.tag == "Lava")
        {
            if (health >= 1)
            {
                health -= 1;
                healthBlocks[health].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            }
            else if (health < 1)
            {
                health = 0;
                healthBlocks[health].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                anim.SetInteger("Dead", 1);
                Invoke("death", 2);

            }
        }
    


    }
    public void OnTriggerEnter2D(Collider2D other)
    {

        if(other.gameObject.tag == "Camera")
        {
            horizontal = true;
          
        }

        if (other.gameObject.tag == "Pick-Up")
        {
            rocketCount += 1;
            rocketText.text = "" + rocketCount;
            Destroy(other.gameObject);
           
        }
        else if(other.gameObject.tag == "Health" && health <= 4 )
        {
           
            healthBlocks[health++].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
            Destroy(other.gameObject);
        }
      
        else if(other.gameObject.tag == "Save")
        {
            PlayerPrefs.SetFloat("PlayerX", transform.position.x);
            PlayerPrefs.SetFloat("PlayerY", transform.position.y);
            PlayerPrefs.SetFloat("PlayerZ", transform.position.z);
            saved.color = new Color(saved.color.r, saved.color.g, saved.color.b, 1.0f);

        }
        else if(other.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene("Game Over");
        }
       
    }

    void showDamaged(bool hurt)
    {
        if (hurt == true)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);

        }
        else
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 255);
        }
    }


        // Update is called once per frame
        void Update()
    {

       if(damaged == true)
        {
            colourTimer += Time.deltaTime;
            damageTimer += Time.deltaTime;
            if(colourTimer >= 0.1f)
            {
                swapHurt = !swapHurt;
                showDamaged(swapHurt);
                colourTimer = 0;
            }
            if(damageTimer >= 2.0f)
            {
                swapHurt = false;
                damaged = false;
                showDamaged(swapHurt);
                damageTimer = 0;
            }


        }




        if (Input.anyKey)
        {

            if (crouching == false)
            {

               
                if (Input.GetKey(KeyCode.RightArrow))
                {

                    anim.SetInteger("Direction", 1);
                    transform.Translate(new Vector2(speed * Time.deltaTime, 0f));
                    //  allSounds.PlayOneShot(walking);
                    walking1.PlayDelayed(0.08f);
                    faceLeft = false;
                    faceRight = true;

                    
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    anim.SetInteger("Direction", 2);
                    transform.Translate(new Vector2(-speed * Time.deltaTime, 0f));
                   // allSounds.PlayOneShot(walking);
                    walking1.PlayDelayed(0.08f);
                    faceLeft = true;
                    faceRight = false;

                 

                }

            }

     

            if (Input.GetKey(KeyCode.DownArrow) && faceRight == true)
                {

                    anim.SetInteger("Direction", 5);

                    crouching = true;

                   faceRight = true;
                    faceLeft = false;
            }
               else if (Input.GetKey(KeyCode.DownArrow) && faceLeft == true)
                {

                    anim.SetInteger("Direction", 6);
                crouching = true;
                faceRight = false;
                faceLeft = true;

          
            }
            else
            {
                crouching = false;
            }
            

        }
        else if (faceLeft == true)
        {
            anim.SetInteger("Direction", 4);
            faceRight = false;
            faceLeft = true;

           

        }
        else
        {
            anim.SetInteger("Direction", 0);
            faceRight = true;
            faceLeft = false;

            
        }

        if (Input.GetKeyDown(KeyCode.S))
        {

            if (faceRight == true && Loaded == true && shootOnce == false)
            {

                shotLeft = false;
                shotRight = true;
                Shoot();
                allSounds.PlayOneShot(shootSound);
                rocketCount -= 1;
                rocketText.text = "" + rocketCount;

            }
            else if (faceLeft == true && Loaded == true && shootOnce == false)
            {
                shotRight = false;
                shotLeft = true;
                Shoot();
                rocketCount -= 1;
                rocketText.text = "" + rocketCount;

            }
            shootOnce = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            shootOnce = false;

        }


        if (rocketCount == 0)
        {
            Loaded = false;
        }
        else
        {
            Loaded = true;
        }
    }

    void Shoot()
    {


     
        if (shotRight == true)
        {
            GameObject Bullet = Instantiate(BulletRight, BulletSpawnRight.position, Quaternion.identity) as GameObject;
            Bullet.GetComponent<Rigidbody2D>().velocity = Vector2.right * 4f;
            shotRight = false;


        }
        else if(shotLeft== true)
        {
            GameObject Bullet = Instantiate(BulletLeft, BulletSpawnLeft.position, Quaternion.identity) as GameObject;
            Bullet.GetComponent<Rigidbody2D>().velocity = Vector2.left * 4f;
            shotLeft = false;
        }
    }

    void FixedUpdate()
    {
        Debug.DrawLine(lineCastStart.position, lineCastEnd.position, Color.green);

        int groundLayerMask = 1 << LayerMask.NameToLayer("Ground");
        grounded = (Physics2D.Linecast(lineCastStart.position, lineCastEnd.position, groundLayerMask)) ? true : false;

        if (grounded) { anim.SetInteger("Jump", 0); }

        if (Input.GetKey(KeyCode.Space) && grounded == true)
        {
            if (faceRight == true)
            {
                anim.SetInteger("Jump", 1);
                rb.AddForce(new Vector2(0f, maxJump));
                allSounds.PlayOneShot(Jump);
                
                grounded = false;
                faceLeft = false;
                faceRight = true;
            }
            else if (faceLeft == true)
            {
                anim.SetInteger("Jump", 2);
                rb.AddForce(new Vector2(0f, maxJump));
                allSounds.PlayOneShot(Jump);
                grounded = false;
                faceRight = false;
                faceLeft = true;
            }

        }
    }
    /*
    public void prepareSaveGame()
    {
        LoadSaveManager.GameStateData.PlayerData playerData = GameManager.StateManager.gameState.playerData;
        playerData.health = health; 
        playerData.rocketCount = rocketCount;
        playerData.transformData.Posx = transform.position.x;
        playerData.transformData.Posy = transform.position.y;
        playerData.transformData.Posz = transform.position.z;
    }

    public void loadPlayer()
    {
        LoadSaveManager.GameStateData.PlayerData playerData = GameManager.StateManager.gameState.playerData;
        health = playerData.health;
        rocketCount = playerData.rocketCount;
        transform.position = new Vector3(playerData.transformData.Posx, playerData.transformData.Posy, playerData.transformData.Posz);
    }
    */
}
