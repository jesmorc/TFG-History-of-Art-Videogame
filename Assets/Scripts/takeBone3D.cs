using UnityEngine;
using System.Collections;

public class takeBone3D : MonoBehaviour {

    public GameObject character;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "bone")
        {
            Destroy(collider.gameObject);

            character.GetComponent<MainCharacterController>().takeBone();
        }
    }
}
