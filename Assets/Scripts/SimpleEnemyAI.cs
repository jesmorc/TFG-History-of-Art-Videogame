using UnityEngine;
using System.Collections;

public class SimpleEnemyAI : MonoBehaviour {
    private Rigidbody2D myRigidbody;
    public float speed = 4f;
    public float direction = -1;

    private float MaxDist = 15;

    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update () {
        GameObject thePlayer = GameObject.Find("Character");
        float x_player = thePlayer.transform.position.x;
        float y_player = thePlayer.transform.position.y;
        float x_enemy = transform.position.x;
        float y_enemy = transform.position.y;
        float x_difference = x_player - x_enemy;
       

        if ((x_difference > 1 || x_difference < -1) && (x_difference < MaxDist) && (x_difference > -MaxDist)){
            if ((x_player < x_enemy) && (direction > 0)) {
                direction = -direction;
                Flip();
            }
            else if ((x_player > x_enemy) && (direction < 0))
            {
                direction = -direction;

                Flip();
            }

            myRigidbody.velocity = new Vector2(direction*speed, myRigidbody.velocity.y);
        }
        else
        {
            myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
        }
        
    }

    public void Flip()
    {
        Vector3 myScale = transform.localScale;
        myScale.x *= -1;
        transform.localScale = myScale;
    }
}
