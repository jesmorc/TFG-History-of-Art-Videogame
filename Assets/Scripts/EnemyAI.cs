using UnityEngine;
using Pathfinding;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]
public class EnemyAI : MonoBehaviour {

	//what to chase
	public Transform target;

	//how many times a second we will update our path
	public float updateRate = 2f;

	//caching
	private Seeker seeker;
	private Rigidbody2D rb;

	//The calculated path
	public Path path;

	//THe AI's speed per second
	public float speed = 300f;
	public ForceMode2D fMode;	//a way to change between force and impulse

	[HideInInspector]
	public bool pathIsEnded = false;

	//THe max distance from the AI to a waypoint for it to continue to the next waypoint
	public float nextWaypointDistance = 3;

	//the waypoint we are currently moving towards
	private int currentWaypoint = 0;

	private bool searchingForPlayer = false;

	void Start(){
		seeker = GetComponent<Seeker> ();
		rb = GetComponent<Rigidbody2D> ();

		if (target == null) {
			
			if (!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine (SearchForPlayer());
			}

			return;
		}

		//Start a new path to the target position, return the result to the OnPathComplete method
		seeker.StartPath(transform.position,target.position, OnPathComplete);


		StartCoroutine (UpdatePath());

	}

	IEnumerator SearchForPlayer(){
		GameObject sResult = GameObject.FindGameObjectWithTag ("Player");
		if (sResult == null) {
			yield return new WaitForSeconds (0.5f);
			StartCoroutine (SearchForPlayer ());
		} else {
			target = sResult.transform;
			searchingForPlayer = false;
			StartCoroutine (UpdatePath());
			return false;
		}
	}

	IEnumerator UpdatePath() {
		if (target == null) {

			if (!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine (SearchForPlayer());
			}

			return false;
		}

		//Start a new path to the target position, return the result to the OnPathComplete method
		seeker.StartPath(transform.position,target.position, OnPathComplete);

		yield return new WaitForSeconds (1f / updateRate);
		StartCoroutine (UpdatePath());


	}

	public void OnPathComplete(Path p){
		Debug.Log("We got a path. Did it have an error?" + p.error);
		if(!p.error){
			path = p;
			currentWaypoint  = 0;
		}
	}

	//like update but not every frame, is better for doing physics calculations
	void FixedUpdate(){

		if (target == null) {
			//TODO: insert a player search here.
			return;
		}

		//TODO: always look at player?
		if (path == null) {
			return;
		}

		if (currentWaypoint >= path.vectorPath.Count) {
			if (pathIsEnded) {
				return;
			}
			Debug.Log ("End of path reached.");
			pathIsEnded = true;
			return;

		}
		pathIsEnded = false;

		//Direction to next waypoint
		Vector3 dir = ( path.vectorPath[currentWaypoint] - transform.position ).normalized;  //to get a more simple one we normalize it
		dir *= speed * Time.fixedDeltaTime;

		//Move the AI
		rb.AddForce(dir, fMode);

		//check if we are close enough to the next waypoint
		float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
		if(dist < nextWaypointDistance ){
			currentWaypoint++;
			return;
		}

	}
}
