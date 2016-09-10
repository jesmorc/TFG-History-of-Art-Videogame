using UnityEngine;
using System.Collections;

public class exit_inner : MonoBehaviour
{
    public GameObject inner_part;
    public GameObject outside_part;

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
        if (collider.tag == "Player")
        {
            hide_inner();
        }
    }


    void hide_inner()
    {
        inner_part.SetActive(false);
        outside_part.SetActive(true);
    }

}
