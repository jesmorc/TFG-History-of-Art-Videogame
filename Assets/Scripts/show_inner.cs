using UnityEngine;
using System.Collections;

public class show_inner : MonoBehaviour {
    public GameObject[] inner_parts;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            set_active_church_inner();
        }
    }


    void set_active_church_inner()
    {
        foreach (GameObject element in inner_parts)
        {
            element.SetActive(true);
        }
        gameObject.SetActive(false);
    }

    void set_inactive_church_inner()
    {
        foreach (GameObject element in inner_parts)
        {
            element.SetActive(false);
        }
        gameObject.SetActive(true);
    }
}
