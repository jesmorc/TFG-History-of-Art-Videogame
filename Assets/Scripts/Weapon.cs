using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public float fireRate = 0;
	public int Damage = 30;
	public LayerMask whatToHit;  //it tells us what do we wanna hit

	//variable for particle prefab
	public Transform BulletTrailPrefab;
    public Transform BulletTrailPrefab2;
    public Transform BulletTrailPrefab3;
    public Transform HitPrefab;
	public Transform MuzzleFlashPrefab;
	float timeToSpawnEffect = 0;
	public float effectSpawnRate = 10;

    private Transform[] bulletPrefabs;


    public string weaponShootSound = "SplashShoot";
    public string weaponHitSound = "SplashHit";

	float timeToFire = 0;
	Transform firePoint;

    //caching
    AudioManager audioManager;

	// Use this for initialization
	void Awake () {
		firePoint = transform.FindChild ("FirePoint"); 
		if (firePoint == null) {
			Debug.LogError ("No firepoint");
		}
        
	}

    void Start()
    {
        bulletPrefabs = new Transform[3];
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("No audio manager found in scene");
        }
        bulletPrefabs[0] = BulletTrailPrefab;
        if (BulletTrailPrefab2 != null) bulletPrefabs[1] = BulletTrailPrefab2;
        if (BulletTrailPrefab3 != null) bulletPrefabs[2] = BulletTrailPrefab3;

        
    }

    // Update is called once per frame
    void Update () {
        
        if (fireRate == 0) {
			if (Input.GetButtonDown ("Fire1")) {		//or Input.GetKeyDown(KeyCode.Down);
				Shoot ();
			}
		}
		else {
			if (Input.GetButtonDown ("Fire1") && Time.time > timeToFire) {
				timeToFire = Time.time + 1 / fireRate;  //fire delay
				Shoot ();
			}
		}
	}

	void Shoot(){

        audioManager.PlaySound(weaponShootSound);

        Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (firePoint.position.x , firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, mousePosition - firePointPosition, 100, whatToHit);


        Vector2 direction = mousePosition - firePointPosition;
        direction.Normalize();
        int numberProjectile = Random.Range(0, 3);
        Transform projectile = (Transform)Instantiate(bulletPrefabs[numberProjectile], firePoint.position, Quaternion.identity) as Transform;
        projectile.GetComponent<Rigidbody2D>().velocity = direction * 30f;

        Debug.DrawLine (firePointPosition, (mousePosition - firePointPosition)*100, Color.cyan);
		if (hit.collider != null) {
			Debug.DrawLine (firePointPosition, hit.point, Color.red);

			Enemy enemy = hit.collider.GetComponent<Enemy> ();
			if (enemy != null) {
				enemy.DamageEnemy (Damage);
               
                
                audioManager.PlaySound(weaponHitSound);
				Debug.Log ("We hit " + hit.collider.name + " and did " + Damage + " damage. ");
			}

		}
        Destroy(projectile.gameObject, 3f);

        if (Time.time >= timeToSpawnEffect)
        {  //limit number of bullet trails
            Vector3 hitPos;
            Vector4 hitNormal;

            if(hit.collider == null)
            {
                hitPos = (mousePosition - firePointPosition) * 30;
                hitNormal = new Vector3(9999, 9999, 9999);
            }else
            {
                hitPos = hit.point;
                hitNormal = hit.normal;
            }

            Effect(hitPos, hitNormal);
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }
        
    }

	void Effect(Vector3 hitPosition, Vector3 hitNormal){
		//Transform trail = Instantiate (BulletTrailPrefab, firePoint.position, firePoint.rotation) as Transform;   //create a bullet trail
        

        //LineRenderer lr = trail.GetComponent<LineRenderer>();


        //if(lr != null)
        //{

        //    lr.SetPosition(0, firePoint.position);
        //    lr.SetPosition(1, hitPosition);
        //}

        //Destroy(trail.gameObject, 0.2f);

        if(hitNormal != new Vector3(9999, 9999, 9999))
        {
            Transform hitParticle = Instantiate(HitPrefab, hitPosition, Quaternion.FromToRotation(Vector3.right, hitNormal)) as Transform;
            Destroy(hitParticle.gameObject, 0.3f);
        }

        Transform clone = Instantiate (MuzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
		// OR Transform clone = (Transform) Instantiate (MuzzleFlashPrefab, firePoint.position, firePoint.rotation);
		clone.parent = firePoint;
		float size = Random.Range (0.6f, 0.9f);
		clone.localScale = new Vector3 (size , size, size );  // z doesn't matter
		Destroy (clone.gameObject, 1.5f);

      

        //play shoot sound
        //audioManager.PlaySound(weaponShootSound);
	}
}
