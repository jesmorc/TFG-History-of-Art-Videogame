﻿using UnityEngine;
using System.Collections;

public class takeBone : MonoBehaviour {
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

            this.GetComponent<MainCharacterController>().takeBone();
        }
    }
}
