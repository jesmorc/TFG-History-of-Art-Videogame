using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	[System.Serializable]   //to be able to see the public parameters in unity
	public class EnemyStats
	{
        public int maxHealth = 100;
        

        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public int damage = 40;

        public void Init()
        {
            curHealth = maxHealth;
        }

    }

	public EnemyStats stats = new EnemyStats();

    public Transform deathParticles;

    public float shakeAmt = 0.1f;
    public float shakeLength = 0.1f;

    public string deathSoundName = "Explosion";

    private bool m_FacingRight;

    private GameObject player;

    [Header("Optional: ")]
    [SerializeField]
    private StatusIndicator statusIndicator;

    void Start()
    {

        stats.Init();

        if(statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }

        if(deathParticles == null)
        {
            Debug.LogError("No death particles reference on enemy");
        }

        player = GameObject.FindGameObjectWithTag("Player");
        if(player.transform.position.x < this.transform.position.x)
        {
            m_FacingRight = true;
        }else
        {
            m_FacingRight = false;
        }
        
    }

    void Update()
    {
        if (player != null) { 
            if ((player.transform.position.x < this.transform.position.x && m_FacingRight) || (player.transform.position.x > this.transform.position.x && !m_FacingRight))
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;
        this.GetComponent<SpriteRenderer>().flipX = !this.GetComponent<SpriteRenderer>().flipX;

    }

    public void DamageEnemy(int damage){
		stats.curHealth -= damage;
		if (stats.curHealth <= 0) {
			GameMaster.KillEnemy (this);
		}

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }



    //unity function , every time collides with another object
    void OnCollisionEnter2D(Collision2D _colInfo)
    {
        
        
        
        Debug.Log("FEOOOOO" + _colInfo.gameObject.name + _colInfo.gameObject.tag);

        GameObject _papa = _colInfo.gameObject;
        
        //PlayerRenacentismo _player = _colInfo.collider.GetComponent<PlayerRenacentismo>();
        
        if (_papa != null)
        {
            MainCharacterControllerShooter _papas = _papa.GetComponent<MainCharacterControllerShooter>();
            if(!_papas != null) {
                _papas.DamagePlayer(stats.damage);
                DamageEnemy(9999999);
            }
            
        }
    }
}
