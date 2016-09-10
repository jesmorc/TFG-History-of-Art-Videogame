using UnityEngine;
using System.Collections;

public class TriggerBullet : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D otherObj)
    {
        if (otherObj.tag == "Enemy") { 
            Destroy(gameObject);
        }
    }
}
