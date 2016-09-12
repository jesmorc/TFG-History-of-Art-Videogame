using UnityEngine;
using System.Collections;

public class takeKey : MonoBehaviour {
    public GameObject openDoor;
    public GameObject closeDoor;

	// Use this for initialization
	void Start () {
	
	}

	
	// Update is called once per frame
	void Update () {
	
	}

    void llaveCogida()
    {
        openDoor.SetActive(true);
        closeDoor.SetActive(false);

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "key")
        {
            llaveCogida();
            gameObject.GetComponent<MainCharacterController>().key_found = true;
            Destroy(collider.gameObject);

        }
    }
}
