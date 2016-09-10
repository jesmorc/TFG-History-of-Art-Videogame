using UnityEngine;
using System.Collections;

public class change_gravity_character : MonoBehaviour {
    public GameObject character;
    public float new_gravity;
    public bool can_destroy = true;
    public bool can_doubleJump = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject == character)
        {
            character.GetComponent<Rigidbody2D>().gravityScale = new_gravity;
            character.GetComponent<Rigidbody2D>().velocity = new Vector2(character.GetComponent<Rigidbody2D>().velocity.x, 0);

            if (!can_doubleJump)
            {
                character.GetComponent<MainCharacterController>().doubleJump = true;
                //The character can't double jump now
            }
            else
            {
                character.GetComponent<MainCharacterController>().doubleJump = false;
                //The character can double jump now
            }
            if (can_destroy)
                Destroy(gameObject);
        }
    }

}
