using UnityEngine;
using System.Collections;

public class kill_character : MonoBehaviour
{

    public GameObject character;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D otherObj)
    {
        if (otherObj.tag == "Player")
        {
            character.GetComponent<MainCharacterController>().setToSpawn();
        }
    }
}
