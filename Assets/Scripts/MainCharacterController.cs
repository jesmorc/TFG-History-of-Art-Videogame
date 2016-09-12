using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainCharacterController : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {
        public int maxHealth = 100;

        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public void Init()
        {
            curHealth = maxHealth;
        }
    }

    private Transform spawnPosition;

    public PlayerStats stats = new PlayerStats();

    private Rigidbody2D myRigidbody;

    public float jumpStrength = 15f;
    private bool onFloor = true;
    public Transform floorChecker;
    private float radiusChecker = 0.7f;
    public LayerMask floorMask;
    public bool doubleJump = false;

    private bool m_FacingRight = true;  // For determining which way the player is currently facing.

    private AudioSource[] audios;

    private Animator animator;
    private bool running = false;
    public float speed = 8f;
    private bool is_attacking = false;
    public int numer_bones = 0;
    public int max_bones = 10;
    public bool menina_found;
    public bool key_found = false;

    private bool can_uncrouch = true;
    private bool crouch = false;
    private AudioManager audioManager;

    public int fallBoundary = -20;

    public string deathSoundName = "DeathVoice";
    public string damageSoundName = "Grunt";

    public bool able_to_run = true;


    [SerializeField]
    public LifeIndicator lifeIndicator;

    void Awake()
    {
        //spawnPosition.position = gameObject.transform.position;
        animator = GetComponent<Animator>();
        audios = GetComponents<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.Log("No audio manager");
        }

        stats.Init();

        if (lifeIndicator == null)
        {
            //Debug.LogError("No life indicator referenced on Player");
            lifeIndicator = GameObject.FindGameObjectWithTag("LifeIndicator").GetComponent<LifeIndicator>();
            lifeIndicator.Reset();
        }
        else
        {
            lifeIndicator.SetHealth(stats.maxHealth);
        }

    }

    //Se le llama cada cierto tiempo constante, 
    //porque el update normal, si nuestro pc es muy rápido, irá actualizando cada frame muy rápido
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            crouch = true;
        }
        else
        {
            if (crouch)
            {
                if (can_uncrouch)
                {
                    crouch = false;
                }
            }
        }
        animator.SetBool("Crouch", crouch);
        if (running)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y);
        }
        animator.SetFloat("VelX", GetComponent<Rigidbody2D>().velocity.x);
        onFloor = Physics2D.OverlapCircle(floorChecker.position, radiusChecker, floorMask);
        animator.SetBool("isGrounded", onFloor);

        if (onFloor)
        {
            doubleJump = false;
        }
    }
    

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y <= fallBoundary)
            DamagePlayer(9999999);

        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal > 0 || horizontal < 0)
        {
            if ((horizontal < 0 && m_FacingRight) || (horizontal > 0 && !m_FacingRight))
            {
                Flip();
            }

            if (!running)
            {
                //NotificationCenter.DefaultCenter().PostNotification(this, "PersonajeEmpiezaACorrer");
            }
            running = true;
        }
        else {
            running = false;
            //NotificationCenter.DefaultCenter().PostNotification(this, "PersonajeHaParado");
        }

        bool running_fast = Input.GetKey(KeyCode.LeftShift);
        if (able_to_run)
            animator.SetBool("RunFast", running_fast);
        HandleMovement(horizontal, crouch, running_fast);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if ((onFloor || !doubleJump) && !crouch)
            {
                if (!doubleJump && !onFloor)
                {

                    doubleJump = true;
                }
                int randomSound = (int)Random.Range(0, audios.Length);
                audios[randomSound].Play();
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpStrength);

            }
        }

       if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
      
        {
            //int randomSound = (int)Random.Range(0, audios.Length);
            //audios[randomSound].Play();
            if (SceneManager.GetActiveScene().name != "EscenaXilografia")
            {
                audioManager.PlaySound("Attack");
                attack();
            }
        }



    }

    public void DamagePlayer(int damage)
    {
        stats.curHealth -= damage;
        if (stats.curHealth <= 0)
        {
            //play death sound
            audioManager.PlaySound(deathSoundName);

            GameMaster.KillPlayer(this.transform);
        }
        else
        {
            //play damage sound
            audioManager.PlaySound(damageSoundName);
        }

        lifeIndicator.SetHealth(stats.curHealth);
    }

    private void HandleMovement(float horizontal, bool crouch, bool running)
    {
        float mod = 1f;
        if (running && able_to_run)
        {
            mod *= 2;
        }
        if (crouch)
        {
            mod /= 2;
        }

        myRigidbody.velocity = new Vector2(horizontal * speed * mod, myRigidbody.velocity.y);

    }

    public void takeBone()
    {
        this.numer_bones += 1;

        //GameObject bonestext = GameObject.Find("bones count");
        // bonestext.GetComponent<TextMesh>().text = numer_bones.ToString() + "/" + max_bones.ToString();
    }

    private void Flip()
    {

        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void attack()
    {
        is_attacking = true;
        animator.Play("character_attack");
        is_attacking = false;
    }

    public void setToSpawn()
    {
        transform.position = GameMaster.gm.transform.GetChild(0).position;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.CompareTag("Obstacle"))
        {
            can_uncrouch = false;
        }
        else can_uncrouch = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        can_uncrouch = true;
    }

}