using UnityEngine;
using System.Collections;

public class setSpawnPoint : MonoBehaviour {

    public GameObject spawnPoint;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setToSpawn()
    {
        transform.position = spawnPoint.transform.position;
    }
}
