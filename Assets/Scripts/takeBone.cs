using UnityEngine;
using System.Collections;

public class takeBone : MonoBehaviour {
    public GameObject character;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "bone")
        {
            Destroy(collider.gameObject);

            character.GetComponent<MainCharacterController>().takeBone();
        }
    }
}
