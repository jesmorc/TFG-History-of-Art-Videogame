using UnityEngine;
using System.Collections;

public class flip_sprite : MonoBehaviour {

    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
  


// Use this for initialization
void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal > 0 || horizontal < 0)
        {
            if ((horizontal < 0 && m_FacingRight) || (horizontal > 0 && !m_FacingRight))
            {
                Flip();
            }
        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;


    }
}
