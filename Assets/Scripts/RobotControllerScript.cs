using UnityEngine;
using System.Collections;

public class RobotControllerScript : MonoBehaviour {
    private Rigidbody2D rigi;
    public float maxSpeed = 7f;
    bool facingRight = true;

    Animator anim;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float jumpForce = 700f;

    bool doubleJump = false;

	// Use this for initialization
	void Start () {
        rigi = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);

        if (grounded) {

            doubleJump = false;

        }

        anim.SetFloat("vSpeed",rigi.velocity.y);

        //if (!grounded) return;

        float move = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(move));

        rigi.velocity = new Vector2((move * maxSpeed)/2, rigi.velocity.y);

        if(move > 0 && !facingRight)
        {
            Flip();
        }else if (move < 0 && facingRight)
        {
            Flip();
        }
	}

    

    void Update()
    {
        if((grounded || !doubleJump) && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Ground", false);
            rigi.AddForce(new Vector2(0, jumpForce));

            if (!doubleJump && !grounded)
                doubleJump = true;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        
    }
}
