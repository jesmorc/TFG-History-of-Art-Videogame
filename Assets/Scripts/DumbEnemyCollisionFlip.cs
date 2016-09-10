using UnityEngine;
using System.Collections;

public class DumbEnemyCollisionFlip : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject fatherbody = gameObject.transform.parent.gameObject;

        float direction = fatherbody.GetComponent<Rigidbody2D>().velocity.x;

        //if (! (collision.gameObject.name == "Character"))
        if (gameObject.name == "Right Arm" && !(collision.gameObject.name == "Character"))
        {
            fatherbody.GetComponent<DumbEnemy>().Flip_and_change_direction();
        }
       
    }

}
