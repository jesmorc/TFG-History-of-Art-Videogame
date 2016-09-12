using UnityEngine;
using System.Collections;

public class SphereController : MonoBehaviour {

    public float maxSpeed = 7f;
    public float jumpForce = 1200f;
    public int fallBoundary = -20;
    public string spawnAbstractSoundName = "SpawnAbstract";

    private Rigidbody rigi;
    private float groundRadius = 0.4f;
    private float distToGround;
    private Collider colliderSphere;
    private AudioManager audioManager;


    // Use this for initialization
    void Start () {
        rigi = GetComponent<Rigidbody>();
        colliderSphere = GetComponent<Collider>();
        distToGround = colliderSphere.bounds.extents.y;

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("Panic, no audio manager in scene");
        }
    }

    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        rigi.velocity = new Vector2((move * maxSpeed) / 2, rigi.velocity.y);

    }


    void OnCollisionEnter(Collision collision)
    {
        SphereCollider d = rigi.GetComponent<SphereCollider>();

    }

  
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rigi.AddForce(new Vector3(0, jumpForce, 0));

        }

        if (transform.position.y <= fallBoundary)
        {
            transform.position = GameMaster.gm.transform.GetChild(0).position;
            audioManager.PlaySound(spawnAbstractSoundName);
        }
    }

    private bool IsGrounded()
    {
        Vector3 leftRayStart;
        leftRayStart = colliderSphere.bounds.center;

        Debug.DrawRay(leftRayStart, Vector3.down, Color.red);

        return Physics.Raycast(transform.position, -Vector3.up, distToGround + groundRadius);
        
    }
}
