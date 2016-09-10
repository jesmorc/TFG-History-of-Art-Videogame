using UnityEngine;
using System.Collections;

public class EnemyProjectileController : MonoBehaviour {
    public float projectileSpeed;
    Rigidbody2D myRB;


    void Awake()
    {
        myRB = gameObject.GetComponent<Rigidbody2D>();
        if (transform.localRotation.z >0)
            myRB.AddForce(new Vector2(-1,0)*projectileSpeed, ForceMode2D.Impulse);
        else myRB.AddForce(new Vector2(1, 0) *projectileSpeed, ForceMode2D.Impulse);

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update () {
	
	}
}
