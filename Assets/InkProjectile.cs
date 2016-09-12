using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InkProjectile : MonoBehaviour {

    private GameObject target;
    private bool enemy_hit = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player" && !enemy_hit)
        {
            enemy_hit = true;

            GameObject[] searchResult = SceneManager.GetActiveScene().GetRootGameObjects();

            //GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if (searchResult != null)
            {
                for (int i = 0; i < searchResult.Length; i++)
                {
                    if (searchResult[i].tag == "Player" && searchResult[i].activeInHierarchy)
                    {
                        target = searchResult[i];
                        Debug.Log("Target encontrado: " + searchResult[i].name);
                    }

                }

            }

            target.GetComponent<MainCharacterController>().DamagePlayer(30);
         }
    }
}
    