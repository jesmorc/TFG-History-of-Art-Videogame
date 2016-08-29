using UnityEngine;
using System.Collections;

public class SphereController : MonoBehaviour {

    private Rigidbody rigi;
    public float maxSpeed = 7f;
   

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float jumpForce = 300f;

    

    // Use this for initialization
    void Start () {
        rigi = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        //grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
      

        float move = Input.GetAxis("Horizontal");

        rigi.velocity = new Vector2((move * maxSpeed) / 2, rigi.velocity.y);

      
    }


    float Bouncibility = 1.1f;

    void OnCollisionEnter(Collision collision)
    {
        SphereCollider d = rigi.GetComponent<SphereCollider>();
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Slippery"))
        {
           
        }
        else
        {
            
        }
        //rigi.velocity = new Vector2(collision.relativeVelocity.y / Bouncibility, rigi.velocity.y);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigi.AddForce(new Vector3(0, jumpForce, 0));

            
        }
    }
}
