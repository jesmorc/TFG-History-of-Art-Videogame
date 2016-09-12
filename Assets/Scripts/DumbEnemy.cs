using UnityEngine;
using System.Collections;

public class DumbEnemy : MonoBehaviour {
    private Rigidbody2D myRigidbody;
    public float speed = 4f;
    public float direction = -1;
    private float MaxDist = 23;
    private float wait_to_shoot = 3f;

    //For shooting
    public Transform gunTip;
    public GameObject bullet;
    float fireRate = 3f;
    float nextFire = 3f;

    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject thePlayer = GameObject.Find("Character");
        if (thePlayer == null)
            return;
        float x_player = thePlayer.transform.position.x;
        float y_player = thePlayer.transform.position.y;
        float x_enemy = transform.position.x;
        float y_enemy = transform.position.y;
        float x_difference = x_player - x_enemy;

        if ((x_difference > 1 || x_difference < -1) && (x_difference < MaxDist) && (x_difference > -MaxDist))
        {
            myRigidbody.velocity = new Vector2(direction * speed, myRigidbody.velocity.y);
            //enemy shooting
            FireInkBullet();
        }
        else
        {
            myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
        } 
    }



    public void Flip_and_change_direction()
    {
        direction = -direction;
        myRigidbody.velocity = new Vector2(direction * speed, myRigidbody.velocity.y);

        Flip();
    }


    public void Flip()
    {
        Vector3 myScale = transform.localScale;
        myScale.x *= -1;
        transform.localScale = myScale;
    }

    void FireInkBullet()
    {
        if (Time.time > nextFire+ wait_to_shoot)
        {
            nextFire = Time.time + fireRate;
           if (direction > 0)
                Instantiate(bullet, gunTip.position, Quaternion.Euler (new Vector3(0, 0, 180f)));
           else Instantiate(bullet, gunTip.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        }

    }
}



