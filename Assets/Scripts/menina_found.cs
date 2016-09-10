using UnityEngine;
using System.Collections;

public class menina_found : MonoBehaviour {
    // Use this for initialization
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Menina")
        {
            //Destroy(collider.gameObject);

            gameObject.GetComponent<MainCharacterController>().menina_found = true;
        }
    }
}
