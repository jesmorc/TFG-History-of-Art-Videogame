using UnityEngine;
using System.Collections;

public class kill_character_xilo : MonoBehaviour
{

    public GameObject character;
    public GameObject character_ball;

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
            character_ball.GetComponent<setSpawnPoint>().setToSpawn();
        }
    }
}