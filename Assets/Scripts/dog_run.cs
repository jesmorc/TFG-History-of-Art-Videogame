using UnityEngine;
using System.Collections;

public class dog_run : MonoBehaviour {
    private Rigidbody2D myRigidbody;
    public float speed = 4f;
    public float direction = 1;
    private Animator animator;
    public bool run = true;
    private BoxCollider2D myBoxCollider2D;
    public GameObject character;
    public float aliveTime_afterRunning= 5f;
    private float minDistance = 6f;
    public LayerMask characterMask;
    public int numer_bones_this_level = 6;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (character)
        {
            if (Input.GetKeyDown(KeyCode.E) && Physics2D.OverlapCircle(transform.position, 2f, characterMask))
            {
                speak();
            }
        }

        if (run)
        {
            run_animation();
            myRigidbody.velocity = new Vector2(direction * speed, myRigidbody.velocity.y);
        }
        else myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);

        //float horizontal = Input.GetAxis("Horizontal");
        //myRigidbody.velocity = new Vector2(horizontal * speed, myRigidbody.velocity.y);



    }


    private void run_animation()
    {
        Destroy(gameObject, aliveTime_afterRunning);
        animator.Play("Perro_corre_anim");
    }


    
    private void speak()
    {
        run = true;
        myRigidbody.isKinematic = false;
        //myBoxCollider2D.isTrigger = false;
        Destroy(gameObject, aliveTime_afterRunning);

        if (character.GetComponent<MainCharacterController>().numer_bones >= numer_bones_this_level && character.GetComponent<MainCharacterController>().menina_found)
        {
            Debug.Log("Ok, you have the bones and the Menina of this level, follow me.");
        }
        else if (character.GetComponent<MainCharacterController>().numer_bones >= numer_bones_this_level && !character.GetComponent<MainCharacterController>().menina_found)
        {
            Debug.Log("Dorian, look for the lost Menina in this level, follow my footsprints you have seen in the way.");
        }
        else if (character.GetComponent<MainCharacterController>().numer_bones < numer_bones_this_level && character.GetComponent<MainCharacterController>().menina_found)
        {
            Debug.Log("Dorian, you must find " + numer_bones_this_level + " bones. I need them to continue our journey.");
        }
        else {
            Debug.Log("Dorian, look for the lost Menina in this level, follow my footsprints you have seen in the way.");
            Debug.Log("And you must find " + numer_bones_this_level + " bones. I need them to continue our journey.");
        }
        
    }
}
