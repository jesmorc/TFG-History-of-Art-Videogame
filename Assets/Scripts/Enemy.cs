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
            PlayerRenacentismo _papas = _papa.GetComponent<PlayerRenacentismo>();
            _papas.DamagePlayer(stats.damage);
            DamageEnemy(9999999);
        }
    }
}
