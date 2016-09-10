using UnityEngine;
using System.Collections;

public class PlayerAbstracto : MonoBehaviour {

	private float inputDirection;  // X calue of our moveVector
	private float verticalVelocity; // y value of moveVector

	private float speed = 5.0f;
	private float gravity = 30.0f;
	private float jumpForce = 10.0f;
	private bool secondJumpAvail = false;
    public int fallBoundary = -20;

    private Vector3 moveVector;
	private CharacterController controller;
    private AudioManager audioManager;
    public string deathSoundName = "DeathVoice";
    public string spawnAbstractSoundName = "SpawnAbstract";

    private const float minDistance = 2f;

    private bool soundPlayed = false;


    // Use this for initialization
    void Start () {
		controller = GetComponent<CharacterController> ();

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("Panic, no audio manager in scene");
        }
    }
	
	// Update is called once per frame
	void Update () {
		IsControllerGrounded ();
		moveVector = Vector3.zero;
		inputDirection = Input.GetAxis ("Horizontal") * speed;
        moveVector.x = inputDirection;


        if (IsControllerGrounded()) {
			verticalVelocity = 0;

			if(Input.GetKeyDown(KeyCode.Space)) {
				verticalVelocity = jumpForce;
				secondJumpAvail = true;
			}

			

		} else {
			
			if(Input.GetKeyDown(KeyCode.Space)) {

				if (secondJumpAvail) {
					verticalVelocity = jumpForce;
					secondJumpAvail = false;
				}

			}

			verticalVelocity -= gravity * Time.deltaTime;
		}

        if (transform.position.y <= fallBoundary)
        {
            transform.position = GameMaster.gm.transform.GetChild(0).position;
            audioManager.PlaySound(spawnAbstractSoundName);

        }
        /*
        GameObject[] pepe = GameObject.FindGameObjectsWithTag("Triggersito");

        for (int i = 0; i < pepe.Length; i++)
        {
            if ((transform.position - pepe[i].transform.position).sqrMagnitude <= minDistance * minDistance && !soundPlayed)
            {
                Debug.Log("PEPE");
                audioManager.PlaySound(deathSoundName);
                //soundPlayed = true;
                Destroy(pepe[i]);

            }
            //soundPlayed = false;
        }
        */

        moveVector.y = verticalVelocity;
		controller.Move (moveVector * Time.deltaTime);

	}

   

    private bool IsControllerGrounded(){
		Vector3 leftRayStart;
		Vector3 rightRayStart;


		leftRayStart = controller.bounds.center;
		rightRayStart = controller.bounds.center;
	

		leftRayStart.x -= controller.bounds.extents.x;
		rightRayStart.x += controller.bounds.extents.x;

		Debug.DrawRay (leftRayStart, Vector3.down, Color.red);
		Debug.DrawRay (rightRayStart, Vector3.down, Color.green);


		if (Physics.Raycast (leftRayStart, Vector3.down, (controller.height / 2) + 0.1f)) {
			return true;
		}

		if (Physics.Raycast (rightRayStart, Vector3.down, (controller.height / 2) + 0.1f)) {
			return true;
		}

		return false;
	}

	private void OnControllerColliderHit(ControllerColliderHit hit){
		if (controller.collisionFlags == CollisionFlags.Sides) {
			
			if (Input.GetKeyDown (KeyCode.Space)) {
				Debug.DrawRay (hit.point, hit.normal, Color.red, 2.0f);
				moveVector = hit.normal * speed;
				verticalVelocity = jumpForce;
				secondJumpAvail = true;
			}
		}

		//Collectables
		switch(hit.gameObject.tag){
		case "Coin":
			//LevelManager.Instance.CollectCoin ();
			Destroy (hit.gameObject);
			break;
		case "JumpPad":
			verticalVelocity = jumpForce * 2;
			secondJumpAvail = true;
			break;
		case "Teleport":
			transform.position = hit.transform.GetChild (0).position;
			break;
		case "Winbox":
			//LevelManager.Instance.Win ();
			break;
        case "FallingPlatform":
                hit.gameObject.GetComponent<PlatformFall>().Fall();
            break;
		default:
			break;
		}
	}
}
