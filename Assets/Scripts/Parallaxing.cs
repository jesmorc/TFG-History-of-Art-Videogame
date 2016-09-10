using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

	public Transform[] backgrounds;    //Array of all the back and foregrounds to be parallaxed
	private float[]  parallaxScales;	//the proportion of the camers movement to move the backgrounds by.
	public float smoothing = 1f;			//how smooth the parallax will be. Make sure to set it above 0.

	private Transform cam;			//reference to the main cameras Transform
	private Vector3 previousCamPos;	//position of the camera in the previous frame

	//calle before start(). Great for references
	void Awake(){
		//set up the camera reference
		cam = Camera.main.transform;
	}

	// Use this for initialization
	void Start () {
		// the previous frame had the current frames's camera position
		previousCamPos = cam.position;

		//assigning corresponding parallaxScales
		parallaxScales = new float[backgrounds.Length];
		for (int i = 0; i < backgrounds.Length; i++) {
			parallaxScales [i] = backgrounds[i].position.z*-1;
		}
	}
	
	// Update is called once per frame
	void Update () {
        if (backgrounds.Length != 0)
        {
            // for each background
            for (int i = 0; i < backgrounds.Length; i++)
            {
                //the parallax is the opposite of the camera movement because the previous frame multiplied by the scale
                float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

                // set a target x position which is the current position plus the parallax
                float backgroundTargetPosX = backgrounds[i].position.x + parallax;

                //create a target position which is the background's current position with it's target x position
                Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

                // fade between current position and the target position using lerp
                backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
            }
        }
		//set the previousCamPos to the camera's position at the end of the frame
		previousCamPos = cam.position;
	}
}
