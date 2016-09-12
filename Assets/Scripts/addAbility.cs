using UnityEngine;
using System.Collections;

public class addAbility : MonoBehaviour
{

    public Transform playerPrefabXilo2D;
    public Transform playerPrefabXilo;

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

        

            if (playerPrefabXilo != null && playerPrefabXilo2D != null)
            {

                if (playerPrefabXilo.gameObject.activeSelf)
                {

                    playerPrefabXilo2D.gameObject.SetActive(!playerPrefabXilo2D.gameObject.activeSelf);
                    playerPrefabXilo2D.position = playerPrefabXilo.position;

                    playerPrefabXilo.gameObject.SetActive(false);
                
                }
                else
                {

                    playerPrefabXilo.gameObject.SetActive(!playerPrefabXilo.gameObject.activeSelf);
                    playerPrefabXilo.position = playerPrefabXilo2D.position;
                    playerPrefabXilo2D.gameObject.SetActive(false);

                    
                }
            }

        
    }

}